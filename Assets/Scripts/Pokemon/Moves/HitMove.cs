using DigitalRuby.Tween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PokeData/Move/Hit")]
public class HitMove : Move
{
    public int damage;

    public override void Play(Pokemon defender, Pokemon attacker)
    {
        var typeChart = Resources.Load<TypeChart>("Data/Battle/BasicTypeChart");
        var effectiveMultiplier = typeChart.GetMultiplier(pokemonType, defender.GetPokemonTypes()[0]);
        Debug.Log($"Effectiveness {effectiveMultiplier}");
        var startHp = defender.GetHp();
        defender.InflictDamage(damage, attacker.def, attqType, effectiveMultiplier);
        var endHp = defender.GetHp();
        SoundManager.__instance__.PlaySoundEffect(BattleUI.GetSound("DamageSound"));
        // TODO : Maybe use a factory
        Action<ITween<float>> callbackBlinking = (t) => { defender.SetAlpha(t.CurrentValue); };
        Action<ITween<float>> tweenHpCallback = (t) => { defender.UpdateHpBar(t.CurrentValue); };
        defender.gameObject.Tween("hpDefend", startHp, endHp, 0.4f, TweenScaleFunctions.SineEaseIn, tweenHpCallback, (t) =>
        {
            StateStack.Pop();
        });
        defender.gameObject.Tween("Blink", 1f, 0f, 0.1f, TweenScaleFunctions.Linear, callbackBlinking).
            ContinueWith(new FloatTween().Setup(0f, 1f, 0.1f, TweenScaleFunctions.Linear, callbackBlinking)).
            ContinueWith(new FloatTween().Setup(1f, 0f, 0.1f, TweenScaleFunctions.Linear, callbackBlinking)).
            ContinueWith(new FloatTween().Setup(0f, 1f, 0.1f, TweenScaleFunctions.Linear, callbackBlinking));
    }
}
