using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenuUI : MonoBehaviour
{
    private PokemonParty party;
    public List<GameObject> pokeBoxes;

    public Vector2 startPos;
    public float widthSpace, heightSpace;

    public void SetParty(PokemonParty party)
    {
        this.party = party;
    }

    public void OnValidate()
    {
        PositionBoxes();
    }

    public void UpdateBoxes()
    {
        for(int i = 0;i < pokeBoxes.Count;i++)
        {
            var box = pokeBoxes[i].GetComponent<PokeBox>();

            if (i < party.GetLength())
                box.SetPokemon(party.party[i]);
            else
                box.SetPokemon(null);
            box.UpdatePokebox();
        }
    }

    public void PositionBoxes()
    {
        Vector2 position = startPos;
        for(int i = 0;i < pokeBoxes.Count;i++)
        {
            pokeBoxes[i].transform.position = position;
            if(i % 2 == 0)
            {
                position.x += widthSpace;
            }
            else
            {
                position.y += heightSpace;
                position.x = startPos.x;
            }
        }
    }

}
