using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    [SerializeField, Tooltip("These are a list of the biomes and their settings")]
    private List<BiomeGeneration> Biomes;
    [SerializeField]
    private int powerUpChance;
    [SerializeField]
    private GameObject pizza;
    [SerializeField]
    private GameObject jetPack;
    [SerializeField]
    private int seed = 0;
    [SerializeField]
    private bool randomizeSeed = true;
    [SerializeField]
    private int modulesBeforeWorld = 8;

    private BiomeGeneration currentBiome;

    private List<GameObject> spawnedObjects;
    private int noOfSpawnedObjects;

    private BiomeGeneration GetNextBiomeGen(BiomeGeneration _currentBiome)//choose a random biomegeneration from biomes within the available NextBiomes in _currentBiome
    {

    }

    private void Start()
    {
        if (!randomizeSeed)
        {
            Random.InitState(seed);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xPos = gameObject.transform.position.x;
        
        if (xPos * 0.05f> noOfSpawnedObjects)
        {
            Instantiate(groundModules[Random.Range(0, groundModules.Count)], new Vector3((Mathf.RoundToInt(xPos * 0.05f) + 5) * 20, 0), Quaternion.identity);
            Instantiate(floatingModules[Random.Range(0, floatingModules.Count)], new Vector3((Mathf.RoundToInt(xPos * 0.05f) + 5) * 20, Random.Range(0, 5)), Quaternion.identity);
            if (Random.Range(0, powerUpChance) == 0)
            {
                if (Random.Range((int)0, 2) == 0)
                {
                    Instantiate(pizza, new Vector3((Mathf.RoundToInt(xPos * 0.05f) + modulesBeforeWorld) * 20 + Random.Range(0, 20), Random.Range(10, 20), 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(jetPack, new Vector3((Mathf.RoundToInt(xPos * 0.05f) + modulesBeforeWorld) * 20 + Random.Range(0, 20), Random.Range(10, 20), 0), Quaternion.identity);
                }
            }
            noOfSpawnedObjects += 1;
        }
    }
}
