using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeBattle : State
{
    public GameObject battleStationAlly;
    public GameObject battleStationOpponent;
    public GameObject pokemonOpponent;

    public AnimationData battleStationAnimationAlly, battleStationAnimationOpponent;
    public WaitForAnimation waitForAnimation;

    public int numberFinishedAnimation;
    public bool initAnimation;

    public InitializeBattle()
    {
        
    }

    public void End()
    {
    }

    public void Init()
    {
        battleStationAlly = BattleUI.GetGameObject("BattleStationAlly");
        battleStationOpponent = BattleUI.GetGameObject("BattleStationOpponent");
        pokemonOpponent = BattleUI.GetGameObject("PokemonOpponent");

        battleStationAnimationAlly = Resources.Load<AnimationData>("Animations/Battle/BattleStationAnimationAlly");
        battleStationAnimationOpponent = Resources.Load<AnimationData>("Animations/Battle/BattleStationAnimationOpponent");
    }

    public void Update()
    {
        if(numberFinishedAnimation == 2)
        {
            StateStack.Pop();
            return;
        }
        if (initAnimation)
            return;

        battleStationAnimationAlly.GenerateTween(battleStationAlly, "BattleStationAlly", (t) =>
        {
            Debug.Log(t.CurrentValue);
            battleStationAlly.transform.position = t.CurrentValue;
        }, (t) => numberFinishedAnimation += 1);
        battleStationAnimationOpponent.GenerateTween(battleStationOpponent, "BattleStationOp", (t) =>
        {
            battleStationOpponent.transform.position = t.CurrentValue;
        }, (t) => numberFinishedAnimation += 1);

        initAnimation = true;
    }
}
