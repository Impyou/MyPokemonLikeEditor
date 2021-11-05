using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Move : ScriptableObject
{
    public virtual void Play(Pokemon defender, Pokemon attacker)
    {}
}
