using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ALUN
{
    public class ThermalGameObject : MonoBehaviour, IThermal
    {
        [SerializeField, Tooltip("物体影响的半径")]
        private float updateRadius = 10f;
        [SerializeField, Tooltip("当前的温度值")]
        private float currentTemperature = 20;

        [SerializeField, Tooltip("此物体对周围环境的温度影响力")]
        private float temperatureInfluence;

        [SerializeField, Tooltip("温度达到此值时，会触发高温交互")]
        private float highTempThreshold = 40f;

        [SerializeField, Tooltip("温度达到此值时，会触发低温交互")]
        private float lowTempThreshold = 0f;

        [SerializeField, Tooltip("物体周围环境温差超过此值时，开始升温")]
        private float heatingThreshold = 5f;

        [SerializeField, Tooltip("物体周围环境温差低于此值时，开始降温")]
        private float coolingThreshold = 5f;

        [SerializeField, Tooltip("物体升温的速度")]
        private float heatingRate = 1;

        [SerializeField, Tooltip("物体降温的速度")]
        private float coolingRate = 1;
        // 新增一个变量用于保存初始温度
        private float initialTemperature;

        private void Awake()
        {
            // 在物体创建时记录初始温度
            initialTemperature = currentTemperature;
        }
        public bool UpdateObject()
        {
            // 物体的更新逻辑
            InfluenceSurroundings();
            CheckTemperatureThresholds();
            RecoverToInitialTemperature();
            return true;
        }
        // 当受到温度影响时进行的操作
        public void ReceiveTemperatureInfluence(float incomingTemperature, float influence)
        {
            float temperatureDifference = Mathf.Abs(currentTemperature - incomingTemperature);
            if (incomingTemperature > currentTemperature && temperatureDifference > heatingThreshold)
            {
                // 其中 heatingRate 是升温速率，可根据需要进行调整
                currentTemperature += heatingRate * influence;
            }
            else if (incomingTemperature < currentTemperature && temperatureDifference > coolingThreshold)
            {
                // 其中 coolingRate 是降温速率，可根据需要进行调整
                currentTemperature -= coolingRate * influence;
            }
            CheckTemperatureThresholds();
        }

        public List<ThermalGameObject> updatedObjects = new List<ThermalGameObject>(); // 用于展示的已更新物体列表

        // 对周围环境产生影响
        public void InfluenceSurroundings()
        {
            updatedObjects.Clear();
            updatedObjects = InterfaceRangeChecker<ThermalGameObject>.CheckRange(transform, updateRadius, c => c.ReceiveTemperatureInfluence(currentTemperature, temperatureInfluence));

        }

        // 检查当前温度是否超过了温度阈值，如果超过则进行相应的处理
        private void CheckTemperatureThresholds()
        {
            if (currentTemperature > highTempThreshold)
            {
                // 执行高温交互
            }
            else if (currentTemperature < lowTempThreshold)
            {
                // 执行低温交互
            }
        }
        // 新增一个方法用于恢复到初始温度
        private void RecoverToInitialTemperature()
        {
            if (currentTemperature < initialTemperature)
            {
                currentTemperature += heatingRate;
                // 保证不会超过初始温度
                if (currentTemperature > initialTemperature)
                {
                    currentTemperature = initialTemperature;
                }
            }
            else if (currentTemperature > initialTemperature)
            {
                currentTemperature -= coolingRate;
                // 保证不会低于初始温度
                if (currentTemperature < initialTemperature)
                {
                    currentTemperature = initialTemperature;
                }
            }
        }
    }
}