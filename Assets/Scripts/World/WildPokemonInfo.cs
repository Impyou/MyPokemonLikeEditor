using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildPokemonInfo : MonoBehaviour
{
    [Serializable]
    public struct PokemonEncounterInfo
    {
        public PokemonDef pokemon;
        public int rate;
    }

    public PokemonEncounterInfo[] pokemonEncounterInfos;
    public int generateListSize = 1;

    private int currentEncounterIndex = 0;
    public List<PokemonDef> generatePokemonEncounter = new List<PokemonDef>();
    

    public PokemonDef PickPokemon()
    {
        var wildPokemon = generatePokemonEncounter[currentEncounterIndex];

        generatePokemonEncounter[currentEncounterIndex] = GeneratePokemon();
        currentEncounterIndex = (currentEncounterIndex + 1) % generatePokemonEncounter.Count;

        return wildPokemon;
    }

    public PokemonDef GeneratePokemon()
    {
        var totalRate = 0;
        foreach (var pokemonEncounterInfo in pokemonEncounterInfos)
        {
            totalRate += pokemonEncounterInfo.rate;
        }
        float randomPickValue = UnityEngine.Random.Range(0f, 1f);
        var cumulativeRate = 0f;
        foreach (var pokemonEncounterInfo in pokemonEncounterInfos)
        {
            cumulativeRate += (pokemonEncounterInfo.rate / (float)totalRate);
            if (randomPickValue < cumulativeRate)
                return new PokemonDef(pokemonEncounterInfo.pokemon);
        }
        return new PokemonDef(pokemonEncounterInfos[pokemonEncounterInfos.Length - 1].pokemon);
    }

    public void Start()
    {
        for(int i = 0; i < generateListSize;i++)
        {
            generatePokemonEncounter.Add(GeneratePokemon());
        }
    }
}
