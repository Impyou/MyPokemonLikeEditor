using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonController : MonoBehaviour
{
    public Pokemon pokemon;
    protected bool isFace;
    protected int[] currentPokemonMoveIds;

    protected void Start()
    {
        currentPokemonMoveIds = pokemon.GetMoveIds();
        SetPokemonSprite();
    }

    public void SetPokemonSprite()
    {
        var spriteRenderer = pokemon.GetComponent<SpriteRenderer>();
        var targetSprite = isFace ? pokemon.faceSprite : pokemon.backSprite;
        spriteRenderer.sprite = targetSprite; 
    }

}
