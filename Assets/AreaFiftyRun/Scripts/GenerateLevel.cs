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

public class GenerateLevel : MonoBehaviour
{
    [System.Serializable]
    public struct BiomeLevelGeneration
    {
        [Tooltip("What this biome is.")]
        public Biome biome;
        [Tooltip("These are the biomes that this Biome can lead into.")]
        public List<Biome> possibleNextBiomes;
        public List<GameObject> groundModules;
        public List<GameObject> propModules;
        [Range(0, 1), Tooltip("The probability as a percentage that a prop will spawn on a given module")]
        public float propChance;
    }

    [System.Serializable]
    public struct BiomeBackgroundGeneration
    {
        public List<GameObject> backgroundPrefabs;//a list of prefabs that can be spawned in the background
        public GameObject backGroundGround;
        public Color backgroundGroundColor;
    }

    [Header("Level Generation")]//variables to do with level generation and modules
    [SerializeField, Tooltip("These are a list of the biomes and their settings. They should be in the order of the biome enum")]
    private List<BiomeLevelGeneration> levelBiomes = new List<BiomeLevelGeneration>(7);
    [SerializeField, Tooltip("The first biome to be generated")]
    private Biome currentBiome;
    [SerializeField]
    private int modulesBeforeWorld = 8;
    [SerializeField]
    private int minimumModulesPerBiome = 100;
    [SerializeField]
    private int maximumModulesPerBiome = 200;

    private BiomeLevelGeneration currentBiomeGeneration;
    private List<GameObject> spawnedObjects;
    private int noOfSpawnedModules;
    private int modulesUntilNextBiome;

    [Header("Background Generation")]
    [SerializeField]
    private List<BiomeBackgroundGeneration> backgroundBiomes = new List<BiomeBackgroundGeneration>(7);
    [SerializeField]
    private SpriteRenderer BackgroundGround;

    private BiomeBackgroundGeneration currentBackgroundBiome;
    private float nextBackgroundPrefabXPosition;

    [Header("Level Seed")]//variables to do with creating a seed for Random
    [SerializeField]
    private int seed = 0;
    [SerializeField]
    private bool randomizeSeed = true;

    [Header("PowerUps and Collectables")]//variables to do with placing powerups
    [SerializeField, Tooltip("The probability as a percentage that a powerup will spawn on a given module")]
    private float powerUpChance;
    [SerializeField]
    private GameObject pizza;
    [SerializeField]
    private GameObject jetPack;
    

    private Biome GetNextBiome()//choose a random biomegeneration from biomes within the available NextBiomes in _currentBiome
    {
        List<Biome> possibleBiomes = currentBiomeGeneration.possibleNextBiomes;
        Biome nextBiome = possibleBiomes[Random.Range(0, possibleBiomes.Count)];
        return nextBiome;   
    }

    private Vector2 GroundPosition(float xPosition)
    {
        Vector2 rayStartPosition = new Vector2(xPosition, 50);
        var ray = Physics2D.Raycast(rayStartPosition, rayStartPosition - Vector2.down);
        if (ray.collider != null)
        {
            return new Vector2(xPosition, ray.point.y); 
        }
        return new Vector2(xPosition, 20);
    }


    void GenerateBackground()
    {
        float xPos = Camera.main.transform.position.x;

        if (xPos >= nextBackgroundPrefabXPosition)
        {
            GameObject newBackgroundObj = Instantiate(currentBackgroundBiome.backgroundPrefabs[Random.Range(0, currentBackgroundBiome.backgroundPrefabs.Count)], new Vector3(xPos + 150, 0, 0), Quaternion.identity);
            nextBackgroundPrefabXPosition = xPos + Random.Range(1f, 10f);
        }
    }

    //call this whenever we need to change the biome, or when we need to start the game
    void UpdateGenerationVariables()
    {
        BackgroundGround.color = backgroundBiomes[(int)currentBiome].backgroundGroundColor;//set the colour of the ground in the background to the correct colour
        modulesUntilNextBiome = minimumModulesPerBiome + Random.Range(0, maximumModulesPerBiome - minimumModulesPerBiome);//
        currentBiomeGeneration = levelBiomes[(int)currentBiome];
        currentBackgroundBiome = backgroundBiomes[(int)currentBiome];
    }

    //create a void GenerateLevel()? idk

    private void Start()
    {
        UpdateGenerationVariables();
        if (!randomizeSeed)
        {
            Random.InitState(seed);
        }
    }

    void FixedUpdate()
    {
        float xPos = gameObject.transform.position.x;

        if(modulesUntilNextBiome <= 0)
        {
            currentBiome = GetNextBiome();
            UpdateGenerationVariables();
        }

        GenerateBackground();

        if (xPos * 0.05f > noOfSpawnedModules)
        {
            float moduleXPosition = (Mathf.RoundToInt(xPos * 0.05f) + modulesBeforeWorld) * 20;
            //instantiate a ground module.
            Instantiate(currentBiomeGeneration.groundModules[Random.Range(0, currentBiomeGeneration.groundModules.Count)], new Vector3(moduleXPosition, 0f), Quaternion.identity);

            if (Random.Range(0, 1) < currentBiomeGeneration.propChance)
            {
                float propXPosition = Random.Range(0f, 20f);

                Vector2 rayStartPosition = new Vector2(moduleXPosition + propXPosition, 20);
                Instantiate(currentBiomeGeneration.propModules[Random.Range(0, currentBiomeGeneration.propModules.Count)], GroundPosition(moduleXPosition + propXPosition), Quaternion.identity);
            }

            if (Random.Range(0, 1) < powerUpChance)
            {
                float powerUpXPosition = moduleXPosition + Random.Range(0, 20);
                if (Random.Range((int)0, 2) == 0)
                {
                    Instantiate(pizza, GroundPosition(powerUpXPosition), Quaternion.identity);
                }
                else
                {
                    Instantiate(jetPack, GroundPosition(powerUpXPosition), Quaternion.identity);
                }
            }
            noOfSpawnedModules ++;
            modulesUntilNextBiome --;
        }
    }
}