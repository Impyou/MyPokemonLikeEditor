using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectMove : State
{
    SelectArrow selectArrowMove;
    MoveBar moveBar;
    MoveDictionnary moveDictionnary;

    public void Init()
    {
        moveDictionnary = Resources.Load<MoveDictionnary>("PokemonMove");

        moveBar = BattleUI.Get<MoveBar>("MoveBar");
        var actionBar = BattleUI.GetGameObject("ActionBar");
        SetMoveName();
        SetUXState(true);

        selectArrowMove = BattleUI.Get<SelectArrow>("SelectArrowMove");
        Action NoneCallback = () => { };
        Action<int>[] AttackCallbacks = new Action<int>[4] { null, null, null, null };
        var pokemonAlly = BattleUI.Get<Pokemon>("PokemonAlly");
        for (int i = 0;i < pokemonAlly.GetMoves().Length; i++)
        {
            AttackCallbacks[i] = (int cursorIndex) => {
                actionBar.SetActive(false);
                StateStack.Pop();
                StateStack.Pop();
                if (cursorIndex < pokemonAlly.GetMoves().Length)
                    PlayMove(pokemonAlly.GetMoves()[cursorIndex]);
            };
        }
        
        selectArrowMove.Init(new Action<int>[,] { { AttackCallbacks[0], AttackCallbacks[2] }, { AttackCallbacks[1], AttackCallbacks[3] } });
        selectArrowMove.canMoveOnNull = false;
    }

    public void PlayMove(Move move)
    {
        var pokemonAlly = BattleUI.Get<Pokemon>("PokemonAlly");
        var pokemonOpponent = BattleUI.Get<Pokemon>("PokemonOpponent");
        IAMoveSelection IA_moveSelection = new IAMoveSelection(pokemonOpponent, pokemonAlly);
        Move IA_move = IA_moveSelection.ChooseAttack();
        

        StateStack.Push(new PlayTurn(pokemonAlly, pokemonOpponent, move,  () => {
            StateStack.Push(new PlayTurn(pokemonOpponent, pokemonAlly, IA_move, () => { })); 
        }));
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectArrowMove.MoveCursor(SelectArrow.Direction.RIGHT);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectArrowMove.MoveCursor(SelectArrow.Direction.LEFT);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectArrowMove.MoveCursor(SelectArrow.Direction.UP);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectArrowMove.MoveCursor(SelectArrow.Direction.DOWN);
        }
        else if(Input.GetKeyDown(KeyCode.Return))
        {
            selectArrowMove.Call();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            StateStack.Pop();
        }
    }

    public void End()
    {
        SetUXState(false);
    }

    public void SetUXState(bool state)
    {
        moveBar.gameObject.SetActive(state);
    }


    void SetMoveName()
    {
        var pokemon = BattleData.Get<Pokemon>("PokemonAlly");
        var moves = pokemon.GetMoves();
        string[] moveNames = new string[moves.Length];

        foreach (var item in moves.Select((value, i) => (value, i)))
        {
            var name = item.value.name;
            moveNames[item.i] = name;
        }
        moveBar.SetMoves(moveNames);
    }
}
