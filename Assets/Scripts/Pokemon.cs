using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    public PokemonDef def;
    public Sprite faceSprite;
    public Sprite backSprite;

    public int[] GetMoveIds()
    {
        return def.moveIDs;
    }
}
