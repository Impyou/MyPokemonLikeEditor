using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EffectiveLevel { NotEffective = 0, NotVeryEffective = 50, Effective = 100, SuperEffective = 200};
[CreateAssetMenu(menuName = "PokeData/TypeChart")]
public class TypeChart : ScriptableObject
{
    public int[] damageMultiplier;

    public void Reset()
    {
        int typeNumber = Enum.GetNames(typeof(PokemonType)).Length;
        if (damageMultiplier.Length != typeNumber * typeNumber)
            return;

        damageMultiplier = new int[typeNumber * typeNumber];
        for (int i = 0;i < typeNumber;i++)
        {
            for (int j = 0; j < typeNumber; j++)
            {
                SetMultiplier((PokemonType)i, (PokemonType)j, (int)EffectiveLevel.Effective);
            }
        }
    }

    public int GetMultiplier(PokemonType inputType, PokemonType outputType)
    {
        int typeNumber = Enum.GetNames(typeof(PokemonType)).Length;
        return damageMultiplier[(int)inputType + (int)outputType * typeNumber];
    }

    public void SetMultiplier(PokemonType inputType, PokemonType outputType, int multiplier)
    {
        int typeNumber = Enum.GetNames(typeof(PokemonType)).Length;
        damageMultiplier[(int)inputType + (int)outputType * typeNumber] = multiplier;
    }

    public void RollValue(PokemonType inputType, PokemonType outputType)
    {
        var typeList = Enum.GetValues(typeof(EffectiveLevel)).Cast<EffectiveLevel>();
        var currentMultiplier = GetMultiplier(inputType, outputType);
        for(int index = 0; index < typeList.Count();index++)
        {
            if((int)typeList.ElementAt(index) == currentMultiplier)
            {
                var newMultiplier = (int)typeList.ElementAt((index + 1) % typeList.Count());
                SetMultiplier(inputType, outputType, newMultiplier);
                break;
            }
        }

    }
}
