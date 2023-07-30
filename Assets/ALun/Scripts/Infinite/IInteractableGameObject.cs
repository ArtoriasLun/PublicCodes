using System.Collections;
using System.Collections.Generic;
using GameCreator.Runtime.Melee;
using UnityEngine;

public interface IInteractableGameObject
{

}
public interface IDamageable
{
    bool Damage(float amount, MeleeDirection direction);
}

public static class InterfaceUtils
{
    public static bool HasInterface<T>(GameObject obj) where T : class
    {
        return obj.TryGetComponent<T>(out _) || obj.GetComponentInParent<T>() != null;
    }

    public static T GetInterfaceSP<T>(this GameObject obj) where T : class
    {
        return obj.GetComponent<T>() ?? obj.GetComponentInParent<T>();
    }
}



