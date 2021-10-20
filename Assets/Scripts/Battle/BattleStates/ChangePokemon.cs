using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePokemon : State
{
    PokemonController controller;
    int newPokemonIndex;
    Action callback;

    public ChangePokemon(PokemonController controller, int newPokemonIndex, Action callback)
    {
        this.controller = controller;
        this.newPokemonIndex = newPokemonIndex;
        this.callback = callback;
    }

    public void End()
    {
    }

    public void Init()
    {
        StateStack.Push(new Textbox($"Come back {controller.GetName(controller.GetCurrentPokemonIndex())} !", Textbox.TargetTextbox.BATTLE_TEXTBOX, () =>
        {
            StateStack.Push(new Textbox($"Go {controller.GetName(newPokemonIndex)} !", Textbox.TargetTextbox.BATTLE_TEXTBOX));
        }));
        
    }

    public void Update()
    {
        StateStack.Pop();
        controller.SetPokemon(newPokemonIndex);
        callback.Invoke();
    }
}
