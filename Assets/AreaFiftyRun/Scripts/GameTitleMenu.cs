using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameTitleMenu : MonoBehaviour
{
    
    public void startGame()
    {
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        Debug.Log ("Quit");
        Application.Quit();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
