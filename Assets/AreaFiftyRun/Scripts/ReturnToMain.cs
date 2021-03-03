using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMain : MonoBehaviour
{
   
    
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

}


