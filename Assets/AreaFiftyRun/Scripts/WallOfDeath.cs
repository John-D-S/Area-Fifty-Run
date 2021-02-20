using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOfDeath : MonoBehaviour
{
    [SerializeField, Tooltip("Speed is in units per second.")]
    private float Speed;
    [SerializeField, Tooltip("If true, the box will not move")]
    private bool doNotMove;

    void FixedUpdate()
    {
        if (!doNotMove)
        {
        gameObject.transform.Translate(new Vector3(Time.fixedDeltaTime * Speed, 0, 0));
        }
    }
}
