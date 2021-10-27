using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPokemonMenu : State
{

    GameObject selectPokemonMenu;

    public void End()
    {
        selectPokemonMenu.SetActive(false);
    }

    public void Init()
    {
        selectPokemonMenu = WorldUI.GetGameObject("SelectPokemonMenu");
        selectPokemonMenu.SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            StateStack.Pop();
    }
}
