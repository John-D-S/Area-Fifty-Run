using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOfDeath : MonoBehaviour
{
    [SerializeField, Tooltip("Speed is in units per second.")]
    private float Speed;
    [SerializeField, Tooltip("If true, the box will not move")]
    private bool doNotMove;
    [SerializeField]
    private PlayerController player;

    private bool playerLeftBunker;

    void FixedUpdate()
    {
        if (!playerLeftBunker)
        {
            if (player.leftBunker)
            {
                playerLeftBunker = true;
            }
        }

        if (!doNotMove && playerLeftBunker)
        {
        gameObject.transform.Translate(new Vector3(Time.fixedDeltaTime * Speed, 0, 0));
        }
    }
}
