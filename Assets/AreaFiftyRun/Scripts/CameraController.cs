using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float lerpTime = 0.5f;

    void FixedUpdate()
    {
        
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, player.transform.position + Vector3.back, lerpTime);
    }
}
