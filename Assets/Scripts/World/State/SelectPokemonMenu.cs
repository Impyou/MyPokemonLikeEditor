using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPokemonMenu : State
{

    SelectMenuUI selectPokemonUI;
    PokemonParty party;

    public SelectPokemonMenu(PokemonParty party)
    {
        this.party = party;
    }

    public void End()
    {
        selectPokemonUI.gameObject.SetActive(false);
    }

    public void Init()
    {
        selectPokemonUI = WorldUI.Get<SelectMenuUI>("SelectPokemonMenu");
        selectPokemonUI.SetParty(party);
        selectPokemonUI.UpdateBoxes();
        selectPokemonUI.gameObject.SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            StateStack.Pop();
    }
}
