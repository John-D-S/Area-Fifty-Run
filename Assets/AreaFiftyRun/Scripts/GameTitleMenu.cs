using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameTitleMenu : MonoBehaviour
{
    
    public void StartGame()
    {
        //Loads the game scene
        SceneManager.LoadScene(1); 
    }

    public void QuitGame()
    {
        //Quits to the editor if in Unity, Quits the game if playing the build.
        Debug.Log ("Quit");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    

}
