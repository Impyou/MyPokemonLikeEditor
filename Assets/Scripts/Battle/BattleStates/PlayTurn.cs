using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTurn : State
{
    MoveDictionnary moveDictionnary;
    Pokemon attacker;
    Pokemon defender;

    int moveId;

    Move move;
    Action callback;

    public PlayTurn(Pokemon attacker, Pokemon defender, int moveId, Action callback)
    {
        moveDictionnary = Resources.Load<MoveDictionnary>("PokemonMove");
        this.attacker = attacker;
        this.defender = defender;
        this.moveId = moveId;

        move = moveDictionnary.GetMove(this.moveId);
        this.callback = callback;
    }

    public void End()
    {
        if(callback != null)
            callback.Invoke();
    }

    public void Init()
    {
        StateStack.Push(new Textbox($"bulbizare is using {moveDictionnary.GetName(moveId)}", Textbox.TargetTextbox.BATTLE_TEXTBOX));
    }


    public void Update()
    {
        defender.inflictDamage(move.damage);
        SoundManager.__instance__.Play(BattleUI.Play("DamageSound"));
        defender.UpdateHpBar(defender.getHp());
        StateStack.Pop();
        //var hpRate = defender.getHpRate();
    }
}
