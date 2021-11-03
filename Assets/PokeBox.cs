using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeBox : MonoBehaviour
{
    public PokemonDef currentPokemon;
    public SpriteRenderer iconRenderer;
    public TextMesh levelValueText;
    public TextMesh pokeName;
    public UxBar hpBar, expBar;

    public void SetPokemon(PokemonDef pokemon)
    {
        currentPokemon = pokemon;
    }

    public void UpdatePokebox()
    {
        if(currentPokemon == null)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        iconRenderer.sprite = currentPokemon.icon;
        levelValueText.text = currentPokemon.level.ToString();
        pokeName.text = currentPokemon.name;
        hpBar.ChangeValues(currentPokemon.hpCurrent, currentPokemon.currentStats.hp);
        expBar.FullReset(currentPokemon.expCurrentLevel, currentPokemon.totalExpToLevelUp, currentPokemon.currentExp);
    }

    public void OnValidate()
    {
        UpdatePokebox();
    }
}
