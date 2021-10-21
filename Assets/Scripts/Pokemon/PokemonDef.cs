using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PokemonDef
{
    public string name;

    public int level;
    public int currentExp;
    public int totalExpToLevelUp;

    public int hpCurrent;

    // TODO : Set to non serialized
    public PokemonStats currentStats;
    public PokemonStats effortValueStats;
    public PokemonStats individualValueStats;
    public PokemonDefBase defBase;

    public string nature;
    public string ability;

    public int[] moveIDs;
    public Sprite backSprite;
    public Sprite frontSprite;

    public PokemonDef(string name,
                      int level,
                      PokemonDefBase defBase,
                      PokemonStats IV,
                      PokemonStats EV,
                      string nature,
                      string ability,
                      int[] moveIDs,
                      Sprite backSprite,
                      Sprite frontSprite
                      )
    {
        this.name = name;
        this.level = level;
        this.defBase = defBase;
        this.individualValueStats = IV;
        this.effortValueStats = EV;
        this.nature = nature;
        this.ability = ability;
        this.moveIDs = moveIDs;
        this.backSprite = backSprite;
        this.frontSprite = frontSprite;

        ComputeStats();
        this.hpCurrent = this.currentStats.hp;
    }

    public PokemonDef(PokemonDef original) : this(original.name,
                                                  original.level,
                                                  original.defBase,
                                                  original.individualValueStats,
                                                  original.effortValueStats,
                                                  original.nature,
                                                  original.ability,
                                                  original.moveIDs,
                                                  original.backSprite,
                                                  original.frontSprite)
    {

    }

    public void ComputeStats()
    {
        currentStats.hp = ComputeHpFormula(defBase.v.hp, effortValueStats.hp, individualValueStats.hp);
        currentStats.attq = ComputeIndividualStat(defBase.v.attq, effortValueStats.attq, individualValueStats.attq);
        currentStats.attqSpe = ComputeIndividualStat(defBase.v.attqSpe, effortValueStats.attqSpe, individualValueStats.attqSpe);
        currentStats.def = ComputeIndividualStat(defBase.v.def, effortValueStats.def, individualValueStats.def);
        currentStats.defSpe = ComputeIndividualStat(defBase.v.defSpe, effortValueStats.defSpe, individualValueStats.defSpe);
        currentStats.speed = ComputeIndividualStat(defBase.v.speed, effortValueStats.speed, individualValueStats.speed);
    }


    public int ComputeHpFormula(int baseStat, int ev, int iv)
    {
        return ((2 * baseStat + iv + (ev / 4)) * level + level * 100 + 1000) / 100;
    }

    public int ComputeIndividualStat(int baseStat, int ev, int iv)
    {
        return (2 * baseStat + iv + (ev / 4) ) * level / 100 + 5;
    }

    public void FullHeal()
    {
        hpCurrent = currentStats.hp;
    }

    public bool IsKO()
    {
        return hpCurrent == 0;
    }
}
