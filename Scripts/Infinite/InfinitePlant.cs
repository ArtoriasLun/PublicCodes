using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ALUN;

[System.Serializable]
public class InfinitePlant : MonoBehaviour, ILocalUpdatable,IGrowable
{
    [Header("生长阶段")]
    public GrowthStage[] growthStages;
    [Header("营养值")]
    public float nutrition;

    [Header("当前显示的外形")]
    private GameObject currentShape;

    [Header("当前生长阶段")]
    public int currentStage = 0;

    // 每秒增加的营养值
    public float nutritionPerSecond = 0.1f;

    // 标记植物是否被采集
    public bool isHarvested = false;

    public bool UpdateObject()
    {
        if (!isHarvested)
        {
            IncreaseNutrition(nutritionPerSecond);
            CheckGrowthStage();
        }
        return true;
    }

    void IncreaseNutrition(float amount)
    {
        nutrition += amount * Time.timeScale;
    }

    void CheckGrowthStage()
    {
        for (int i = 0; i < growthStages.Length; i++)
        {
            // 如果营养值大于当前阶段的阈值，那么进入下一个阶段
            if (nutrition >= growthStages[i].nutritionThreshold)
            {
                // 仅在进入新阶段时更新
                if (currentStage != i + 1 && currentStage < growthStages.Length)
                {
                    currentStage = i + 1;
                    // 移除旧的外形
                    if (currentShape != null)
                        ObjectPoolerManager.ReleaseInstance(currentShape);

                    // 添加新的外形
                    currentShape = ObjectPoolerManager.GetInstance(growthStages[currentStage - 1].prefab, transform.position, transform.rotation, transform);
                    // Set the size of the new shape
                    currentShape.transform.localScale = Vector3.one * growthStages[currentStage - 1].size;

                    // Debug.Log("Plant has grown to stage " + currentStage);
                }
            }
        }
    }

    // 采集植物并触发重新生长
    public float HarvestPlant()
    {
        float nutritionValue = nutrition;
        isHarvested = true;
        RegrowPlant();
        return nutritionValue;
    }

    // 重新生长植物
    private void RegrowPlant()
    {
        // 重置营养值
        nutrition = 0f;

        // 重置当前生长阶段
        currentStage = 0;

        // 移除旧的外形
        if (currentShape != null)
            ObjectPoolerManager.ReleaseInstance(currentShape);

        // 添加新的外形，默认为第一个生长阶段的外形
        currentShape = ObjectPoolerManager.GetInstance(growthStages[currentStage].prefab, transform.position, transform.rotation, transform);
        // Set the size of the new shape
        currentShape.transform.localScale = Vector3.one * growthStages[currentStage].size;

        // 将植物标记为未采集状态
        isHarvested = false;
    }

    public float GetNutrition()
    {
       return nutrition;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public int GetCurrentStage()
    {
        return currentStage;
    }
}

[System.Serializable]
public class GrowthStage
{
    public float nutritionThreshold;
    public GameObject prefab;
    public float size = 1f;
}
