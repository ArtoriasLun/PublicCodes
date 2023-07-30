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
            DetectAndDrawRays();
            ConsumeNutrition(creatureParameters.creatureGameInfo.nutritionDecayRate * Time.deltaTime);
        }
        // 检测周围的障碍物和食物给与奖励或惩罚
        public override void DetectAndDrawRays()
        {
            Vector3 position = transform.position + Vector3.up * 0.4f;
            // 获取最近的障碍物位置
            Vector3[] rayDirections = new Vector3[] {
                transform.forward, // 前方
                -transform.forward,//后方
                -transform.right, // 左侧
                transform.right, // 右侧
                Quaternion.Euler(0, 45, 0) * transform.forward, // 前右方
                Quaternion.Euler(0, -45, 0) * transform.forward, // 前左方
                Quaternion.Euler(0, 135, 0) * transform.forward, // 后左方
                Quaternion.Euler(0, -135, 0) * transform.forward // 后右方
            };  // 设定射线方向，包括前、后、左、右及其对角线 

            //检测周围的障碍物和食物
            for (int i = 0; i < rayDirections.Length; i++)
            {
                RaycastHit obstacleHit = RaycastForObstacle(position, rayDirections[i], 1f);
                if (obstacleHit.collider != null)
                    PenaltyForCollision();
                else
                    FitnessForAvoidance();

                RaycastHit foodHit;
                IGrowable plant = RaycastForFood(position, rayDirections[i], 1f, out foodHit);
                if (plant != null && !plant.GetGameObject().GetComponent<InfinitePlant>().isHarvested)
                {
                    FitnessForEating(plant.HarvestPlant());
                }
            }



        }
        // 当生物撞到障碍物时，给予惩罚
        private void PenaltyForCollision()
        {
            creatureParameters.creatureNeuralInfo.fitness -= 1f * Time.timeScale;
        }

        // 当生物成功避开障碍物时，给予奖励
        private void FitnessForAvoidance()
        {
            creatureParameters.creatureNeuralInfo.fitness += 0.1f * Time.timeScale;
        }

        //当生物成功吃到食物时，给予奖励
        private void FitnessForEating(float fitness = 10f)
        {
            creatureParameters.creatureNeuralInfo.fitness += fitness * Time.timeScale;
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