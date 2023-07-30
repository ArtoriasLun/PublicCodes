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
            // // 重置神经网络输入
            // neuralNetworkManager.ResetInput();

            //检测周围的障碍物和食物
            for (int i = 0; i < rayDirections.Length; i++)
            {
                // 检测障碍物
                RaycastHit hit;
                //检测layer是Terrain或者Default的物体为障碍物

                bool obstacleHit = Physics.Raycast(position, rayDirections[i], out hit, 1, 1 << LayerMask.NameToLayer("Terrain") | 1 << LayerMask.NameToLayer("Default"));
                if (obstacleHit) PenaltyForCollision();
                // else RewardForAvoidance();

                bool foodHit = Physics.Raycast(position, rayDirections[i], out hit, 1, 1 << LayerMask.NameToLayer("Plant"));
                if (foodHit) RewardForEating();
            }



        }
           // 当生物撞到障碍物时，给予惩罚
        private void PenaltyForCollision()
        {
            creatureParameters.creatureNeuralInfo.reward -= 1f * Time.timeScale;
        }

        // 当生物成功避开障碍物时，给予奖励
        private void RewardForAvoidance()
        {
            creatureParameters.creatureNeuralInfo.reward += 0.1f * Time.timeScale;
        }

        //当生物成功吃到食物时，给予奖励
        private void RewardForEating()
        {
            creatureParameters.creatureNeuralInfo.reward += 10f * Time.timeScale;
        }
        private void DrawRay(Vector3 direction, bool raycastHit)
        {
            // 绘制射线，红色表示检测到障碍物，绿色表示没有检测到障碍物
            Color rayColor = raycastHit ? Color.red : Color.green;
            Debug.DrawRay(transform.position + Vector3.up * 0.5f, direction * 2f, rayColor);
        }

        // private void HandleInfinitePlantDetection(RaycastHit hit, InfinitePlant plant)
        // {
        //     float reward = 1;

        //     if (hit.distance <= 1f && plant.currentStage >= plant.growthStages.Length - 1)
        //     {
        //         GainNutrition(10f);
        //         reward += 10;
        //     }

        //     creatureParameters.creatureNeuralInfo.reward += reward;
        // }

        // private void HandleObstacleDetection(RaycastHit hit)
        // {
        //     float reward = -0.01f;

        //     // 作为神经网络输入的障碍物信息
        //     float obstacleDistance = hit.distance;
        //     Vector3 obstacleDirection = hit.normal;

        //     // 将障碍物信息输入到神经网络，获取输出的旋转角度
        //     float rotateAngle = neuralNetworkManager.GetOutput(obstacleDistance, obstacleDirection, 10f, 180f);
        //     Vector3 forceDirection = Quaternion.Euler(0, rotateAngle, 0) * transform.forward;

        //     // 根据神经网络输出的角度进行旋转
        //     Rotate(rotateAngle);

        //     // 根据神经网络输出的方向进行移动
        //     Move(forceDirection);

        //     creatureParameters.creatureNeuralInfo.reward += reward;
        // }

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