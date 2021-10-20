using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAction : State
{
    public SelectArrow selectArrow;
    public SelectAction()
    {
        
    }

    public void End()
    {
    }

    public void Init()
    {
        var actionBar = BattleUI.GetGameObject("ActionBar");
        var player = BattleUI.Get<PokemonController>("ControllerAlly");
        var opponent = BattleUI.Get<PokemonController>("ControllerOpponent");
        actionBar.SetActive(true);

        selectArrow = BattleUI.Get<SelectArrow>("SelectArrow");

        Action<int> NoneCallback = (int i) => { };
        Action<int> SelectMoveAction = (int i) => { StateStack.Push(new SelectMove()); };
        Action<int> RunCallback = (int i) => {
            actionBar.SetActive(false);
            BattleState.GetInstance().info.run = true;
            StateStack.Pop();
        };
        Action<int> CatchCallback = (int i) =>
        {
            actionBar.SetActive(false);
            StateStack.Pop();
            StateStack.Push(new CatchingState(0.5f, player, opponent, callbackOnSuccess: () => { }, callbackOnFail: () =>
              {
                  StateStack.Push(new PlayTurn(opponent.pokemon, player.pokemon, 0, () => { }));
              }));
        };
        Action<int> ChangePokemon = (int i) => {
                actionBar.SetActive(false);
                StateStack.Pop();
                StateStack.Push(new ChangePokemon(player, player.GetNextAlivePokemonIndex(), () => {
                    StateStack.Push(new PlayTurn(opponent.pokemon, player.pokemon, 0, () => { }));
                }));
        };
        selectArrow.Init(new Action<int>[,] { { SelectMoveAction, ChangePokemon } , { CatchCallback, RunCallback } });
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectArrow.MoveCursor(SelectArrow.Direction.RIGHT);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectArrow.MoveCursor(SelectArrow.Direction.LEFT);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectArrow.MoveCursor(SelectArrow.Direction.UP);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectArrow.MoveCursor(SelectArrow.Direction.DOWN);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            selectArrow.Call();
        }
    }
}
