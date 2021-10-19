using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonControllerIA : PokemonController
{
    public PokemonControllerIA() : base()
    {
        isFace = true;
    }

    public new void Start()
    {
        pokemonParty = new PokemonParty
        {
            party = new PokemonDef[] { WorldUI.Get<WildPokemonInfo>("WildPokemonInfo").PickPokemon() }
        };

        base.Start();
    }
}
