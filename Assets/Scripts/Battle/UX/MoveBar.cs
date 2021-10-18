using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBar : MonoBehaviour
{
    public TextMesh[] Moves;

    public void SetMoves(string[] names)
    {
        for(int i = 0; i < Moves.Length; i++)
        {
            if (i >= names.Length)
                Moves[i].text = "";
            else
                Moves[i].text = names[i];
        }
    }
}
