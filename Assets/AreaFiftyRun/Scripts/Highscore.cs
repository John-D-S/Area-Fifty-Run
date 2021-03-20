using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscore : MonoBehaviour
{

    public int highScore;
    public int gameScore;

    
    public void SaveHighscore()
    {
        
       

        if (PlayerPrefs.HasKey("highScore"))
        {
            if (gameScore > PlayerPrefs.GetInt("highScore"))
            {
                highScore = gameScore;
                PlayerPrefs.SetInt("highScore", highScore);
                PlayerPrefs.Save();
            }
        }
        else
        {
            if (gameScore > highScore)
            {
                highScore = gameScore;
                PlayerPrefs.SetInt("highScore", highScore);
                PlayerPrefs.Save();
            }
        }
    }

    public int CallHighscore(int _highScore)
    {
        _highScore =  PlayerPrefs.GetInt("highscore");
        return _highScore;
    }
}
