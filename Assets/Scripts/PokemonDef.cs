using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PokemonDef
{
    public int hpMax;
    public int hpCurrent;
    
    public int attq;
    public int attqSpe;
    public int def;
    public int defSpe;
    public int speed;

    public string nature;
    public string ability;

    public int[] moveIDs;

    public PokemonDef(int hpMax,
                      int hpCurrent,
                      int attq,
                      int attqSpe,
                      int def,
                      int defSpe,
                      int speed,
                      string nature,
                      string ability,
                      int[] moveIDs)
    {
        this.hpMax = hpMax;
        this.hpCurrent = hpCurrent;
        this.attq = attq;
        this.attqSpe = attqSpe;
        this.def = def;
        this.defSpe = defSpe;
        this.speed = speed;
        this.nature = nature;
        this.ability = ability;
        this.moveIDs = moveIDs;
    }
}
