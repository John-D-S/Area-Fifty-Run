using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOfDeath : MonoBehaviour
{
    [SerializeField, Tooltip("Speed is in units per second.")]
    private float Speed;

    void FixedUpdate()
    {
        gameObject.transform.Translate(new Vector3(Time.fixedDeltaTime * Speed, 0, 0));
    }
}
