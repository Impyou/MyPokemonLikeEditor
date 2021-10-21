using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : State
{
    PokemonDef pokemonAlly, pokemonOpponent;
    int rewardExp;

    public Reward(PokemonDef pokemonAlly, PokemonDef pokemonOpponent)
    {
        this.pokemonAlly = pokemonAlly;
        this.pokemonOpponent = pokemonOpponent;
    }

    public void End()
    {

    }

    public void Init()
    {
        rewardExp = pokemonOpponent.GetExpValue();
        StateStack.Push(new Textbox($"{pokemonAlly.name} gain {rewardExp} exp !", Textbox.TargetTextbox.BATTLE_TEXTBOX));
    }

    public void Update()
    {
        if(rewardExp == 0)
        {
            StateStack.Pop();
            return;
        }
        rewardExp = pokemonAlly.GainExp(rewardExp);
        Debug.Log(rewardExp);
        if(pokemonAlly.HasLevelUpAndResetIt())
        {
            Debug.Log("Textbox");
            pokemonAlly.LevelUp();
            StateStack.Push(new Textbox($"{pokemonAlly.name} grew to level {pokemonAlly.level}", Textbox.TargetTextbox.BATTLE_TEXTBOX));
        }
    }
}
