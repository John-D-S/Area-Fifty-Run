using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Score : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private TextMeshProUGUI scoreText;

    internal static int GrabScore(object score)
    {
        throw new NotImplementedException();
    }

    private int score = 0;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (player.transform.position.x > score)
        {
            score = Mathf.RoundToInt(player.transform.position.x);
        }
        scoreText.text = "Score: " + score;
    }

    


}