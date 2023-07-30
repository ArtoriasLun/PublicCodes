using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
namespace ALUN
{
    [CreateAssetMenu(menuName = "ALUN/CreatureParameters Dataset", order = 1)]
    public class CreatureParametersDataset : ScriptableObject
    {
        public List<CreatureParameters> dataset = new List<CreatureParameters>();

        public void AddParameter(CreatureParameters parameter)
        {
            dataset.Add(parameter);
        }

        public bool RemoveParameter(CreatureParameters parameter)
        {
            return dataset.Remove(parameter);
        }

        public CreatureParameters GetParameterById(int id)
        {
            return dataset.Find(parameter => parameter.id == id);
        }
    }

    [System.Serializable]
    public struct CreatureParameters
    {
        public int id;  // 基础参数：id
        public string creatureName;  // 基础参数：名称
        public CreatureGameInfo creatureGameInfo;  // 游戏参数
        public CreatureGameFlag creatureGameFlag; 
        public CreatureNeuralInfo creatureNeuralInfo;  // 神经网络参数
        public CreatureParameters Clone()
        {
            CreatureParameters clone = new CreatureParameters();
            clone.id = id;
            clone.creatureName = creatureName;
            clone.creatureGameFlag = new CreatureGameFlag();
            clone.creatureGameInfo = new CreatureGameInfo();
            clone.creatureGameInfo.moveSpeed = creatureGameInfo.moveSpeed;
            clone.creatureGameInfo.rotationSpeed = creatureGameInfo.rotationSpeed;
            clone.creatureGameInfo.nutritionValue = creatureGameInfo.nutritionValue;
            clone.creatureGameInfo.nutritionDecayRate = creatureGameInfo.nutritionDecayRate;
            clone.creatureNeuralInfo = new CreatureNeuralInfo();
            clone.creatureNeuralInfo.inputCount = creatureNeuralInfo.inputCount;
            clone.creatureNeuralInfo.hiddenLayerCount = creatureNeuralInfo.hiddenLayerCount;
            clone.creatureNeuralInfo.neuronPerHiddenLayer = creatureNeuralInfo.neuronPerHiddenLayer;
            clone.creatureNeuralInfo.outputCount = creatureNeuralInfo.outputCount;
            clone.creatureNeuralInfo.obstacleRayDistance = creatureNeuralInfo.obstacleRayDistance;
            clone.creatureNeuralInfo.foodRayDistance = creatureNeuralInfo.foodRayDistance;
            clone.creatureNeuralInfo.fitness = creatureNeuralInfo.fitness;
            clone.creatureNeuralInfo.penalty = creatureNeuralInfo.penalty;
            return clone;
        }

    }
    [System.Serializable]
    public struct CreatureNeuralInfo
    {
        [InfoBox("输入节点数")]
        public int inputCount;

        [InfoBox("输出节点数")]
        public int outputCount;

        [InfoBox("隐藏层数量")]
        public int hiddenLayerCount;

        [InfoBox("每个隐藏层的神经元数量")]
        public int neuronPerHiddenLayer;

        [InfoBox("食物射线")]
        public float foodRayDistance;
        [InfoBox("障碍物射线")]
        public float obstacleRayDistance;
        [InfoBox("奖励值")]
        public float fitness;

        [InfoBox("惩罚值")]
        public float penalty;

        private void Reset()
        {
            this.inputCount = 8;
            this.outputCount = 4;
            this.hiddenLayerCount = 1;
            this.neuronPerHiddenLayer = 8;
            this.foodRayDistance = 30f;
            this.obstacleRayDistance = 10f;
            this.fitness = 0f;
            this.penalty = 0f;
        }

        public CreatureNeuralInfo Clone()
        {
            CreatureNeuralInfo clone = new CreatureNeuralInfo();
            clone.inputCount = inputCount;
            clone.outputCount = outputCount;
            clone.hiddenLayerCount = hiddenLayerCount;
            clone.neuronPerHiddenLayer = neuronPerHiddenLayer;
            clone.foodRayDistance = foodRayDistance;
            clone.obstacleRayDistance = obstacleRayDistance;
            clone.fitness = fitness;
            clone.penalty = penalty;
            return clone;
        }
    }
    [System.Serializable]
    public struct CreatureGameFlag
    {
        public bool isDied;
    }
    [System.Serializable]
    public struct CreatureGameInfo
    {
        [InfoBox("移动速度")]
        public float moveSpeed;

        [InfoBox("旋转速度")]
        public float rotationSpeed;

        [InfoBox("营养值")]
        public float nutritionValue;

        [InfoBox("营养衰减速度")]
        public float nutritionDecayRate;

        private void Reset()
        {
            this.moveSpeed = 2f;
            this.rotationSpeed = 100f;
            this.nutritionValue = 10f;
            this.nutritionDecayRate = 0.1f;
        }
    }


}