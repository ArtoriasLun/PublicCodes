using UnityEngine;
using System.Collections.Generic;
using ALUN;
using GameCreator.Runtime.VisualScripting;
using GameCreator.Runtime.Melee;

public class BindPrefabInRange : MonoBehaviour, ILocalUpdatable
{
    [System.Serializable]
    public class PrefabBinding
    {
        public GameObject targetObject; // 目标物体
        public GameObject prefab; // 预制体
        public GameObject instantiatedPrefab; // 实例化的预制体

        public PrefabBinding(GameObject targetObject, GameObject prefab, GameObject instantiatedPrefab)
        {
            this.targetObject = targetObject;
            this.prefab = prefab;
            this.instantiatedPrefab = instantiatedPrefab;
        }
    }

    public float range = 5f; // 范围半径
    public List<PrefabBinding> bindings = new List<PrefabBinding>(); // 预制体绑定列表
    public List<Hotspot> checks = new List<Hotspot>(); // 检查列表
    public Transform parent;
    public GameObject prefab;
    public bool UpdateObject()
    {
        checks = GetChecks();
        for (int i = 0; i < checks.Count; i++)
        {
            PrefabBinding binding = bindings.Find(x => x.targetObject == checks[i].gameObject);
            if (binding == null)
                bindings.Add(new PrefabBinding(checks[i].gameObject, prefab, null));
        }

        for (int i = 0; i < bindings.Count; i++)
        {
            // 计算目标物体与当前对象之间的距离
            float distance = Vector3.Distance(transform.position, bindings[i].targetObject.transform.position);

            // 如果目标物体在范围内且没有绑定预制体，则创建预制体实例并进行绑定
            if (distance <= range)
            {
                if (bindings[i].instantiatedPrefab == null)
                    bindings[i].instantiatedPrefab = ObjectPoolerManager.GetInstance(bindings[i].prefab, bindings[i].targetObject.transform.position, Quaternion.identity, parent);
                else
                    bindings[i].instantiatedPrefab.transform.position = bindings[i].targetObject.transform.position;
            }
            // 如果目标物体超出范围且已绑定预制体，则销毁预制体实例并解除绑定
            else if (distance > range && bindings[i].instantiatedPrefab != null)
            {
                ObjectPoolerManager.ReleaseInstance(bindings[i].instantiatedPrefab);
                bindings[i].instantiatedPrefab = null;
            }
        }
        return true;
    }
    public List<Hotspot> GetChecks()
    {
        List<Hotspot> foundObjects = new List<Hotspot>();

        // 使用 Physics.OverlapSphere 获取范围内的所有碰撞体
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);

        // 对每个碰撞体进行检查，看它们是否有组件 T
        foreach (Collider collider in colliders)
        {
            // if (collider.GetComponentInChildren<Striker>() == null) continue;
            // 获取 GameObject 上的 T 组件
            var component = collider.GetComponentInChildren<Hotspot>();
            if (component == null) continue;
            // 如果组件存在并且不在列表中，则添加到列表中
            if (!foundObjects.Contains(component))
            {
                foundObjects.Add(component);
            }
        }

        return foundObjects;
    }
}
