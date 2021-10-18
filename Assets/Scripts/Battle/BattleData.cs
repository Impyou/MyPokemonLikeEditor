using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleData : MonoBehaviour
{
    private static BattleData __instance__ = null;

    [Serializable]
    public class NameToObject : RotaryHeart.Lib.SerializableDictionary.SerializableDictionaryBase<string, GameObject> { }
    public NameToObject nameToGo;

    public void Awake()
    {
        if (__instance__ != null)
            Debug.LogError("Multiple BattleData instances !!!");

        __instance__ = this;
    }

    public static GameObject GetGameObject(string name)
    {
        return __instance__.nameToGo[name];
    }

    public static T Get<T>(string name)
    {
        return GetGameObject(name).GetComponent<T>();
    }
}
