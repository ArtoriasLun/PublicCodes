using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ALUN
{
    public class CreatureTomato : Creature
    {
        public DebugPlane debugPlane;
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            creatureParameters = creatureParametersDataset.GetParameterById(creatureParameters.id).Clone();
            neuralNetworkManager = gameObject.Bind<NeuralNetworkManager>();
            neuralNetworkManager.Initialize(creatureParameters);

        }

        public override void Tick()
        {
            foreach (var item in fade)
                item.Tick();
            if (creatureParameters.creatureGameFlag.isDied) return;
            neuralNetworkManager.enabled = creatureParameters.creatureGameInfo.nutritionValue < 100f;
            DetectAndDrawRays();
            ConsumeNutrition(creatureParameters.creatureGameInfo.nutritionDecayRate * Time.deltaTime);
        }
        // 检测周围的障碍物和食物给与奖励或惩罚
        public override void DetectAndDrawRays() // 定义一个公共的、可以被子类重写的方法，名为"DetectAndDrawRays"，用于检测和绘制射线。
        {
            Vector3 position = transform.position + Vector3.up * 0.4f; // 定义一个三维向量"position"，表示当前物体的位置。这个位置是当前物体的位置加上向上0.4个单位的偏移。

            Vector3[] rayDirections = new Vector3[] { // 定义一个三维向量数组"rayDirections"，用于存储射线的方向。
        transform.forward, // 前方
        -transform.forward,//后方
        -transform.right, // 左侧
        transform.right, // 右侧
        Quaternion.Euler(0, 45, 0) * transform.forward, // 前右方
        Quaternion.Euler(0, -45, 0) * transform.forward, // 前左方
        Quaternion.Euler(0, 135, 0) * transform.forward, // 后左方
        Quaternion.Euler(0, -135, 0) * transform.forward // 后右方
    };  // 设定射线方向，包括前、后、左、右及其对角线 



            int detectedCreatures = 0;
            for (int i = 0; i < rayDirections.Length; i++) // 这是一个for循环，用于遍历所有的射线方向。
            {
                RaycastHit obstacleHit = RaycastForObstacle(position, rayDirections[i], 2f); // 使用RaycastForObstacle方法，从当前位置向指定方向发射射线，检测是否有障碍物，返回的结果存储在"obstacleHit"中。

                if (obstacleHit.collider != null) // 如果射线碰到了障碍物（即obstacleHit.collider不为空）。
                {
                    float angle = Vector3.Angle(obstacleHit.normal, Vector3.up); // 计算障碍物的角度，即障碍物的法线和向上方向的夹角。

                    if (angle >= 30) // 如果角度大于等于30度。
                    {
                        PenaltyForCollision(); // 调用PenaltyForCollision方法，对碰撞进行惩罚。
                    }
                }
                else // 如果射线没有碰到障碍物。
                {
                    FitnessForAvoidance(); // 调用FitnessForAvoidance方法，对避开障碍物进行奖励。
                }

                RaycastHit foodHit;
                IGrowable plant = RaycastForFood(position, rayDirections[i], 1f, out foodHit); // 使用RaycastForFood方法，从当前位置向指定方向发射射线，检测是否有食物，返回的结果存储在"plant"中，同时将射线碰撞的结果输出到"foodHit"。

                if (plant != null && !plant.GetGameObject().GetComponent<InfinitePlant>().isHarvested && plant.GetCurrentStage() > 0) // 如果射线碰到了食物，且食物没有被收获，且食物的当前阶段大于0。
                {
                    FitnessForEating(plant.HarvestPlant()); // 调用FitnessForEating方法，对吃食物进行奖励。
                }

                RaycastHit creatureHit = RaycastForCreature(position, rayDirections[i], 4f);//使用RaycastForCreature方法，从当前位置向指定方向发射射线，检测是否有生物，返回的结果存储在"creatureHit"中。
                if (creatureHit.collider != null) detectedCreatures++;
            }

            CheckCreatureCloseEachOther(detectedCreatures);
        }

        float checkTime = 1f;
        // 当生物撞到障碍物时，给予惩罚
        private void PenaltyForCollision()
        {
            //加一个时间间隔，防止每帧都检测
            if (checkTime > 0)
            {
                checkTime -= Time.deltaTime * Time.timeScale;
                return;
            }
            checkTime = 1f;
            creatureParameters.creatureNeuralInfo.fitness -= 1f * Time.timeScale;
            creatureParameters.creatureGameInfo.nutritionValue -= 1f * Time.timeScale;
        }

        // 当生物自由行走时，给予奖励
        private void FitnessForAvoidance()
        {
            creatureParameters.creatureNeuralInfo.fitness += 0.1f * Time.timeScale;
        }

        //当生物成功吃到食物时，给予奖励
        private void FitnessForEating(float nutrition = 10f)
        {
            creatureParameters.creatureGameInfo.nutritionValue += nutrition;
            creatureParameters.creatureNeuralInfo.fitness += nutrition;
        }
        /*
              检测到附近creature距离2，如果有2根射线检测到了，进行繁殖，增加奖励值
      检测到附近creature距离2，如果有4根射线检测到了，开始减少营养，减少奖励值
      检测到附近creature距离2，如果没有射线检测到，开始减少营养，减少奖励值
      检测到附近creature距离2，如果有1或2或3根射线检测到了，不减少营养，增加奖励值
              */
        private void CheckCreatureCloseEachOther(int detectedCreatures)
        {
            if (detectedCreatures == 2)
            {
                owner.SpawnAdd();
                creatureParameters.creatureNeuralInfo.fitness += 1f * Time.timeScale;
                creatureParameters.creatureGameInfo.nutritionValue += 1f * Time.timeScale;
            }
            else if (detectedCreatures == 4)
            {
                creatureParameters.creatureNeuralInfo.fitness -= 1f * Time.timeScale;
                creatureParameters.creatureGameInfo.nutritionValue -= 1f * Time.timeScale;
            }
            else if (detectedCreatures == 0)
            {
                creatureParameters.creatureNeuralInfo.fitness -= 1f * Time.timeScale;
                creatureParameters.creatureGameInfo.nutritionValue -= 1f * Time.timeScale;
            }
            else if (detectedCreatures == 1 || detectedCreatures == 3)
            {
                creatureParameters.creatureNeuralInfo.fitness += 1f * Time.timeScale;
                creatureParameters.creatureGameInfo.nutritionValue += 1f * Time.timeScale;
            }
        }


        private void DrawRay(Vector3 direction, bool raycastHit)
        {
            // 绘制射线，红色表示检测到障碍物，绿色表示没有检测到障碍物
            Color rayColor = raycastHit ? Color.red : Color.green;
            Debug.DrawRay(transform.position + Vector3.up * 0.5f, direction * 2f, rayColor);
        }


        float moveTime = 0.5f;
        public override void Move(Vector3 direction)
        {
            if (!HelperTimer.TickMinus(moveTime, (f) => moveTime = f)) return;
            moveTime = 0.5f;
            debugPlane.magnitude = rb.velocity.magnitude;

            if (rb.velocity.magnitude >= creatureParameters.creatureGameInfo.moveSpeed)
            {
                debugPlane.isAddForce = false;
                return;
            }

            debugPlane.isAddForce = true;

            float deviationAngle = Vector3.Angle(rb.velocity.normalized, direction.normalized);

            if (deviationAngle > 10f)
            {
                Vector3 reverseForce = -rb.velocity.normalized * creatureParameters.creatureGameInfo.moveSpeed;
                rb.AddForce(reverseForce);
            }

            rb.AddForce(direction.normalized * creatureParameters.creatureGameInfo.moveSpeed);
        }

        public void Rotate(float angle)
        {
            transform.Rotate(0, angle * creatureParameters.creatureGameInfo.rotationSpeed * Time.deltaTime, 0);
        }

        public override Creature Clone()
        {
            CreatureTomato clone = base.Clone() as CreatureTomato;
            clone.creatureParameters = this.creatureParameters.Clone();
            clone.neuralNetworkManager.neuralNetwork = this.neuralNetworkManager.neuralNetwork.Clone();
            return clone;
        }



    }
    [System.Serializable]
    public struct DebugPlane
    {
        public bool isAddForce;
        public float magnitude;
    }
}