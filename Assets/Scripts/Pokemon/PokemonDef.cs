using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PokemonStatsModifiers
{
    public int attq;
    public int attqSpe;
    public int def;
    public int defSpe;
    public int speed;
    public int accuracy;
    public int evasiness;

    public void Reset()
    {
        attq = 0;
        attqSpe = 0;
        def = 0;
        defSpe = 0;
        speed = 0;
        accuracy = 0;
        evasiness = 0;
    }
}

[Serializable]
public class PokemonDef
{
    public string name;

    public int level;
    public int currentExp;
    public int totalExpToLevelUp;
    public int expCurrentLevel;

    public int hpCurrent;

    // TODO : Set to non serialized
    public PokemonStats currentStats;
    public PokemonStats effortValueStats;
    public PokemonStats individualValueStats;
    public PokemonStatsModifiers currentStatsModifiers;
    public PokemonDefBase defBase;

    public string nature;
    public string ability;

    public Move[] moves;

    public bool shouldLevelUp = false;

    public PokemonDef(string name,
                      int level,
                      PokemonDefBase defBase,
                      PokemonStats IV,
                      PokemonStats EV,
                      string nature,
                      string ability,
                      Move[] moves
                      )
    {
        this.name = name;
        this.level = level;
        this.defBase = defBase;
        this.individualValueStats = IV;
        this.effortValueStats = EV;
        this.nature = nature;
        this.ability = ability;
        this.moves = moves;

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
                                                  original.moves)
    {

    }

    public void InitNew()
    {
        ComputeStats();
        FullHeal();
        SetExpToLevel();
    }

    public void Reset()
    {
        currentStatsModifiers.Reset();
    }

    public void ComputeStats()
    {
        currentStats.hp = ComputeHpFormula(defBase.v.hp, effortValueStats.hp, individualValueStats.hp);
        currentStats.attq = ComputeIndividualStat(defBase.v.attq, effortValueStats.attq, individualValueStats.attq);
        currentStats.attqSpe = ComputeIndividualStat(defBase.v.attqSpe, effortValueStats.attqSpe, individualValueStats.attqSpe);
        currentStats.def = ComputeIndividualStat(defBase.v.def, effortValueStats.def, individualValueStats.def);
        currentStats.defSpe = ComputeIndividualStat(defBase.v.defSpe, effortValueStats.defSpe, individualValueStats.defSpe);
        currentStats.speed = ComputeIndividualStat(defBase.v.speed, effortValueStats.speed, individualValueStats.speed);
        totalExpToLevelUp = defBase.GetNeededExpLevel(level + 1);
        expCurrentLevel = defBase.GetNeededExpLevel(level);
    }

    public void ApplyModifier(StatModifier modifier)
    {
        switch(modifier.type)
        {
            case StatType.ATTQ:
                currentStatsModifiers.attq += modifier.level;
                currentStatsModifiers.attq = Mathf.Clamp(currentStatsModifiers.attq, -6, 6);
                break;
            case StatType.ATTQ_SPE:
                currentStatsModifiers.attqSpe += modifier.level;
                currentStatsModifiers.attqSpe = Mathf.Clamp(currentStatsModifiers.attqSpe, -6, 6);
                break;
            case StatType.DEF:
                currentStatsModifiers.def += modifier.level;
                currentStatsModifiers.def = Mathf.Clamp(currentStatsModifiers.def, -6, 6);
                break;
            case StatType.DEF_SPE:
                currentStatsModifiers.defSpe += modifier.level;
                currentStatsModifiers.defSpe = Mathf.Clamp(currentStatsModifiers.defSpe, -6, 6);
                break;
            case StatType.SPEED:
                currentStatsModifiers.speed += modifier.level;
                currentStatsModifiers.speed = Mathf.Clamp(currentStatsModifiers.speed, -6, 6);
                break;
            case StatType.EVASINESS:
                currentStatsModifiers.evasiness += modifier.level;
                currentStatsModifiers.evasiness = Mathf.Clamp(currentStatsModifiers.evasiness, -6, 6);
                break;
            case StatType.ACCURACY:
                currentStatsModifiers.accuracy += modifier.level;
                currentStatsModifiers.accuracy = Mathf.Clamp(currentStatsModifiers.accuracy, -6, 6);
                break;
        }
    }

    public void InflictDamage(int damagePower, PokemonDef attacker, AttqTypes attqType, int effectivenessMultiplier)
    {
        if (attqType == AttqTypes.PHYSICAL)
            InflictPhysicalDamage(damagePower, attacker, effectivenessMultiplier);
        else
            InflictSpecialDamage(damagePower, attacker, effectivenessMultiplier);
    }

