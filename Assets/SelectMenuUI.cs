using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenuUI : MonoBehaviour
{
    private PokemonParty party;
    public List<GameObject> pokeBoxes;

    public Vector2 startPos;
    public float widthSpace, heightSpace;

    public void CreateBoxes()
    {
        foreach(var pokemon in party.party)
        {
            
        }
    }

    public void OnValidate()
    {
        PositionBoxes();
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
