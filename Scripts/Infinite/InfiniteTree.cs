using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GameCreator.Runtime.Melee;
using Sirenix.OdinInspector;

// 无尽树木类，继承自无尽游戏对象
public class InfiniteTree : InfiniteGameObject
{
    public TreeInfo treeInfo;                   // 树木信息结构体
    public int growthStages;                     // 生长阶段总数
    public int currentGrowthStage = 0;           // 当前的生长阶段

    protected void Start()
    {
        growthStages = Random.Range(treeInfo.growthStagesMin, treeInfo.growthStagesMax + 1);
    }

    // 重写SpawnObject方法
    public override IEnumerator SpawnObject()
    {
        yield return base.SpawnObject();

        if (spawnNew == null) yield break;

        // 根据生长阶段逐步缩放树木
        for (int i = 0; i < growthStages; i++)
        {
            if (spawnNew == null) yield break;
            
            float targetScale = (float)(i + 1) * (1f / growthStages);

            // 缩放动画
            for (float t = 0; t <= 0.5f; t += Time.deltaTime)
            {
                float scale = Mathf.Lerp(spawnNew.transform.localScale.y, targetScale, t / 0.5f);
                spawnNew.transform.DOScale(scale, 0.5f);
                yield return null;
            }

            currentGrowthStage = i + 1;
            interactionInfo.completeness++;

            if (i < growthStages - 1)
            {
                yield return new WaitForSeconds(treeInfo.growthStageDuration);
            }
        }
    }

    // 重写Damage方法
    public override bool Damage(float amount, MeleeDirection direction)
    {
        if (!base.Damage(amount, direction)) return false;
        ObjectPoolerManager.GetInstance(treeInfo.woodPrefab, transform.position + Vector3.up, Quaternion.identity, null);
        return true;
    }
}

// 树木信息结构体
[System.Serializable]
public struct TreeInfo
{
    public GameObject woodPrefab;              // 木材预制体
    public float growthStageDuration;          // 生长阶段持续时间
    public int growthStagesMin;                // 生长阶段最小值
    public int growthStagesMax;                // 生长阶段最大值
    
    // 重置树木信息为默认值
    [Button]
    public void ResetTreeInfo()
    {
        this.growthStageDuration = 10;
        this.growthStagesMin = 5;
        this.growthStagesMax = 5;
    }
}
