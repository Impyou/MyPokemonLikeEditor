using DigitalRuby.Tween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PokeData/Move/Healing")]
public class HealingMove : Move
{
    public int healRate;

    public override void Play(Pokemon defender, Pokemon attacker)
    {
        var startHp = attacker.GetHp();
        attacker.Heal(healRate);
        var endHp = attacker.GetHp();
        // TODO : Maybe use a factory
        Action<ITween<float>> tweenHpCallback = (t) => { attacker.UpdateHpBar(t.CurrentValue); };
        attacker.gameObject.Tween("hpDefend", startHp, endHp, 0.4f, TweenScaleFunctions.SineEaseIn, tweenHpCallback, (t) =>
        {
            StateStack.Pop();
        });
    }
}
