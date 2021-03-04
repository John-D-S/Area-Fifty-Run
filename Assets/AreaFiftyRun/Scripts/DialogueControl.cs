using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogueControl : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
   
    [SerializeField] private TextMeshProUGUI playerDialogueText;
    [SerializeField] private TextMeshProUGUI playerDeathText;

    [SerializeField] private AudioSource source;



    [SerializeField] private string[] playerStartSentences = new string[] 
    {
        "I got the need for speed!",
        "I'm free at last.",
        "Let me outta this place",
        "Gotta fight, gotta fly, gotta crow",
        "Say hello to my little friend",
        "You can't catch the gingerbread man",
        "LEEROY JENKINS!",
        "Toto, I've got a feeling we're not in Kansas anymore",
        "May the Force be with you......I mean me.",
        "I love the smell of Napalm in the morning",
        "E.T. phone home",
        "There's no place like home",
        "There's no place like gnome",
        "Elementary, my dear Watson",
        "Hasta la vista, baby",
        "Nobody puts Baby in a corner"
    };
    [SerializeField] private string[] playerDeathSentences = new string[] 
    {
        "You all came for little old me, you shouldn't have...",
        "They may take away our lives, but they'll never take our FREEDOM!",
        "To die will be an awefully big adventure",
        "Frankly my dear, I don't give a damn!",
        "Go ahead, make my day!",
        "I'll be back!",
        "You talking to me!",
        "Hey! I'm walking here!",
        "Show me the money!",
        "You can't handle the truth!",
        "STELLA!",
        "Houston, we have a problem"
    };
    
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
        if (playerController.leftBunker == true)
        {
            if(!source.isPlaying)
                source.Play();
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

        if (playerController.dead == true)
        {
            deathBubble.SetActive(true);
        }
    }

    
}
