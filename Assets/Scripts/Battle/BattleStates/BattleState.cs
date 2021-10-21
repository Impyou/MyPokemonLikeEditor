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
        StateStack.Push(new SelectAction());

        map = WorldUI.GetGameObject("Map");

        controllerAlly = BattleUI.Get<PokemonController>("ControllerAlly");
        controllerOpponent = BattleUI.Get<PokemonController>("ControllerOpponent");

        controllerAlly.SetNextAlivePokemon();
        controllerOpponent.SetNextAlivePokemon();
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
        {
            if(controllerOpponent.IsKO())
                info.kill = true;
            StateStack.Push(new Reward(controllerAlly.pokemon.def, controllerOpponent.pokemon.def));
        }

        if (info.kill || info.run || info.die || quitBattle)
            return;

        StateStack.Push(new SelectAction());
    }
}
