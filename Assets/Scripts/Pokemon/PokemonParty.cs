using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PokemonParty
{
    public List<PokemonDef> party;

    public void HealParty()
    {
        foreach(var pokemon in party)
        {
            pokemon.FullHeal();
        }
    }

    public int GetLength()
    {
        return party.Count;
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

    public void AddPokemon(PokemonDef pokemonDef)
    {
        party.Add(pokemonDef);
    }

    public PokemonDef RemovePokemon(int index)
    {
        var pokemonDef = party[index];
        party.RemoveAt(index);
        return pokemonDef;
    }

    public void StealFirst(PokemonParty partyToSteal)
    {
        AddPokemon(partyToSteal.RemovePokemon(0));
    }

    public bool IsEmpty()
    {
        return GetLength() == 0;
    }
}
