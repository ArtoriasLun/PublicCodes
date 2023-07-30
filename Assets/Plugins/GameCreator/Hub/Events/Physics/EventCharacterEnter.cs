using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using GameCreator.Runtime.Characters;
using UnityEngine;

[Version(1, 0, 0)]

[Title("On Character Enter")]
[Category("Physics/On Character Enter")]
[Description("Executed when a character enters the Trigger collider")]

[Image(typeof(IconPlayer), ColorTheme.Type.Green)]
[Keywords("Pass", "Through", "Touch", "Collision", "Collide", "Enter")]

[Serializable]
public class EventCharacterEnter : GameCreator.Runtime.VisualScripting.Event
{
    public override bool RequiresCollider => true;

    protected override void OnAwake(Trigger trigger)
    {
        base.OnAwake(trigger);
        trigger.RequireRigidbody();
    }

    protected override void OnTriggerEnter3D(Trigger trigger, Collider collider)
    {
        base.OnTriggerEnter3D(trigger, collider);

        if (collider.gameObject.GetComponent<Character>() == null) return;
        _ = this.m_Trigger.Execute(collider.gameObject);
        GetTriggerPoint(trigger, collider);
    }

    protected override void OnCollisionEnter3D(Trigger trigger, Collision collider)
    {
        base.OnCollisionEnter3D(trigger, collider);
        if (collider.gameObject.GetComponent<Character>() == null) return;
        _ = this.m_Trigger.Execute(collider.gameObject);
        triggerEnterPoint = collider.contacts[0].point;
    }

    protected virtual void GetTriggerPoint(Trigger trigger, Collider other)
    {
        // 获取两个碰撞体的边界（Bounds）
        Bounds myBounds = trigger.GetComponent<Collider>().bounds;
        Bounds otherBounds = other.bounds;

        // 计算两个碰撞体边界的交点
        Bounds intersectionBounds = new Bounds();
        intersectionBounds.SetMinMax(
            Vector3.Max(myBounds.min, otherBounds.min),
            Vector3.Min(myBounds.max, otherBounds.max)
        );

        // 交点的中心作为近似的触发点
        triggerEnterPoint = intersectionBounds.center;
    }
  
    public Vector3 triggerEnterPoint;
    // public override string Title => $"Trigger in point {base.triggerEnterPoint}";
}
