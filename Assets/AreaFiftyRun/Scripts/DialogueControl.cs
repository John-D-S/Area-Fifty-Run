using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueControl : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private TextMeshProUGUI playerDialogueText;
    [SerializeField] private TextMeshProUGUI playerDeathText;

    [SerializeField] private string[] playerStartSentences;
    [SerializeField] private string[] playerDeathSentences;

    
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private GameObject deathBubble;
    
    private float speechBubbleTimer = 3f;



    // Start is called before the first frame update
    void Start()
    {
        playerDialogueText.text = playerStartSentences[Random.Range(0, playerStartSentences.Length)];
        playerDeathText.text = playerDeathSentences[Random.Range(0, playerDeathSentences.Length)];


    }

    // Update is called once per frame
    void Update()
    {
        speechBubbleTimer -= Time.deltaTime;
        if (speechBubbleTimer > 0)
        {
            speechBubble.SetActive(true);            
        }
        else 
        {
            speechBubble.SetActive(false);
        }

       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "WallOfDeath")
        {
            deathBubble.SetActive(true);
        }
    }
}
