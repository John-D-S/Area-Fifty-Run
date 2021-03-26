using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Removable : MonoBehaviour
{
    private GameObject wallOfDeath;

    public void GetWallOfDeath() 
    {
        wallOfDeath = GameObject.FindGameObjectWithTag("WallOfDeath");
    } 

    public void DestroyOldObject()
    {
        if (wallOfDeath)
        {
            if (wallOfDeath.transform.position.x > gameObject.transform.position.x)
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
