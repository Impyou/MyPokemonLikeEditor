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
}
