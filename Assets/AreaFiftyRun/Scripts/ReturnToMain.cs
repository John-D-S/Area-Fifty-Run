using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMain : MonoBehaviour
{
   
    
    public void MainMenu()
    {
        //Sets the timescale back to 1 and Loads the Main menu screen
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

}


