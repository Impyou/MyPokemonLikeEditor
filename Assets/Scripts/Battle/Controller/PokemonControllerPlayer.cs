using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PokemonControllerPlayer : PokemonController
{
    public MoveBar moveBar;

    MoveDictionnary moveDictionnary;

    public PokemonControllerPlayer(): base()
    {
        isFace = false;
    }

    public new void Start()
    {       
        moveDictionnary = Resources.Load<MoveDictionnary>("PokemonMove");
        pokemonParty = MapUI.Get<Character>("Character").party;

        base.Start();
    }

    public void Update()
    {
        
    }

}
