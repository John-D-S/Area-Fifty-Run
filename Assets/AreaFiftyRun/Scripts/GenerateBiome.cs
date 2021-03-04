using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Biome
{
    desert,
    mesa,
    forest,
    farm,
    urban,
    city,
    industrial
}

public class BiomeGeneration
{
    [SerializeField]
    private Biome biome;
    [SerializeField, Tooltip("These are the biomes that this Biome can lead into")]
    private List<Biome> possibleNextBiomes;
    [SerializeField]
    private List<List<GameObject>> groundModules;
    [SerializeField]
    private List<GameObject> propModules;
    [SerializeField]
    private int powerUpChance;
    [SerializeField]
    private GameObject pizza;
    [SerializeField]
    private GameObject jetPack;
}