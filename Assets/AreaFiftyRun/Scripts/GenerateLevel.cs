using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> groundModules;
    [SerializeField]
    private List<GameObject> floatingModules;
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

    private List<GameObject> spawnedObjects;
    private int noOfSpawnedObjects;

    private void Start()
    {
        if (!randomizeSeed)
        {
            Random.InitState(seed);
        }

        for (int i = 0; i < 5; i++)
        {
            if (i == 0) //this if-else statement ensures that the first module is always just the default one. it's probably not the best way of doing it
            {
                Instantiate(groundModules[0], new Vector3((float)i * 20, 0), Quaternion.identity);//quaternion.identity is just "quaternion with no rotation"
            }
            else
            {
                Instantiate(groundModules[Random.Range(0, groundModules.Count)], new Vector3((float)i * 20, 0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xPos = gameObject.transform.position.x;
        if (xPos * 0.05f> noOfSpawnedObjects)
        {
            Debug.Log("ModuleJustSpawned");
            Instantiate(groundModules[Random.Range(0, groundModules.Count)], new Vector3((Mathf.RoundToInt(xPos * 0.05f) + 5) * 20, 0), Quaternion.identity);
            Instantiate(floatingModules[Random.Range(0, floatingModules.Count)], new Vector3((Mathf.RoundToInt(xPos * 0.05f) + 5) * 20, Random.Range(0, 5)), Quaternion.identity);
            if (Random.Range(0, powerUpChance) == 0)
            {
                if (Random.Range((int)0, 2) == 0)
                {
                    Instantiate(pizza, new Vector3((Mathf.RoundToInt(xPos * 0.05f) + 5) * 20 + Random.Range(0, 20), Random.Range(10, 20), 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(jetPack, new Vector3((Mathf.RoundToInt(xPos * 0.05f) + 5) * 20 + Random.Range(0, 20), Random.Range(10, 20), 0), Quaternion.identity);
                }
            }

            
            noOfSpawnedObjects += 1;
        } 
    }
}
