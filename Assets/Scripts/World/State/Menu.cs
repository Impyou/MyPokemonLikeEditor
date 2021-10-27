using DigitalRuby.Tween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : State
{
    public GameObject menuEffectObj;
    public Material menuEffectMat;
    public PauseMenuData pauseMenuData;
    public bool quitting;
    public bool isMenuInit;

    public void End()
    {
    }

    public void Init()
    {
        menuEffectMat = WorldUI.Get<SpriteRenderer>("MenuEffect").material;
        menuEffectObj = WorldUI.GetGameObject("MenuEffect");

        pauseMenuData = WorldUI.Get<PauseMenuData>("PauseMenu");

        var waitState = new WaitForAnimation();
        var lockMenu = waitState.GetNewLock();
        var lockPokemonMenu = waitState.GetNewLock();
        var lockPokemonBotMenu = waitState.GetNewLock();

        Action<ITween<float>> updateEffect = (t) => { menuEffectMat.SetFloat("FadeValue", t.CurrentValue); };
        menuEffectObj.Tween("MenuEffectDown", 0f, 3f, 0.5f, TweenScaleFunctions.Linear, updateEffect, (t)=> { lockMenu.Open(); });

        pauseMenuData.topMenuAnimationData.GenerateTween(pauseMenuData.topMenu, "PokemonMenuAnimate", 
        (t) => pauseMenuData.topMenu.transform.position = t.CurrentValue, 
        (t) => lockPokemonMenu.Open());

        pauseMenuData.botMenuAnimationData.GenerateTween(pauseMenuData.botMenu, "PokemonBotMenuAnimate",
        (t) => pauseMenuData.botMenu.transform.position = t.CurrentValue,
        (t) => lockPokemonBotMenu.Open());

        StateStack.Push(waitState);
    }

    public void Update()
    {
        if (quitting)
            return;

        if(!isMenuInit)
        {
            pauseMenuData.InitMenu();
            isMenuInit = true;
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            quitting = true;
            Action<ITween<float>> updateEffect = (t) => { menuEffectMat.SetFloat("FadeValue", t.CurrentValue); };
            menuEffectObj.Tween("MenuEffectDown", 3f, 0f, 0.5f, TweenScaleFunctions.Linear, updateEffect, (t) => {
                StateStack.Pop();
            });
            pauseMenuData.topMenuAnimationData.GenerateReverseTween(pauseMenuData.topMenu, "PokemonMenuAnimate",
            (t) => pauseMenuData.topMenu.transform.position = t.CurrentValue);
            pauseMenuData.botMenuAnimationData.GenerateReverseTween(pauseMenuData.botMenu, "PokemonBotMenuAnimate",
            (t) => pauseMenuData.botMenu.transform.position = t.CurrentValue);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pauseMenuData.Move(Direction.LEFT);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            pauseMenuData.Move(Direction.RIGHT);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pauseMenuData.Move(Direction.UP);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            pauseMenuData.Move(Direction.DOWN);
        }
    }
}
