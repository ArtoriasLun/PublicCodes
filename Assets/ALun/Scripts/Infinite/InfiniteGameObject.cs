using System.Collections;
using System.Collections.Generic;
using GameCreator.Runtime.Melee;
using MapMagic.Terrains;
using UnityEngine;
using ALUN;
using AdvancedCullingSystem.DynamicCullingCore;

// 无尽游戏对象类
public class InfiniteGameObject : InteractionGameObject, ILocalUpdatable
{
    // public InfiniteGameObjectManager manager;           // 无尽游戏对象管理器
    public List<GameObject> objectPrefabs;              // 游戏对象预制体列表
    public float initialDelay;                          // 初始延迟时间
    public bool spawning, firstSpawn = true;

    // protected virtual void Start()
    // {
    //     manager = GetComponentInParent<TerrainTile>().GetComponentInChildren<InfiniteGameObjectManager>();
    //     if (manager == null)
    //         manager = GetComponent<InfiniteGameObjectManager>();
    //     if (manager == null) return;
    //     manager.AddObject(this);
    // }

    // 生成游戏对象
    public void Spawn()
    {
        if (spawning) return;
        spawning = true;
        if (firstSpawn)
        {
            spawnNew = ObjectPoolerManager.GetInstance(objectPrefabs[Random.Range(0, objectPrefabs.Count)], transform.position, Quaternion.identity, transform);
            AddObjectsForCullingExceptImpostors(spawnNew);
            firstSpawn = false;
        }
        else if (spawnNew == null) StartCoroutine(SpawnObject());
    }

    public override bool Damage(float amount, MeleeDirection direction)
    {
        if (spawnNew == null) return false;
        if (!base.Damage(amount, direction)) return false;
        spawning = false;
        firstSpawn = false;
        return true;
    }

    // 生成游戏对象的协程
    public virtual IEnumerator SpawnObject()
    {
        yield return new WaitForSeconds(initialDelay);

        if (spawnNew == null)
        {
            spawnNew = ObjectPoolerManager.GetInstance(objectPrefabs[Random.Range(0, objectPrefabs.Count)], transform.position, Quaternion.identity, transform);
            AddObjectsForCullingExceptImpostors(spawnNew);
            // MeshRenderer[] meshRenderers = spawnNew.GetComponentsInChildren<MeshRenderer>();
            // DynamicCulling.Instance.AddObjectsForCulling(meshRenderers);
        }
    }

    // 添加需要剔除的游戏对象，排除伪装物体
    void AddObjectsForCullingExceptImpostors(GameObject parentObject)
    {
        MeshRenderer[] meshRenderers = parentObject.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in meshRenderers)
        {
            if (!renderer.gameObject.name.ToLower().Contains("impostor"))
            {
                DynamicCulling.Instance.AddObjectForCulling(renderer);
                // Debug.Log("AddObjectForCulling: " + renderer.gameObject.name);
            }
        }
    }

    public bool UpdateObject()
    {
        if (spawning) return false;
        Spawn();
        return true;
    }
}
