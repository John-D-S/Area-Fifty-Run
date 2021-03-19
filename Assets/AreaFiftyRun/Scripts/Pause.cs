using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public bool gamePaused = false;
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))  
        {
            if (gamePaused == false)
            {
                //Pauses the time scale of the game, and sets the pause menu as active
                Time.timeScale = 0;
                gamePaused = true;
                //Cursor.visible = true;
                pauseMenu.SetActive(true);
            }
            else
            {
                //unpause the time scale of the game, and sets the pause menu inactive
                pauseMenu.SetActive(false);
                //Cursor.visible = false;
                gamePaused = false;
                Time.timeScale = 1;
            }
        }
    }
    /// <summary>
    /// The function for the resume button of the pause menu
    /// Deactiavtes the pause menu and sets the games time scale back to 1
    /// </summary>
    public void UnpauseGame()
    {
        pauseMenu.SetActive(false);
        //Cursor.visible = false;
        gamePaused = false;
        Time.timeScale = 1;
    }
}
