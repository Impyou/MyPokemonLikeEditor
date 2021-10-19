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
    private Pokemon pokemonAlly, pokemonOpponent;
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

        map = MapUI.GetGameObject("Map");
        pokemonAlly = BattleUI.Get<Pokemon>("PokemonAlly");
        pokemonOpponent = BattleUI.Get<Pokemon>("PokemonOpponent");

        controllerAlly = BattleUI.Get<PokemonController>("ControllerAlly");
        controllerOpponent = BattleUI.Get<PokemonController>("ControllerOpponent");

        controllerAlly.SetPokemon(0);
        controllerOpponent.SetPokemon(0);
    }

    public void UpdateQuitBattle()
    {
        if (sceneUnloading == null)
            sceneUnloading = SceneManager.UnloadSceneAsync("BattleScene");
        if(sceneUnloading.isDone)
        {
            map.SetActive(true);
            StateStack.Pop();
            SoundManager.__instance__.PlayMusic(MapUI.GetSound("RoadMusic"));
        }
    }

    public void Update()
    {
        if(quitBattle)
        {
            UpdateQuitBattle();
            return;
        }

        if (pokemonAlly.getHp() == 0)
            info.die = true;
        else if (pokemonOpponent.getHp() == 0)
            info.kill = true;

        if (info.run)
        {
            StateStack.Push(new Textbox("You run away !", Textbox.TargetTextbox.BATTLE_TEXTBOX));
            quitBattle = true;
            return;
        }
        else if (info.die)
        {
            StateStack.Push(new Textbox("You've been extreminated !", Textbox.TargetTextbox.BATTLE_TEXTBOX));
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
        StateStack.Push(new SelectAction());
    }
}
