using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttqTypes { PHYSICAL, SPECIAL };
public enum PokemonType { NORMAL, FIRE, WATER, PLANT, FLY, POISON, BUG, ELECTRIC, STEEL, ROCK, GROUND, GHOST, DARK, PSY, FIGHT, DRAGON, ICE }
[Serializable]
public class Move : ScriptableObject
{
    public AttqTypes attqType;
    public PokemonType pokemonType;

    public virtual void Play(Pokemon defender, Pokemon attacker)
    {}
}
