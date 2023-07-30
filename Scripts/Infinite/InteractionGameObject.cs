using UnityEngine;
using GameCreator.Runtime.Melee;
using Sirenix.OdinInspector;

// 交互游戏对象类
public class InteractionGameObject : MonoBehaviour, IDamageable
{
    public InteractionInfo interactionInfo = new InteractionInfo(1);  // 交互信息
    public GameObject prefab, spawnNew, destroyEffect;                // 预制体
    public Vector3 prefabRot = Vector3.zero, prefabScale = Vector3.one; // 预制体的旋转和缩放
    [Button]
    protected void LoadEffects()
    {
        destroyEffect = Resources.Load<GameObject>("Effects/Destroy");
        // defaultSelf = gameObject;
    }
    private void OnEnable()
    {
        interactionInfo.completeness = interactionInfo.defaultCompleteness;
    }
    public virtual bool Damage(float amount, MeleeDirection direction)
    {
        if (interactionInfo.needDirection[0] != MeleeDirection.Forward)
        {
            bool isRight = false;
            foreach (MeleeDirection d in interactionInfo.needDirection)
            {
                if (d == direction)
                {
                    isRight = true;
                    break;
                }
            }
            if (!isRight) return false;
        }

        // 递减 completeness
        interactionInfo.completeness -= amount;

        // 检查 completeness 是否小于等于 0
        if (interactionInfo.completeness <= 0)
        {
            if (prefab != null)
            {
                interactionInfo.completeness = 9999;
                GameObject sp = ObjectPoolerManager.GetInstance(prefab, transform.position + interactionInfo.positionOffset, Quaternion.identity, null);
                sp.transform.localScale = prefabScale;
                sp.transform.eulerAngles = prefabRot;
            }
            if (interactionInfo.willBeRelease)
            {
                if (GetComponentsInChildren<SmokeWrapper>() != null)
                {
                    foreach (SmokeWrapper sw in GetComponentsInChildren<SmokeWrapper>())
                    {
                        sw.Play();
                    }
                }

                // 播放 destroy 效果
                if (destroyEffect != null)
                    ObjectPoolerManager.GetInstance(destroyEffect, transform.position, Quaternion.identity, null);

                if (spawnNew.GetComponent<PooledObject>() != null)
                    ObjectPoolerManager.ReleaseInstance(spawnNew);
                else
                    Destroy(spawnNew);
                spawnNew = null;
            }
            return true;  // 返回 true 表示实例已经损坏
        }
        return false;  // 返回 false 表示实例未损坏
    }
}

// 交互信息结构体
[System.Serializable]
public struct InteractionInfo
{
    public float completeness;                // 完整度
    public float defaultCompleteness;         // 默认完整度
    public MeleeDirection[] needDirection;    // 需要的攻击方向
    public bool willBeRelease;                // 是否会被释放
    public Vector3 positionOffset;            // 位置偏移

    public InteractionInfo(float completeness)
    {
        this.completeness = completeness;
        this.willBeRelease = true;
        this.defaultCompleteness = 1;
        this.positionOffset = Vector3.up * 0.5f;
        this.needDirection = new MeleeDirection[] { MeleeDirection.Forward };
    }
}
