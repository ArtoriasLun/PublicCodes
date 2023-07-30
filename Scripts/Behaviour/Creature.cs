using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ALUN
{
    public abstract class Creature : MonoBehaviour
    {
        public NeuralNetworkManager neuralNetworkManager;
        public CreatureParametersDataset creatureParametersDataset;
        public CreatureParameters creatureParameters;
        public Action<Creature> onNaturalDied;
        protected List<GameObjectModelFade> fade = new List<GameObjectModelFade>();
        private void OnEnable()
        {
            if (fade.Count < 1)
                fade.AddRange(GetComponentsInChildren<GameObjectModelFade>());
            foreach (var item in fade)
                item.Play(0.5f, -1);
        }
        public abstract void Move(Vector3 direction);
        public abstract void Tick();
        public abstract void DetectAndDrawRays();
        public virtual void ResetCreature()
        {
            //TODO 这里要改成重置，而不是继承，继承应该在其他地方   
            creatureParameters.creatureGameInfo = creatureParametersDataset.GetParameterById(creatureParameters.id).creatureGameInfo;
            creatureParameters.creatureNeuralInfo.fitness=neuralNetworkManager.creatureParameters.creatureNeuralInfo.fitness;
            GetComponentInChildren<Animator>().speed = 1;
            creatureParameters.creatureGameFlag.isDied = false;
        }

        public virtual IEnumerator NatureDie(float delay)
        {
            GetComponentInChildren<Animator>().speed = 0;
            creatureParameters.creatureGameFlag.isDied = true;
            foreach (var item in fade)
                item.Play(delay, 1);
            yield return new WaitForSeconds(delay);
            ResetCreature(); // 重置 CreatureGameInfo
            onNaturalDied?.Invoke(this);
            ObjectPoolerManager.ReleaseInstance(gameObject);
        }

        public virtual void ConsumeNutrition(float amount)
        {
            creatureParameters.creatureGameInfo.nutritionValue -= amount;
            if (creatureParameters.creatureGameInfo.nutritionValue <= 0)
            {
                StartCoroutine(NatureDie(5f));
            }
        }

        public virtual void GainNutrition(float amount)
        {
            creatureParameters.creatureGameInfo.nutritionValue += amount;
            if (creatureParameters.creatureGameInfo.nutritionValue >= 100)
            {
                Creature clone = Clone();
                creatureParameters.creatureGameInfo.nutritionValue = 10;
            }
        }

        public virtual Creature Clone()
        {
            Creature clone = ObjectPoolerManager.GetInstance(gameObject, transform.position, transform.rotation, transform.parent).GetComponent<Creature>();
            return clone;
        }
    }
    [System.Serializable]
    public class CreatureGenome
    {
        public NeuralNetwork neuralNetwork;  // 生物的神经网络
        public float reward;  // 生物的奖励值
        public Transform transform;  // 生物的 transform 位置参数

        public CreatureGenome(NeuralNetwork network, float reward, Transform transform)
        {
            this.neuralNetwork = network;
            this.reward = reward;
            this.transform = transform;
        }
    }

}