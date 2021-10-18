using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    private static BattleUI __instance__ = null;

    [Serializable]
    public class NameToObject : RotaryHeart.Lib.SerializableDictionary.SerializableDictionaryBase<string, GameObject> { }
    public NameToObject nameToGo;

    [Serializable]
    public class NameToSound : RotaryHeart.Lib.SerializableDictionary.SerializableDictionaryBase<string, AudioClip> { }
    public NameToSound nameToSound;

    public void Awake()
    {
        if (__instance__ != null)
            Debug.LogError("Multiple BattleUI instances !!!");

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

    public static AudioClip Play(string name)
    {
        return __instance__.nameToSound[name];
    }

}
