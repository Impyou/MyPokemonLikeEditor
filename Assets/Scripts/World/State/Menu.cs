using DigitalRuby.Tween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : State
{
    public GameObject menuEffectObj;
    public Material menuEffectMat;

    public void End()
    {
    }

    public void Init()
    {
        menuEffectMat = WorldUI.Get<SpriteRenderer>("MenuEffect").material;
        menuEffectObj = WorldUI.GetGameObject("MenuEffect");

        var waitState = new WaitForAnimation();
        var lockMenu = waitState.GetNewLock();

        Action<ITween<float>> updateEffect = (t) => { menuEffectMat.SetFloat("FadeValue", t.CurrentValue); };
        menuEffectObj.Tween("MenuEffectDown", 0f, 3f, 0.5f, TweenScaleFunctions.Linear, updateEffect, (t)=> { lockMenu.Open(); });
        
        StateStack.Push(waitState);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Action<ITween<float>> updateEffect = (t) => { menuEffectMat.SetFloat("FadeValue", t.CurrentValue); };
            menuEffectObj.Tween("MenuEffectDown", 3f, 0f, 0.5f, TweenScaleFunctions.Linear, updateEffect, (t) => {
                StateStack.Pop();
            });
        }
    }
}
