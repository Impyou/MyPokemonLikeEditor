using DigitalRuby.Tween;
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

    bool isPlaying = false;

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
        if (attacker.GetHp() == 0 || defender.GetHp() == 0)
            StateStack.Pop();
        else
            StateStack.Push(new Textbox($"{attacker.def.name} is using {moveDictionnary.GetName(moveId)}", Textbox.TargetTextbox.BATTLE_TEXTBOX));
    }


    public void Update()
    {
        if (isPlaying)
            return;

        var startHp = defender.GetHp();
        defender.inflictDamage(move.damage, attacker.def);
        var endHp = defender.GetHp();
        SoundManager.__instance__.PlaySoundEffect(BattleUI.GetSound("DamageSound"));
        // TODO : Maybe use a factory
        Action<ITween<float>> tweenHpCallback = (t) => { defender.UpdateHpBar((int)t.CurrentValue); };
        defender.gameObject.Tween("hpDefend", startHp, endHp, 0.4f, TweenScaleFunctions.SineEaseIn, tweenHpCallback, (t) =>
        {
            StateStack.Pop();
        });

        isPlaying = true;
    }
}
