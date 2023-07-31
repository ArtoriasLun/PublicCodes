using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace ALUN
{
    public class CharacterSpawner : MonoBehaviour
    {
        public bool autoLoadGeneration, autoSaveGeneration;
        public CreatureParametersDataset creatureParametersDataset;
        public GameObject creaturePrefab;
        public Transform spawnPoint;
        public List<Creature> currentCreatures = new List<Creature>();
        public List<CreatureGenome> creatures = new List<CreatureGenome>();
        public List<CreatureGenome> lastGenerationCreatures = new List<CreatureGenome>(); // 新增列表以保存前一代基因信息
        public List<CreatureGenome> newGeneration;
        private int deadCreatures = 0; // 用来记录死亡生物的数量
        public int populationSize = 10;
        // int nextGenerationSize = 10; // 用于判断生成下一代的数量
        [SerializeField] private int generation = 1;
        private float spawnInterval = 0.5f;
        private float deltaTime = 0.0f;
        private int frames = 0;
        [SerializeField] private float height = 100f;
        [SerializeField] private float spawnRadius = 50f;
        [SerializeField] private int fpsLimit = 30;

        private void Start()
        {
            currentCreatures = new List<Creature>();
            // nextGenerationSize = populationSize;
            for (int i = 0; i < populationSize; i++)
            {
                Debug.Log("生成第 " + generation + " 代，第 " + i + " 个生物。" + i % populationSize);
                currentCreatures.Add(SpawnCreature(i));
            }
            panelPositionOffset = new Vector2(((-(float)Screen.width / 2f) + (panelSize.x / 2f) + 30f), 30f);
            if (autoLoadGeneration) LoadGeneration();
        }
        private void OnApplicationQuit()
        {
            if (autoSaveGeneration) SaveGeneration();
        }
        public Creature SpawnCreature(int index)
        {
            Vector3 randomPosition = transform.position + (UnityEngine.Random.insideUnitSphere * spawnRadius);
            randomPosition.y = height;
            GameObject creatureGO = ObjectPoolerManager.GetInstance(creaturePrefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);
            creatureGO.transform.position = randomPosition;

            // 存储基因信息
            NeuralNetworkManager neuralNetworkManager = creatureGO.GetComponent<NeuralNetworkManager>();
            NeuralNetwork neuralNetwork = neuralNetworkManager.neuralNetwork; // 创建新的神经网络
            CreatureGenome creatureGenome = new CreatureGenome(neuralNetwork, 0); // 初始化基因信息
            creatures.Add(creatureGenome);
            Creature c = creatureGO.GetComponent<Creature>();
            c.id = creatureGenome.id;
            c.onNaturalDied = CreatureNatureDied;

            CreatureParameters creatureParameters = GetCreatureParameters(index);
            c.neuralNetworkManager.Initialize(creatureParameters);
            return c;
        }

        private CreatureParameters GetCreatureParameters(int index)
        {
            CreatureParameters baseParameters = creatureParametersDataset.dataset[0];
            CreatureParameters creatureParameters = baseParameters.Clone();

            creatureParameters.creatureName = baseParameters.creatureName + " " + index;

            if (creatures.Count > 0)
            {
                creatureParameters.creatureNeuralInfo.fitness = creatures[index].fitness;
            }

            NeuralNetworkManager neuralNetworkManager = creaturePrefab.GetComponent<NeuralNetworkManager>();
            if (neuralNetworkManager != null)
            {
                NeuralNetwork neuralNetwork = newGeneration[index].neuralNetwork;
                neuralNetworkManager.neuralNetwork = neuralNetwork != null ? neuralNetwork.Clone() : null;
            }

            return creatureParameters;
        }
        bool importNewGeneration = false;
        public void CreatureNatureDied(Creature creature)
        {
            // 修改基因信息
            CreatureGenome cg = creatures.Find(c => c.id == creature.id);
            cg.neuralNetwork = creature.neuralNetworkManager.neuralNetwork.Clone(); // 复制神经网络
            cg.fitness = creature.creatureParameters.creatureNeuralInfo.fitness; // 保存奖励值
            deadCreatures++; // 生物死亡时，增加死亡生物的数量

            bool allDied = deadCreatures == creatures.Count; // 检查所有生物是否都已经死亡
            Debug.Log("生物自然死亡，奖励值为 " + cg.fitness + " is All Died " + allDied + "creatures.Count " + creatures.Count);
            // 检查是否所有的生物都已经死亡
            if (allDied)
            {
                Debug.Log("所有生物都已死亡，生成下一代。");
                if (!importNewGeneration) lastGenerationCreatures = new List<CreatureGenome>(creatures);
                importNewGeneration = false;
                creatures.Clear();
                CreateNewGeneration();
            }
        }
        private void CreateNewGeneration()
        {
            // 计算当前一代的reward平均值，并添加到generationRewards列表中
            float averageFitness = lastGenerationCreatures.Count > 0 ? lastGenerationCreatures.Average(lastGenerationCreatures => lastGenerationCreatures.fitness) : 0f;
            generationFitness.Add(averageFitness);
            deadCreatures = 0;
            generation++;
            newGeneration = new List<CreatureGenome>();



            // 使用轮盘赌选择策略生成新的生物
            float totalReward = lastGenerationCreatures.Sum(creature => creature.fitness);
            int numGenesToAdd = Mathf.Max(populationSize - lastGenerationCreatures.Count, 0);

            if (totalReward <= 0f || numGenesToAdd == 0)
            {
                // 如果前一代没有足够的基因，或者总奖励值小于等于0，则采用随机生成基因的方法
                for (int i = 0; i < populationSize; i++)
                {
                    CreatureGenome randomGenome = CloneGenome(lastGenerationCreatures[UnityEngine.Random.Range(0, lastGenerationCreatures.Count)]);
                    randomGenome.neuralNetwork.Mutate(0.1f); // 设置随机突变率
                    newGeneration.Add(randomGenome);
                }
            }
            else
            {
                AverageFitnessSelectionAndCrossoverMutation(totalReward);
                // // 在这里添加交叉和变异的代码
                // for (int i = 0; i < numGenesToAdd; i++)
                // {
                //     // 随机选择第一个生物
                //     CreatureGenome parent1 = lastGenerationCreatures[UnityEngine.Random.Range(0, lastGenerationCreatures.Count)];

                //     // 创建一个新的列表，包含除了 parent1 之外的所有生物
                //     List<CreatureGenome> otherCreatures = new List<CreatureGenome>(lastGenerationCreatures);
                //     otherCreatures.Remove(parent1);

                //     // 从新的列表中随机选择第二个生物
                //     CreatureGenome parent2 = otherCreatures[UnityEngine.Random.Range(0, otherCreatures.Count)];

                //     // 克隆其中一个生物的基因组
                //     CreatureGenome offspring = CloneGenome(parent1);

                //     // 对克隆得到的基因组进行交叉
                //     offspring.neuralNetwork.Crossover(parent2.neuralNetwork);

                //     // 对交叉后的基因组进行变异
                //     offspring.neuralNetwork.Mutate(0.1f - 0.05f * (offspring.fitness / totalReward));

                //     // 将新的基因组添加到新一代的基因组列表中
                //     newGeneration.Add(offspring);
                // }
            }

            // 补充前一代的剩余基因
            int numGenesRemaining = populationSize - newGeneration.Count;
            for (int i = 0; i < numGenesRemaining; i++)
            {
                CreatureGenome randomGenome = CloneGenome(lastGenerationCreatures[UnityEngine.Random.Range(0, lastGenerationCreatures.Count)]);
                randomGenome.neuralNetwork.Mutate(0.1f); // 设置随机突变率
                newGeneration.Add(randomGenome);
            }

            Debug.Log("Creatures.Count " + creatures.Count + "newGeneration.Count " + newGeneration.Count + " populationSize " + populationSize);

            // 清零reward
            foreach (CreatureGenome genome in newGeneration)
            {
                genome.fitness = 0;
            }

            StartCoroutine(SpawnCreaturesWithRaidus());
        }
        /// <summary>
        /// 平均适应度淘汰选择和交叉变异
        /// </summary>
        /// <param name="totalReward"></param>
        private void AverageFitnessSelectionAndCrossoverMutation(float totalReward)
        {
            float averageFitness = lastGenerationCreatures.Average(creature => creature.fitness);
            List<CreatureGenome> aboveAverageCreatures = lastGenerationCreatures.Where(creature => creature.fitness >= averageFitness).ToList();

            int numGenesToAdd = lastGenerationCreatures.Count - aboveAverageCreatures.Count;

            for (int i = 0; i < numGenesToAdd; i++)
            {
                CreatureGenome parent1 = aboveAverageCreatures[UnityEngine.Random.Range(0, aboveAverageCreatures.Count)];
                // 创建一个新的列表，包含除了 parent1 之外的所有生物
                List<CreatureGenome> otherCreatures = new List<CreatureGenome>(lastGenerationCreatures);
                otherCreatures.Remove(parent1);

                // 从新的列表中随机选择第二个生物
                CreatureGenome parent2 = otherCreatures[UnityEngine.Random.Range(0, aboveAverageCreatures.Count)];

                CreatureGenome child = CloneGenome(parent1);
                child.neuralNetwork.Crossover(parent2.neuralNetwork);
                child.neuralNetwork.Mutate(0.1f - 0.05f * (child.fitness / totalReward));

                aboveAverageCreatures.Add(child);
            }

            newGeneration = aboveAverageCreatures;
        }
        /// <summary>
        /// 轮盘赌选择和交叉变异
        /// </summary>
        /// <param name="totalReward"></param>
        private void RouletteWheelSelectionAndCrossoverMutation(float totalReward)
        {
            float totalFitness = lastGenerationCreatures.Sum(creature => creature.fitness);
            List<CreatureGenome> _newGeneration = new List<CreatureGenome>();

            for (int i = 0; i < lastGenerationCreatures.Count; i++)
            {
                float randomPoint = UnityEngine.Random.Range(0, totalFitness);
                float cumulativeFitness = 0;

                foreach (CreatureGenome creature in lastGenerationCreatures)
                {
                    cumulativeFitness += creature.fitness;
                    if (cumulativeFitness >= randomPoint)
                    {
                        CreatureGenome parent1 = creature;
                        // 创建一个新的列表，包含除了 parent1 之外的所有生物
                        List<CreatureGenome> otherCreatures = new List<CreatureGenome>(lastGenerationCreatures);
                        otherCreatures.Remove(parent1);

                        // 从新的列表中随机选择第二个生物
                        CreatureGenome parent2 = otherCreatures[UnityEngine.Random.Range(0, otherCreatures.Count)];

                        CreatureGenome child = CloneGenome(parent1);
                        child.neuralNetwork.Crossover(parent2.neuralNetwork);
                        child.neuralNetwork.Mutate(0.1f - 0.05f * (child.fitness / totalReward));

                        _newGeneration.Add(child);
                        break;
                    }
                }
            }

            newGeneration = _newGeneration;
        }


        private CreatureGenome CloneGenome(CreatureGenome original)
        {
            CreatureGenome clone = new CreatureGenome(original.neuralNetwork != null ? original.neuralNetwork.Clone() : null, original.fitness);
            return clone;
        }

        [SerializeField] int spawnIndex = 0;

        [HideInInspector] public Vector3 playerPosition;
        IEnumerator SpawnCreaturesWithRaidus()
        {
            spawnIndex = 0;
            currentCreatures = new List<Creature>();
            while (spawnIndex < populationSize)
            {

                float distanceToPlayer = Vector3.Distance(playerPosition, transform.position);

                if (distanceToPlayer <= spawnRadius) // spawnRadius是生成生物的有效半径，需要你自己定义
                {
                    currentCreatures.Add(SpawnCreature(spawnIndex));
                    spawnIndex++;
                }
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private string GetPopulationSizeProgressBar()
        {
            int currentPopulation = Mathf.Min(creatures.Count, populationSize);
            int progress = Mathf.RoundToInt((float)currentPopulation / populationSize * 20);
            string progressBar = "[";

            for (int i = 0; i < 20; i++)
            {
                if (i < progress)
                    progressBar += "#";
                else
                    progressBar += " ";
            }

            progressBar += "] " + currentPopulation + " / " + populationSize;

            return progressBar;
        }

        private float GetHighestReward()
        {
            float highestReward = 0f;
            foreach (CreatureGenome creature in creatures)
            {
                if (creature.fitness > highestReward)
                {
                    highestReward = creature.fitness;
                }
            }
            return highestReward;
        }
        private float GetAverageReward(List<CreatureGenome> targetGenomes)
        {
            float totalReward = 0f;
            foreach (CreatureGenome creature in targetGenomes)
            {
                totalReward += creature.fitness;
            }
            return creatures.Count > 0 ? totalReward / creatures.Count : 0f;
        }

        public void SaveGeneration()
        {
            // 获取当前的日期和时间，然后将其格式化为 "yyyyMMdd"
            string date = DateTime.Now.ToString("yyyyMMdd");
            string fileName = $"Export/lastGeneration-{date}.json";

            List<CreatureGenome> exportGeneration = new List<CreatureGenome>();
            for (int i = 0; i < lastGenerationCreatures.Count; i++)
            {
                exportGeneration.Add(lastGenerationCreatures[i]);
                exportGeneration[i].id = i;
            }
            // 导出最新的基因为json并且保存在一个json文件中
            string json = JsonConvert.SerializeObject(exportGeneration);
            System.IO.File.WriteAllText(fileName, json);
            //Debug保存信息，带上filename
            Debug.Log("导出最新的基因为json并且保存在json文件中" + fileName);

        }
        public void LoadGeneration()
        {
            // 获取当前的日期和时间，然后将其格式化为 "yyyyMMdd"
            string date = DateTime.Now.ToString("yyyyMMdd");
            string fileName = $"Export/lastGeneration-{date}.json";

            // 读取json文件中的基因信息
            string json = System.IO.File.ReadAllText(fileName);
            List<CreatureGenome> importGeneration = JsonConvert.DeserializeObject<List<CreatureGenome>>(json);
            // 将读取到的基因信息赋值给currentCreatures列表中的每一个生物
            for (int i = 0; i < importGeneration.Count; i++)
            {
                //判断如果currentCreatures数量小于导入的基因数量，就不再进行导入
                if (i < currentCreatures.Count)
                {
                    currentCreatures[i].id = importGeneration[i].id;
                    currentCreatures[i].neuralNetworkManager.neuralNetwork = importGeneration[i].neuralNetwork;
                    currentCreatures[i].creatureParameters.creatureNeuralInfo.fitness = importGeneration[i].fitness;
                }
                CreatureGenome.nextID = importGeneration[i].id + 1;
                lastGenerationCreatures = new List<CreatureGenome>(importGeneration);
                importNewGeneration = true;
            }
        }
        private GUIStyle guiStyle = new GUIStyle();
        public float gameSpeed = 1f;
        public Color panelBackgroundColor = new Color(0f, 0f, 0f, 0.2f); // 默认为透明度为0.2的黑色背景色
        public Vector2 panelPositionOffset = new Vector2(0, 0); // 面板的位置偏移量，默认为零
        public Vector2 panelSize = new Vector2(300f, 200f); // 面板的初始大小
        private List<float> generationFitness = new List<float>();
        private void OnGUI()
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);

            // Check if the Transform is inside the camera's viewport
            if (viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1)
            {
                guiStyle.normal.textColor = Color.white;
                float panelX = screenPos.x - panelSize.x / 2; // Subtract half the width
                float panelY = Screen.height - screenPos.y - panelSize.y; // Subtract half the height

                GUI.DrawTexture(new Rect(panelX, panelY, panelSize.x, panelSize.y), Texture2D.whiteTexture, ScaleMode.StretchToFill, true, 0f, panelBackgroundColor, 0f, 0f);
                GUI.Box(new Rect(panelX, panelY, panelSize.x, panelSize.y), "", guiStyle);

                float labelX = panelX;
                float labelY = panelY + 10;
                float labelWidth = panelSize.x;
                float labelHeight = 20;
                float labelMargin = 30;

                GUI.Label(new Rect(labelX, labelY, labelWidth, labelHeight), "代数: " + generation.ToString(), guiStyle);
                labelY += labelMargin;

                GUI.Label(new Rect(labelX, labelY, labelWidth, labelHeight), "种群大小: " + GetPopulationSizeProgressBar(), guiStyle);
                labelY += labelMargin;

                labelY += labelMargin; // 增加间距，将 Y 轴位置调整为下一个标签的位置
                GUI.Label(new Rect(labelX, labelY, labelWidth, labelHeight), "平均奖励: " + GetAverageReward(lastGenerationCreatures).ToString(), guiStyle);


                GUIStyle buttonStyleP = new GUIStyle(GUI.skin.button);
                buttonStyleP.normal.textColor = Color.white; // 按钮文字设置为纯黑色

                GUIStyle buttonStyleM = new GUIStyle(GUI.skin.button);
                buttonStyleM.normal.textColor = Color.white; // 按钮文字设置为纯黑色

                // 这里使用了前面的代码来设定按钮的位置和大小
                float buttonX = labelX;
                float buttonY = labelY + labelMargin;
                float buttonWidth = 100;
                float buttonHeight = 30;

                // 减少游戏速度的按钮
                if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "速度 - 0.5", buttonStyleM) && Time.timeScale >= 1.0f)
                {
                    Time.timeScale = Mathf.Max(0.5f, Time.timeScale - 0.5f);
                }

                buttonX += buttonWidth + 10; // 增加按钮之间的间距

                // 增加游戏速度的按钮
                if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "速度 + 0.5", buttonStyleP))
                {
                    Time.timeScale += 0.5f;
                }

                // 显示当前游戏速度的标签
                float speedLabelX = buttonX + buttonWidth + 10;
                float speedLabelY = buttonY;
                float speedLabelWidth = labelWidth;
                float speedLabelHeight = labelHeight;
                GUI.Label(new Rect(speedLabelX, speedLabelY, speedLabelWidth, speedLabelHeight), "游戏速度: " + Time.timeScale.ToString(), guiStyle);

                // 更新面板大小
                float panelHeight = speedLabelY + speedLabelHeight + 10 - panelY;
                panelSize.y = panelHeight;

                // 更新面板位置
                float panelXOffset = (Screen.width - panelSize.x) / 2 + panelPositionOffset.x;
                float panelYOffset = 20 + panelPositionOffset.y;
                float panelWidthOffset = panelSize.x;
                float panelHeightOffset = panelSize.y;
                GUI.Box(new Rect(panelXOffset, panelYOffset, panelWidthOffset, panelHeightOffset), "", guiStyle);

                // 计算文本的位置
                float textX = panelX + panelSize.x / 2 - 50;
                float textY = panelY - labelHeight;

                // 显示Box的文本信息在面板中间外部的顶部
                GUI.Label(new Rect(textX, textY, 100, labelHeight), "生物进化面板", guiStyle);



                // 创建 "保存基因" 按钮
                if (GUI.Button(new Rect(panelXOffset + 10, panelYOffset + panelHeightOffset + 10, 100, 50), "保存基因"))
                    SaveGeneration();

                //创建"读取基因"按钮,并用for循环修改currentCreatures列表中的每一个生物的基因和id
                if (GUI.Button(new Rect(panelXOffset + 120, panelYOffset + panelHeightOffset + 10, 100, 50), "读取基因"))
                    LoadGeneration();
            }

            // DrawAverageRewardChart();
        }

        public List<float> ASCIIList = new List<float>();
        public int lineHeight = 20;
        public int columnWidth = 20;

        private void DrawAverageRewardChart()
        {
            if (generationFitness.Count == 0) return;
            ASCIIList = new List<float>();

            // 找到generationRewards列表中的最大绝对值
            float maxAbsReward = float.MinValue;
            foreach (float reward in generationFitness)
            {
                if (Mathf.Abs(reward) > maxAbsReward)
                {
                    maxAbsReward = Mathf.Abs(reward);
                }
            }

            // 对列表中的每一个数进行等比例的转化
            for (int i = 0; i < generationFitness.Count; i++)
            {
                // 如果最大绝对值是0，直接把值设为0以避免除以0的情况
                float currentHeight = maxAbsReward != 0 ? (generationFitness[i] / maxAbsReward) * 10 : 0;
                if (i < ASCIIList.Count)
                {
                    ASCIIList[i] = currentHeight;
                }
                else
                {
                    ASCIIList.Add(currentHeight);
                }

                // 在这里绘制图表
                for (int j = 0; j < currentHeight; j++)
                {
                    // 注意，Unity的GUI坐标系在屏幕左上角为原点
                    // 为了在屏幕左下角绘制图形，我们需要根据屏幕高度来调整矩形的y坐标
                    GUI.Box(new Rect(i * columnWidth, Screen.height - (j + 1) * lineHeight, columnWidth, lineHeight), "#");
                }
            }
        }
    }
}
