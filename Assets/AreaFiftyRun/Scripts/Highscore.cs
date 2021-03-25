using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class Highscore : MonoBehaviour
{

    private TMP_Text highscoreText;

    private int highScore;
    public int HighScore 
    {
        get
        {
            return highScore;
        }
        private set
        {
            highScore = value;
            highscoreText.text = "Highscore: " + highScore.ToString();
            
        }
     }


    private int gameScore;
    public int GameScore
    {
        get => gameScore;
        set
        {
            gameScore = value;
            SaveHighscore();
        }
    }

    private void Awake() => highscoreText = GetComponent<TMP_Text>();

    private void Start() => HighScore = CallHighscore(highScore);

    private void OnDisable() => SaveHighscore();

    public void SaveHighscore()
    {
        
       

        if (PlayerPrefs.HasKey("highScore"))
        {
            if (gameScore > PlayerPrefs.GetInt("highScore"))
            {
                HighScore = gameScore;
                PlayerPrefs.SetInt("highScore", HighScore);
                PlayerPrefs.Save();
            }
        }
        else
        {
            if (gameScore > highScore)
            {
                HighScore = gameScore;
                PlayerPrefs.SetInt("highScore", HighScore);
                PlayerPrefs.Save();
            }
        }
    }

    public int CallHighscore(int _highScore)
    {

        _highScore =  PlayerPrefs.GetInt("highScore", 0);
        return _highScore;
    }
}
