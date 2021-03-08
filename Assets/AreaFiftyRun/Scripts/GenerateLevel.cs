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

public enum GroundAngle
{
    incline,
    flat,
    decline
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
        public List<GameObject> inclineGroundModules;
        public List<GameObject> flatGroundModules;
        public List<GameObject> declineGroundModules;
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
    [SerializeField]
    private int maximumModuleHeight;

    private BiomeLevelGeneration currentBiomeGeneration;
    private List<GameObject> spawnedObjects;
    private int noOfSpawnedModules;
    private int modulesUntilNextBiome;
    private int currentModuleHeight = 0;

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

    private List<Vector2> rayStartPositions = new List<Vector2>();
    //private List<Vector2> rayDirections = new List<Vector2>();

    private Vector2 GroundPosition(float xPosition)
    {
        Vector2 rayStartPosition = new Vector2(xPosition, 50);
        //Vector2 rayDirection = new Vector2(xPosition, 49);
        var ray = Physics2D.Raycast(rayStartPosition, Vector2.down);
        rayStartPositions.Add(rayStartPosition);
        //rayDirections.Add(rayDirection);
        //Debug.DrawRay(rayStartPosition, rayStartPosition - Vector2.down);
        if (ray.collider != null)
        {
            return new Vector2(xPosition, ray.point.y); 
        }
        else
        {
            return new Vector2(xPosition, 20);
        }
        
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

            GroundAngle angle = (GroundAngle)Random.Range(0,3);//choose a random angle.
            if (currentModuleHeight >= maximumModuleHeight && angle == GroundAngle.incline)//switch from incline to either flat or decline if already at or above max height to avoid exceeding it.
            {
                angle = (GroundAngle)Random.Range(1, 3);
            }
            else if (currentModuleHeight <= 0 && angle == GroundAngle.decline)//switch from decline to either flat or incline if already at or below 0 height
            {
                angle = (GroundAngle)Random.Range(0, 2);
            }

            switch (angle)
            {
                case GroundAngle.incline:
                    Instantiate(currentBiomeGeneration.inclineGroundModules[Random.Range(0, currentBiomeGeneration.inclineGroundModules.Count)], new Vector2(moduleXPosition, currentModuleHeight * 5f), Quaternion.identity);
                    currentModuleHeight++; //instantiate a random incline module and increase the current height by one
                    break;
                case GroundAngle.flat:
                    Instantiate(currentBiomeGeneration.flatGroundModules[Random.Range(0, currentBiomeGeneration.flatGroundModules.Count)], new Vector2(moduleXPosition, currentModuleHeight * 5f), Quaternion.identity);
                    break;
                case GroundAngle.decline:
                    Instantiate(currentBiomeGeneration.declineGroundModules[Random.Range(0, currentBiomeGeneration.declineGroundModules.Count)], new Vector2(moduleXPosition, currentModuleHeight * 5f), Quaternion.identity);
                    currentModuleHeight--; //instantiate a random decline module and decrease the current height by one
                    break;
            }

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
        ///*
        foreach(Vector2 rayStartPosition in rayStartPositions)
        {

            Debug.DrawRay(rayStartPosition, Vector2.down);  

        }
        //*/

    }
}