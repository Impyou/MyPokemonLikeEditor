using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonController : MonoBehaviour
{
    public Pokemon pokemon;
    protected bool isFace;
    protected int[] currentPokemonMoveIds;

    public PokemonParty pokemonParty;
    public int currentPokemonId;

    protected void Start()
    {
        
    }

    public void SetPokemon(int partyId)
    {
        pokemon.def = pokemonParty.party[partyId];
        pokemon.ResetHpBar();
        currentPokemonMoveIds = pokemon.GetMoveIds();
        SetPokemonSprite();
    }

    public void ChangePokemonToNext()
    {
        currentPokemonId = (currentPokemonId + 1) % pokemonParty.GetLength();
        SetPokemon(currentPokemonId);
    }

    public void SetPokemonSprite()
    {
        var spriteRenderer = pokemon.GetComponent<SpriteRenderer>();
        var targetSprite = isFace ? pokemon.def.frontSprite : pokemon.def.backSprite;
        spriteRenderer.sprite = targetSprite; 
    }

}
