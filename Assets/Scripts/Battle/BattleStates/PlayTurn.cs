using DigitalRuby.Tween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTurn : State
{
    Pokemon attacker;
    Pokemon defender;
    Move move;

    Action callback;

    bool isPlaying = false;

    public PlayTurn(Pokemon attacker, Pokemon defender, Move move, Action callback)
    {
        this.attacker = attacker;
        this.defender = defender;
        this.move = move;

        this.callback = callback;

        
    }

    public void End()
    {
        if(callback != null)
            callback.Invoke();
    }

    public void Init()
    {
        if (attacker.GetHp() == 0 || defender.GetHp() == 0)
            StateStack.Pop();
        else
            StateStack.Push(new Textbox($"{attacker.def.name} is using {move.name}", Textbox.TargetTextbox.BATTLE_TEXTBOX));
    }


    public void Update()
    {
        if (isPlaying)
            return;
        move.Play(defender, attacker);
        isPlaying = true;
    }
}
