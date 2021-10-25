using DigitalRuby.Tween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleState : State
{
    private static BattleState __instance__ = null;
    private AsyncOperation sceneUnloading;
    private GameObject map;
    public struct Info
    {
        public bool run;
        public bool die;
        public bool kill;
    }

    public Info info;
    public bool quitBattle;
    private PokemonController controllerAlly, controllerOpponent;

    public static BattleState GetInstance()
    {
        return __instance__;
    }

    public void End()
    {
        __instance__ = null;
    }

    public void Init()
    {
        if (__instance__ != null)
            Debug.LogError("MultipleBattleState instance !!!");

        __instance__ = this;

        map = WorldUI.GetGameObject("Map");

        controllerAlly = BattleUI.Get<PokemonController>("ControllerAlly");
        controllerOpponent = BattleUI.Get<PokemonController>("ControllerOpponent");

        controllerAlly.SetNextAlivePokemon();
        controllerOpponent.SetNextAlivePokemon();

        StateStack.Push(new InitializeBattle());
    }

    public void UpdateQuitBattle()
    {
        if (sceneUnloading == null)
            sceneUnloading = SceneManager.UnloadSceneAsync("BattleScene");
        if(sceneUnloading.isDone)
        {
            map.SetActive(true);
            StateStack.Pop();
            SoundManager.__instance__.PlayMusic(WorldUI.GetSound("RoadMusic"));
        }
    }

    public void Update()
    {
        if(quitBattle)
        {
            UpdateQuitBattle();
            return;
        }
        else if (info.run)
        {
            StateStack.Push(new Textbox("You run away !", Textbox.TargetTextbox.BATTLE_TEXTBOX));
            quitBattle = true;
            return;
        }
        else if (info.die)
        {
            StateStack.Push(new Textbox("You've been exterminated !", Textbox.TargetTextbox.BATTLE_TEXTBOX));
            quitBattle = true;
            return;
        }
        else if (info.kill)
        {
            SoundManager.__instance__.PlayMusic(BattleUI.GetSound("VictoryMusic"));
            StateStack.Push(new Textbox("You won the battle !", Textbox.TargetTextbox.BATTLE_TEXTBOX));
            quitBattle = true;
            return;
        }
        //TODO : rename to pokemon caught
        if (controllerOpponent.pokemonParty.IsEmpty())
        {
            quitBattle = true;
            return;
        }
        if (controllerAlly.IsCurrentPokemonKO())
        {
            //TODO : rename is KO
            if (controllerAlly.IsKO())
                info.die = true;
            else
            {
                StateStack.Push(new ChangePokemon(controllerAlly, controllerAlly.GetNextAlivePokemonIndex(), () => { }));
                return;
            }

        }
        else if (controllerOpponent.IsCurrentPokemonKO())
            OpponentPokemonKO();

        if (info.kill || info.run || info.die || quitBattle)
            return;

        StateStack.Push(new SelectAction());
    }

    public void OpponentPokemonKO()
    {
        if (controllerOpponent.IsKO())
            info.kill = true;

        var pokeRef = controllerOpponent.pokemon;

        var waitAnimationState = new WaitForAnimation();
        var pokeFallLock = waitAnimationState.GetNewLock();
        Action<ITween<float>> pokemonFall = (t) => {
            pokeRef.SetPosition(new Vector2(8.57f, t.CurrentValue));
        };
        pokeRef.gameObject.Tween("Fall", 6.06f, -6.32f, 1f, TweenScaleFunctions.CubicEaseOut, pokemonFall, (t) =>
        {
            pokeFallLock.Open();
        });
        string pokemonName = controllerOpponent.GetName(controllerOpponent.GetCurrentPokemonIndex());
        StateStack.Push(new Textbox($"Wild {pokemonName} has fainted", Textbox.TargetTextbox.BATTLE_TEXTBOX, () =>
        {
            StateStack.Push(new Reward(controllerAlly.pokemon.def, controllerOpponent.pokemon.def));
        }, type : Textbox.TextboxType.ONE_FRAME));
        StateStack.Push(waitAnimationState);
    }
}
