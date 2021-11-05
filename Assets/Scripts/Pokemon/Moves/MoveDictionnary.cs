using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PokeData/MoveDictionnary")]
public class MoveDictionnary : ScriptableObject
{
    public Move[] moves;

    public string GetName(int moveId)
    {
        return moves[moveId].name;
    }

    public Move GetMove(int id)
    {
        return moves[id];
    }
}
