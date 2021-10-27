using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeBattle : State
{
    public GameObject battleStationAlly, battleStationOpponent;
    public GameObject pokemonOpponent, pokemonAlly;

    public AnimationData battleStationAnimationAlly, battleStationAnimationOpponent,
                         pokemonOpponentAppearAnimation, pokemonAllyAppearAnimation;

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
        pokemonAlly = BattleUI.GetGameObject("PokemonAlly");

        battleStationAnimationAlly = Resources.Load<AnimationData>("Animations/Battle/BattleStationAnimationAlly");
        battleStationAnimationOpponent = Resources.Load<AnimationData>("Animations/Battle/BattleStationAnimationOpponent");
        pokemonOpponentAppearAnimation = Resources.Load<AnimationData>("Animations/Battle/PokemonOpponentAppearAnimation");
        pokemonAllyAppearAnimation = Resources.Load<AnimationData>("Animations/Battle/PokemonAllyAppearAnimation");
    }

    public void Update()
    {
        if(numberFinishedAnimation == 2)
        {
            pokemonAlly.GetComponent<Pokemon>().SetGUIActive();
            pokemonOpponent.GetComponent<Pokemon>().SetGUIActive();
            StateStack.Pop();
            StateStack.Push(new Textbox($"A wild {pokemonOpponent.GetComponent<Pokemon>().GetName()} appear !", Textbox.TargetTextbox.BATTLE_TEXTBOX, () =>
            {
                StateStack.Push(new SelectAction());
            }));
            return;
        }
        if (initAnimation)
            return;

        battleStationAnimationAlly.GenerateTween(battleStationAlly, "BattleStationAlly", (t) =>
        {
            battleStationAlly.transform.position = t.CurrentValue;
            pokemonAlly.GetComponent<Pokemon>().AnimatePokeSpawn();
        }, (t) => numberFinishedAnimation += 1);
        battleStationAnimationOpponent.GenerateTween(battleStationOpponent, "BattleStationOp", (t) =>
        {
            battleStationOpponent.transform.position = t.CurrentValue;
        }, (t) => numberFinishedAnimation += 1);
        initAnimation = true;
    }
}
