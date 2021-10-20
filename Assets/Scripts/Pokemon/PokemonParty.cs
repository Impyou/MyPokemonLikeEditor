using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PokemonParty
{
    public PokemonDef[] party;

    public void HealParty()
    {
        foreach(var pokemon in party)
        {
            pokemon.FullHeal();
        }
    }

    public int GetLength()
    {
        return party.Length;
    }

    public bool IsPartyKO()
    {
        foreach (var pokemon in party)
        {
            if (pokemon.hpCurrent != 0)
                return false;
        }
        return true;
    }
}
