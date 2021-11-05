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
    public Move ChooseAttack()
    {
        var moves = ownedPokemon.GetMoves();
        var rnd = Random.Range(0, moves.Length);
        return moves[rnd];
    }
}
