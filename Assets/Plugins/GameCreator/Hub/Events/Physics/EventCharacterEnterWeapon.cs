using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using GameCreator.Runtime.Characters;
using UnityEngine;

[Version(1, 0, 0)]

[Title("On Character Enter Weapon")]
[Category("ALun/Physics/On Character Enter Weapon")]

[Image(typeof(IconPlayer), ColorTheme.Type.Green)]

[Serializable]
public class EventCharacterEnterWeapon : EventCharacterEnter
{
    public bool isCheckCharacter = true;
    public string isCheckName = "";
    [SerializeField] PropertyGetGameObject effect;
    protected override void GetTriggerPoint(Trigger trigger, Collider other)
    {
        if (CheckIsSelfCharacter(cld: other)) return;
        base.GetTriggerPoint(trigger, other);
        _ = this.m_Trigger.Execute();
        CreateHitPoint();
    }
    protected override void OnCollisionEnter3D(Trigger trigger, Collision collider)
    {
        if (CheckIsSelfCharacter(cls: collider)) return;
        base.OnCollisionEnter3D(trigger, collider);
        _ = this.m_Trigger.Execute();
        CreateHitPoint();
    }
    //写一个新方法，返回布尔值检测是否碰撞到父级有Character组件
    bool CheckIsSelfCharacter(Collider cld = null, Collision cls = null)
    {



        Character selfCharacter = Self.GetComponentInParent<Character>();
        Character targetCharacter = null;
        if (cld != null)
        {
            if (!string.IsNullOrEmpty(isCheckName) && cld.gameObject != selfCharacter.gameObject && cld.gameObject.name.ToLower().Contains(isCheckName.ToLower())) return true;
            targetCharacter = cld.gameObject.GetComponentInParent<Character>();
        }
        if (cls != null)
        {
            if (!string.IsNullOrEmpty(isCheckName) && cls.gameObject != selfCharacter.gameObject && cls.gameObject.name.ToLower().Contains(isCheckName.ToLower())) return true;
            targetCharacter = cls.gameObject.GetComponentInParent<Character>();
        }

        if (selfCharacter != null && targetCharacter != null && selfCharacter == targetCharacter)
            return true;
        return false;
    }

    void CreateHitPoint()
    {
        // 创建一个球体
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.GetComponent<Collider>().enabled = false;
        sphere.transform.position = triggerEnterPoint;
        sphere.transform.localScale = Vector3.one * 0.2f;
        GameObject.Destroy(sphere, 3f);
    }
}
