using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ALUN
{
    public class CharacterSpawner : MonoBehaviour
    {
        public CreatureParametersDataset creatureParametersDataset;
        public GameObject creaturePrefab;
        public Transform spawnPoint;
        public List<CreatureGenome> creatures = new List<CreatureGenome>();
        public List<CreatureGenome> lastGenerationCreatures = new List<CreatureGenome>(); // 新增列表以保存前一代基因信息

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
            // nextGenerationSize = populationSize;
            for (int i = 0; i < populationSize; i++)
            {
                Debug.Log("生成第 " + generation + " 代，第 " + i + " 个生物。" + i % populationSize);
                SpawnCreature(i);
            }
            panelPositionOffset = new Vector2(((-(float)Screen.width / 2f) + (panelSize.x / 2f) + 30f), 30f);
        }

        public void SpawnCreature(int index)
        {
            Vector3 randomPosition = transform.position + (UnityEngine.Random.insideUnitSphere * spawnRadius);
            randomPosition.y = height;
            GameObject creatureGO = ObjectPoolerManager.GetInstance(creaturePrefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);
            creatureGO.transform.position = randomPosition;

            // 存储基因信息
            NeuralNetworkManager neuralNetworkManager = creatureGO.GetComponent<NeuralNetworkManager>();
            NeuralNetwork neuralNetwork = neuralNetworkManager.neuralNetwork; // 创建新的神经网络
            CreatureGenome creatureGenome = new CreatureGenome(neuralNetwork, 0, creatureGO.transform); // 初始化基因信息
            if (creatureGenome.transform.gameObject.activeSelf) creatures.Add(creatureGenome);

            creatureGO.GetComponent<Creature>().onNaturalDied = CreatureNatureDied;


            CreatureParameters creatureParameters = GetCreatureParameters(index);
            creatureGO.GetComponent<NeuralNetworkManager>().Initialize(creatureParameters);
        }

        private CreatureParameters GetCreatureParameters(int index)
        {
            CreatureParameters baseParameters = creatureParametersDataset.dataset[0];
            CreatureParameters creatureParameters = baseParameters.Clone();

            creatureParameters.creatureName = baseParameters.creatureName + " " + index;

            if (creatures.Count > 0)
            {
                creatureParameters.creatureNeuralInfo.reward = creatures[index].reward;
            }

            NeuralNetworkManager neuralNetworkManager = creaturePrefab.GetComponent<NeuralNetworkManager>();
            if (neuralNetworkManager != null)
            {
                NeuralNetwork neuralNetwork = creatures[index].neuralNetwork;
                neuralNetworkManager.neuralNetwork = neuralNetwork != null ? neuralNetwork.Clone() : null;
            }

            return creatureParameters;
        }

        public void CreatureNatureDied(Creature creatureGenome)
        {
            // 修改基因信息
            CreatureGenome cg = creatures.Find(c => c.transform == creatureGenome.transform);
            cg.neuralNetwork = creatureGenome.neuralNetworkManager.neuralNetwork.Clone(); // 复制神经网络
            cg.reward = creatureGenome.creatureParameters.creatureNeuralInfo.reward; // 保存奖励值
            deadCreatures++; // 生物死亡时，增加死亡生物的数量

            bool allDied = deadCreatures == creatures.Count; // 检查所有生物是否都已经死亡
            Debug.Log("生物自然死亡，奖励值为 " + cg.reward + " is All Died " + allDied + "creatures.Count " + creatures.Count);
            // 检查是否所有的生物都已经死亡
            if (allDied)
            {
                Debug.Log("所有生物都已死亡，生成下一代。");
                lastGenerationCreatures = new List<CreatureGenome>(creatures);
                creatures.Clear();
                CreateNewGeneration();
            }
        }
        private void CreateNewGeneration()
        {
            // 计算当前一代的reward平均值，并添加到generationRewards列表中
            float averageReward = lastGenerationCreatures.Count > 0 ? lastGenerationCreatures.Average(lastGenerationCreatures => lastGenerationCreatures.reward) : 0f;
            generationRewards.Add(averageReward);
            deadCreatures = 0;
            generation++;

            List<CreatureGenome> newGeneration = new List<CreatureGenome>();

            // 使用轮盘赌选择策略生成新的生物
            float totalReward = lastGenerationCreatures.Sum(creature => creature.reward);
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
                int randomClone = UnityEngine.Random.Range(0, 2);

                // 使用轮盘赌选择策略生成新的基因
                for (int i = 0; i < numGenesToAdd; i++)
                {
                    float rand = UnityEngine.Random.Range(0, totalReward);
                    float accumulativeReward = 0;
                    foreach (CreatureGenome genome in lastGenerationCreatures)
                    {
                        accumulativeReward += genome.reward;
                        if (accumulativeReward >= rand)
                        {
                            CreatureGenome clonedGenome = CloneGenome(genome);
                            clonedGenome.neuralNetwork.Mutate(0.1f - 0.05f * (clonedGenome.reward / totalReward)); // 设置突变率
                            newGeneration.Add(clonedGenome);
                            break;
                        }
                    }
                }
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
                genome.reward = 0;
            }

            StartCoroutine(SpawnCreaturesWithRaidus());
        }



        private CreatureGenome CloneGenome(CreatureGenome original)
        {
            CreatureGenome clone = new CreatureGenome(original.neuralNetwork != null ? original.neuralNetwork.Clone() : null, original.reward, original.transform);
            return clone;
        }

        [SerializeField] int spawnIndex = 0;

        [HideInInspector] public Vector3 playerPosition;
        IEnumerator SpawnCreaturesWithRaidus()
        {
            spawnIndex = 0;

            while (spawnIndex < populationSize)
            {

                float distanceToPlayer = Vector3.Distance(playerPosition, transform.position);

                if (distanceToPlayer <= spawnRadius) // spawnRadius是生成生物的有效半径，需要你自己定义
                {
                    SpawnCreature(spawnIndex); // SpawnCreature是生成生物的方法，需要你自己定义
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
                if (creature.reward > highestReward)
                {
                    highestReward = creature.reward;
                }
            }
            return highestReward;
        }
        private GUIStyle guiStyle = new GUIStyle();
        public float gameSpeed = 1f;
        public Color panelBackgroundColor = new Color(0f, 0f, 0f, 0.2f); // 默认为透明度为0.2的黑色背景色
        public Vector2 panelPositionOffset = new Vector2(0, 0); // 面板的位置偏移量，默认为零
        public Vector2 panelSize = new Vector2(300f, 200f); // 面板的初始大小
        private List<float> generationRewards = new List<float>();
        private void OnGUI()
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);

            // Check if the Transform is inside the camera's viewport
            if (viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1)
            {
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
                GUI.Label(new Rect(labelX, labelY, labelWidth, labelHeight), "最高奖励: " + GetHighestReward().ToString(), guiStyle);


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


                // labelY += labelMargin; // 增加间距，将 Y 轴位置调整为下一个标签的位置
                // GUI.Label(new Rect(labelX, labelY, labelWidth, labelHeight), "FPS: " + fps.ToString("F2"), guiStyle);
            }

            // DrawAverageRewardChart();
        }

        public List<float> ASCIIList = new List<float>();
        public int lineHeight = 20;
        public int columnWidth = 20;

        private void DrawAverageRewardChart()
        {
            if (generationRewards.Count == 0) return;
            ASCIIList = new List<float>();

            // 找到generationRewards列表中的最大绝对值
            float maxAbsReward = float.MinValue;
            foreach (float reward in generationRewards)
            {
                if (Mathf.Abs(reward) > maxAbsReward)
                {
                    maxAbsReward = Mathf.Abs(reward);
                }
            }

            // 对列表中的每一个数进行等比例的转化
            for (int i = 0; i < generationRewards.Count; i++)
            {
                // 如果最大绝对值是0，直接把值设为0以避免除以0的情况
                float currentHeight = maxAbsReward != 0 ? (generationRewards[i] / maxAbsReward) * 10 : 0;
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