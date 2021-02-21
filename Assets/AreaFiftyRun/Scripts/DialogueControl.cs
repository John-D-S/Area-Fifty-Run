using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueControl : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI playerDialogueText;

    [SerializeField] private string[] playerStartSentences;
    [SerializeField] private string[] playerDeathSentences;

    
    [SerializeField] private GameObject speechBubble;

    
    private float speechBubbleTimer = 3f;



    // Start is called before the first frame update
    void Start()
    {
        playerDialogueText.text = playerStartSentences[Random.Range(0, playerStartSentences.Length)];


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

}
