using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PokemonControllerPlayer : PokemonController
{
    public SelectArrow selectArrow;
    public MoveBar moveBar;

    MoveDictionnary moveDictionnary;

    public PokemonControllerPlayer(): base()
    {
        isFace = false;
    }

    public void Start()
    {
        base.Start();
        moveDictionnary = Resources.Load<MoveDictionnary>("PokemonMove");
        SetMoveName();
    }

    public void Update()
    {
        
    }

    void SetMoveName()
    {
        string[] moveNames = new string[currentPokemonMoveIds.Length];
        foreach(var item in currentPokemonMoveIds.Select((value, i) => (value, i)))
        {
            var name = moveDictionnary.GetName(item.value);
            moveNames[item.i] = name;
        }
        moveBar.SetMoves(moveNames);
    }
}
