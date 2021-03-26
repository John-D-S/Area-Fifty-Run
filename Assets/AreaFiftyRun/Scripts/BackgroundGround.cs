using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGround : MonoBehaviour
{
    [SerializeField, Range(0, 1), Tooltip("1 means the horizon is infinitely far away, 0 means it is on the player's layer")]
    float HorizonDistance = 0.95f;
    [SerializeField]
    private float GroundWidth = 50;
    private GameObject mainCamera;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        Vector3 camPos = mainCamera.transform.position;
        transform.position = new Vector3(camPos.x, camPos.y * HorizonDistance * 0.5f);
        transform.localScale = new Vector3(GroundWidth, camPos.y * HorizonDistance);
    }
}
