using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType {ATTQ, ATTQ_SPE, DEF, DEF_SPE, SPEED, ACCURACY, EVASINESS};
public enum StatTarget { ATTACKER, DEFENDER};

[Serializable]
public class StatModifier
{
    [HideInInspector]
    public string name;
    public StatTarget target;
    public StatType type;
    public int level;

    public string GetSign()
    {
        return level > 0 ? "+" : "-";
    }
}

[CreateAssetMenu(menuName = "PokeData/Move/StatModifier")]
public class StatModifierMove : Move
{
    public List<StatModifier> modifiers;

    public override void Play(Pokemon defender, Pokemon attacker)
    {
        foreach(var modifier in modifiers)
        {
            switch(modifier.target)
            {
                case StatTarget.ATTACKER:
                    attacker.ApplyModifier(modifier);
                    break;
                case StatTarget.DEFENDER:
                    defender.ApplyModifier(modifier);
                    break;
            }
        }
        StateStack.Pop();
    }

    public void OnValidate()
    {
        foreach(var modifier in modifiers)
        {
            modifier.name = $"{modifier.target} : {modifier.type} {modifier.GetSign()} {Mathf.Abs(modifier.level)}";
        }
    }
}
