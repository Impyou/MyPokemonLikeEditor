using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMoveSelection
{
    Pokemon ownedPokemon, opponentPokemon;
    public IAMoveSelection(Pokemon ownedPokemon, Pokemon opponentPokemon)
    {
        this.ownedPokemon = ownedPokemon;
        this.opponentPokemon = opponentPokemon;
    }

    // Return a moveId
    public int ChooseAttack()
    {
        var moveIds = ownedPokemon.GetMoveIds();
        var rnd = Random.Range(0, moveIds.Length);
        return moveIds[rnd];
    }
}
