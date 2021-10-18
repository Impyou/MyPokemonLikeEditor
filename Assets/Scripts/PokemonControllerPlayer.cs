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
    }

    public void Update()
    {
        
    }

}
