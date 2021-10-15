using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class MoveDictionnary : ScriptableObject
{
    [Serializable]
    public struct Move
    {
        public string name;
        public int damage;
    }

    public Move[] moves;

    public string GetName(int moveId)
    {
        return moves[moveId].name;
    }
}
