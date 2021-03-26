using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Removable : MonoBehaviour
{
    private GameObject wallOfDeath;
    private Vector2 gameObjectOriginalPosition;

    public void GetWallOfDeath() 
    {
        gameObjectOriginalPosition = gameObject.transform.position;
        wallOfDeath = GameObject.FindGameObjectWithTag("WallOfDeath");
    } 

    public void DestroyOldObject()
    {
        if (wallOfDeath)
        {
            if (wallOfDeath.transform.position.x > gameObjectOriginalPosition.x)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            wallOfDeath = GameObject.FindGameObjectWithTag("WallOfDeath");
        }
    }
}
