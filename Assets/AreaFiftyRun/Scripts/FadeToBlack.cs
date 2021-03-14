using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class FadeToBlack : MonoBehaviour
{
    
    [SerializeField]
    private PlayerController playerController;
    [SerializeField, Tooltip("the time in seconds that it takes for the screen to fade to black after the player dies")]
    private float fadeToBlackTime = 1;
    [SerializeField, Tooltip("The time in seconds that the black screen stays before the level restarts")]
    private float timeToStayBlack = 3;

    //faded becomes true once the screen is completely black
    bool faded;
    private Image blackScreen;

    private void Start()
    {
        blackScreen = gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        if (playerController.dead == true)
        {
            if (!faded)
            {
                if (blackScreen.color.a + Time.deltaTime / fadeToBlackTime <= 1)
                {
                    blackScreen.color = new Color(0, 0, 0, blackScreen.color.a + Time.deltaTime / fadeToBlackTime);
                }
                else
                {
                    blackScreen.color = Color.black;
                    faded = true;
                }
            }
            else
            {
                timeToStayBlack -= Time.deltaTime;
                if (timeToStayBlack < 0)
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
    }
}