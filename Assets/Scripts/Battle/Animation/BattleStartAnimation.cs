using DigitalRuby.Tween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStartAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float flickerTime = 0.2f;
    Color startColor;
    Color endColor;

    Action<ITween<Color>> updateColor;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        updateColor = (t) => { spriteRenderer.color = t.CurrentValue; };
    }

    public void StartAnimate(Action<ITween<Color>> callback)
    {
        gameObject.Tween("StartBattle", startColor, endColor, flickerTime, TweenScaleFunctions.Linear, updateColor, (t) => { }).
            ContinueWith(new ColorTween().Setup(endColor, startColor, flickerTime, TweenScaleFunctions.Linear, updateColor, (t) => { })).
            ContinueWith(new ColorTween().Setup(startColor, endColor, flickerTime, TweenScaleFunctions.Linear, updateColor, callback));
    }

    public void EndAnimate()
    {
        gameObject.Tween("StartBattle", endColor, startColor, flickerTime, TweenScaleFunctions.Linear, updateColor, (t) => { });
    }
}