    public void InflictPhysicalDamage(int damagePower, PokemonDef attacker, int effectivenessMultiplier)
    {
        var attqMultiplierNom = 2 + (attacker.currentStatsModifiers.attq > 0 ? attacker.currentStatsModifiers.attq : 0);
        var attqMultiplierDenom = 2 - (attacker.currentStatsModifiers.attq < 0 ? attacker.currentStatsModifiers.attq : 0);
        var defMultiplierNom = 2 + (currentStatsModifiers.def > 0 ? currentStatsModifiers.def : 0);
        var defMultiplierDenom = 2 - (currentStatsModifiers.def < 0 ? currentStatsModifiers.def : 0);


        var damage = ((2 * level / 5) + 2) * damagePower * attacker.currentStats.attq * attqMultiplierNom * defMultiplierDenom /
                      (50 * currentStats.def * attqMultiplierDenom * defMultiplierNom) + 2;

        damage = damage * effectivenessMultiplier / 100;
        hpCurrent -= damage;
        hpCurrent = Mathf.Max(hpCurrent, 0);
    }

    public void InflictSpecialDamage(int damagePower, PokemonDef attacker, int effectivenessMultiplier)
    {
        var attqMultiplierNom = 2 + (attacker.currentStatsModifiers.attqSpe > 0 ? attacker.currentStatsModifiers.attqSpe : 0);
        var attqMultiplierDenom = 2 - (attacker.currentStatsModifiers.attqSpe < 0 ? attacker.currentStatsModifiers.attqSpe : 0);
        var defMultiplierNom = 2 + (currentStatsModifiers.defSpe > 0 ? currentStatsModifiers.defSpe : 0);
        var defMultiplierDenom = 2 - (currentStatsModifiers.defSpe < 0 ? currentStatsModifiers.defSpe : 0);


        var damage = ((2 * level / 5) + 2) * damagePower * attacker.currentStats.attqSpe * attqMultiplierNom * defMultiplierDenom /
                      (50 * currentStats.defSpe * attqMultiplierDenom * defMultiplierNom) + 2;
        damage = damage * effectivenessMultiplier / 100;

        hpCurrent -= damage;
        hpCurrent = Mathf.Max(hpCurrent, 0);
    }

    public void Heal(int rate)
    {
        hpCurrent += currentStats.hp * rate / 100;
        hpCurrent = Mathf.Min(hpCurrent, currentStats.hp);
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
        ComputeStats();
        hpCurrent = currentStats.hp;
    }

    public bool IsKO()
    {
        return hpCurrent == 0;
    }

    public struct ExpGainSummary
    {
        public int remainingExp;
        public int expPercent;
    }

    /// <summary>
    /// Add the exp to pokemon until a level is reached 
    /// and return the remainig exp
    /// </summary>
    /// <param name="expPoint"></param>
    /// <returns>remaing exp</returns>
    public ExpGainSummary GainExp(int expPoint)
    {
        int totalLevelExp = defBase.GetNeededExpLevel(level + 1) - defBase.GetNeededExpLevel(level);
        var nextLevelNeededExp = defBase.GetNeededExpLevel(level + 1) - currentExp;
        int remainingExp;
        int expAdded = 0;
        if (expPoint - nextLevelNeededExp < 0)
        {
            expAdded = expPoint;
            remainingExp = 0;
            currentExp += expPoint;
        }
        else
        {
            expAdded = nextLevelNeededExp;
            remainingExp = expPoint - nextLevelNeededExp;
            currentExp += nextLevelNeededExp;
            shouldLevelUp = true;
        }
        var expPercent = expAdded * 100 / totalLevelExp;

        return new ExpGainSummary { remainingExp = remainingExp,
                                    expPercent = expPercent};
    }

    public int GetExpValue()
    {
        return (int)(defBase.expMultiplier * level / 7f);
    }

    public bool HasLevelUpAndResetIt()
    {
        var _shouldLevelUp = shouldLevelUp;
        shouldLevelUp = false;
        return _shouldLevelUp;
    }

    public void LevelUp()
    {
        level += 1;
    }

    public void SetExpToLevel()
    {
        currentExp = defBase.GetNeededExpLevel(level);
    }

    public Sprite GetIcon()
    {
        return defBase.icon;
    }

    public Sprite GetFrontSprite()
    {
        return defBase.frontSprite;
    }

    public Sprite GetBackSprite()
    {
        return defBase.backSprite;
    }

    public PokemonType[] GetPokemonTypes()
    {
        return defBase.types;
    }
}
