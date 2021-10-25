using DigitalRuby.Tween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : State
{
    PokemonDef pokemonAlly, pokemonOpponent;
    Pokemon pokemonAllyUI;
    int rewardExp;
    bool isAnimated;

    public Reward(PokemonDef pokemonAlly, PokemonDef pokemonOpponent)
    {
        this.pokemonAlly = pokemonAlly;
        this.pokemonOpponent = pokemonOpponent;
        pokemonAllyUI = BattleUI.Get<Pokemon>("PokemonAlly");
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
        if (isAnimated)
            return;

        if(rewardExp == 0)
        {
            StateStack.Pop();
            return;
        }
        var currentExp = pokemonAlly.currentExp;
        var expGainSummary = pokemonAlly.GainExp(rewardExp);
        var nextExp = pokemonAlly.currentExp;
        rewardExp = expGainSummary.remainingExp;

        isAnimated = true;

        Action<ITween<float>> tweenExpCallback = (t) => { pokemonAllyUI.UpdateExpBar(t.CurrentValue); };
        Debug.Log($"current : {currentExp}, next : {nextExp}");
        SoundManager.__instance__.PlaySoundEffect(BattleUI.GetSound("GainExp"));
        pokemonAllyUI.gameObject.Tween("UpdateExp", currentExp, nextExp, 0.01f * expGainSummary.expPercent, TweenScaleFunctions.SineEaseIn, tweenExpCallback, (t) =>
        {
            SoundManager.__instance__.StopSoundEffect();
            isAnimated = false;
            if (pokemonAlly.HasLevelUpAndResetIt())
            {
                pokemonAlly.LevelUp();
                pokemonAlly.ComputeStats();

                SoundManager.__instance__.PlaySoundEffect(BattleUI.GetSound("LevelUp"));
                StateStack.Push(new Textbox($"{pokemonAlly.name} grew to level {pokemonAlly.level}", Textbox.TargetTextbox.BATTLE_TEXTBOX, () => {
                    pokemonAllyUI.SetExpBarValues(pokemonAlly.expCurrentLevel, pokemonAlly.totalExpToLevelUp, pokemonAlly.expCurrentLevel);
            }));
            }
        });
    }
}
