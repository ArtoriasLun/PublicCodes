using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ALUN
{
    public abstract class Creature : MonoBehaviour
    {
        public int id;
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
            creatureParameters.creatureGameInfo = creatureParametersDataset.GetParameterById(creatureParameters.id).creatureGameInfo;
            creatureParameters.creatureNeuralInfo.fitness = 0;
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
            onNaturalDied?.Invoke(this);
            ResetCreature(); // 重置 CreatureGameInfo
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
        public static RaycastHit RaycastForObstacle(Vector3 position, Vector3 direction, float distance)
        {
            // LayerMask for Terrain and Default layer
            int obstacleLayerMask = 1 << LayerMask.NameToLayer("Terrain") | 1 << LayerMask.NameToLayer("Default");
            RaycastHit hit;
            if (Physics.Raycast(position, direction, out hit, distance, obstacleLayerMask))
            {
                return hit;
            }

            return hit;
        }
        public static RaycastHit RaycastForCreature(Vector3 position, Vector3 direction, float distance)
        {
            // LayerMask for Creature layer
            int creatureLayerMask = 1 << LayerMask.NameToLayer("Creature");
            RaycastHit hit;
            if (Physics.Raycast(position, direction, out hit, distance, creatureLayerMask))
            {
                return hit;
            }

            return hit;
        }
        public static IGrowable RaycastForFood(Vector3 position, Vector3 direction, float distance, out RaycastHit hit)
        {
            // LayerMask for Interactive layer
            int foodLayerMask = 1 << LayerMask.NameToLayer("Interactive");
            if (Physics.Raycast(position, direction, out hit, distance, foodLayerMask))
            {
                // Check if the hit object has the IGrowable component
                IGrowable growableObject = hit.collider.gameObject.GetComponent<IGrowable>();
                if (growableObject != null)
                {
                    return growableObject;
                }
            }

            return null; // Return null if no IGrowable component found or no object hit
        }
    }
    [System.Serializable]
    public class CreatureGenome
    {
        public static int nextID = 0;  // 静态字段，用于生成唯一识别号
        public int id;  // 每个实例的唯一识别号
        public NeuralNetwork neuralNetwork;  // 生物的神经网络
        public float fitness;  // 生物的奖励值
        public CreatureGenome(NeuralNetwork network, float reward)
        {
            this.neuralNetwork = network;
            this.fitness = reward;
            id=nextID;
            nextID++;
        }
    }

}