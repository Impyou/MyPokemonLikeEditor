using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonController : MonoBehaviour
{
    public Pokemon pokemon;
    protected bool isFace;
    protected int[] currentPokemonMoveIds;

    public PokemonParty pokemonParty;
    public int currentPokemonIndex = -1;

    protected void Start()
    {
        
    }

    public void SetPokemon(int partyId)
    {
        currentPokemonIndex = partyId;
        pokemon.def = pokemonParty.party[partyId];
        pokemon.ResetHpBar();
        currentPokemonMoveIds = pokemon.GetMoveIds();
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
        var targetSprite = isFace ? pokemon.def.frontSprite : pokemon.def.backSprite;
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
