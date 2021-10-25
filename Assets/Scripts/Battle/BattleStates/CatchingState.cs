using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchingState : State
{
    private float catchRate;
    private Action callbackOnSuccess, callbackOnFail;
    private PokemonController user, target;
    
    public CatchingState(float catchRate, PokemonController user, PokemonController target, Action callbackOnSuccess = null, Action callbackOnFail = null)
    {
        this.catchRate = catchRate;
        this.callbackOnSuccess = callbackOnSuccess;
        this.callbackOnFail = callbackOnFail;
        this.user = user;
        this.target = target;
    }

    public void End()
    {
        
    }

    public void Init()
    {
        StateStack.Push(new Textbox("You throw a pokeball !", Textbox.TargetTextbox.BATTLE_TEXTBOX));
    }

    public void Update()
    {
        if(DoesItCatch())
        {
            target.pokemon.gameObject.SetActive(false);
            user.CatchOpponentPokemon(target);
            StateStack.Pop();
            StateStack.Push(new Textbox("You caught it !", Textbox.TargetTextbox.BATTLE_TEXTBOX, callbackOnSuccess));
            SoundManager.__instance__.PlayMusic(BattleUI.GetSound("VictoryMusic"));
        }
        else
        {
            StateStack.Pop();
            StateStack.Push(new Textbox("He made it out !", Textbox.TargetTextbox.BATTLE_TEXTBOX, callbackOnFail));
        }
    }

    public bool DoesItCatch()
    {
        var rndPick = UnityEngine.Random.Range(0f, 1f);
        return rndPick < catchRate;

    }
}
