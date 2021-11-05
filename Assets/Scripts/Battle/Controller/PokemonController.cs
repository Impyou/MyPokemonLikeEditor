using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonController : MonoBehaviour
{
    public Pokemon pokemon;
    protected bool isFace;
    protected Move[] currentPokemonMoves;

    public PokemonParty pokemonParty;
    public int currentPokemonIndex = -1;

    protected void Start()
    {
        
    }

    public void SetPokemon(int partyId)
    {
        currentPokemonIndex = partyId;
        var newPokemon = pokemonParty.party[currentPokemonIndex];
        newPokemon.ComputeStats();
        newPokemon.Reset();
        pokemon.def = newPokemon;
        pokemon.ResetUX();
        currentPokemonMoves = pokemon.GetMoves();
        SetPokemonSprite();
    }
        
    public void ChangePokemonToNext()
    {
        currentPokemonIndex = (currentPokemonIndex + 1) % pokemonParty.GetLength();
        SetPokemon(currentPokemonIndex);
    }

    public void SetPokemonSprite()
    {
        var spriteRenderer = pokemon.GetComponent<SpriteRenderer>();
        var targetSprite = isFace ? pokemon.def.GetFrontSprite() : pokemon.def.GetBackSprite();
        spriteRenderer.sprite = targetSprite; 
    }

    public void SetNextAlivePokemon()
    {
        SetPokemon(GetNextAlivePokemonIndex());
    }

    public int GetNextAlivePokemonIndex()
    {
        for (int i = (currentPokemonIndex + 1) % pokemonParty.GetLength(); ; i = (i + 1) % pokemonParty.GetLength())
        {
            var pokemonDef = pokemonParty.party[i];
            if (!pokemonDef.IsKO())
                return i;
        }
    }

    public void CatchOpponentPokemon(PokemonController target)
    {
        pokemonParty.StealFirst(target.pokemonParty);
    }

    public bool IsKO()
    {
        return pokemonParty.IsPartyKO();
    }

    public bool IsCurrentPokemonKO()
    {
        return pokemon.IsKO();
    }

    public int GetCurrentPokemonIndex() => currentPokemonIndex;

    public string GetName(int index)
    {
        return pokemonParty.party[index].name;
    }

}
