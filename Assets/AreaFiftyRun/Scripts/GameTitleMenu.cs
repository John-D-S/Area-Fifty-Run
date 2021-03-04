using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameTitleMenu : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log ("Quit");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    

}
