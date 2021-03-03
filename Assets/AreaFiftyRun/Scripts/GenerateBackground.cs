using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBackground : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> backgroundPrefabs;//a list of prefabs that can be spawned in the background
    [SerializeField]
    
    private float nextBackgroundPrefabXPosition;

    // Update is called once per frame
    void Update()
    {
        float xPos = gameObject.transform.position.x;

        if (xPos >= nextBackgroundPrefabXPosition)
        {
            GameObject newBackgroundObj = Instantiate(backgroundPrefabs[Random.Range(0, backgroundPrefabs.Count)], new Vector3(xPos + 150, 0, 0), Quaternion.identity);
            nextBackgroundPrefabXPosition = xPos + Random.Range(1f, 10f);
        }
    }
}
