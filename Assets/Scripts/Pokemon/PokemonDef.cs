using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PokemonDef
{
    public string name;
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
    public Sprite backSprite;
    public Sprite frontSprite;

    public PokemonDef(string name,
                      int hpMax,
                      int hpCurrent,
                      int attq,
                      int attqSpe,
                      int def,
                      int defSpe,
                      int speed,
                      string nature,
                      string ability,
                      int[] moveIDs,
                      Sprite backSprite,
                      Sprite frontSprite
                      )
    {
        this.name = name;
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
        this.backSprite = backSprite;
        this.frontSprite = frontSprite;
    }

    public PokemonDef(PokemonDef original) : this(original.name, 
                                                  original.hpMax,
                                                  original.hpCurrent,
                                                  original.attq,
                                                  original.attqSpe,
                                                  original.def,
                                                  original.defSpe,
                                                  original.speed,
                                                  original.nature,
                                                  original.ability,
                                                  original.moveIDs,
                                                  original.backSprite,
                                                  original.frontSprite)
    {

    }
    public void FullHeal()
    {
        hpCurrent = hpMax;
    }
}
