using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float posLerpTime = 0.5f;
    [SerializeField] float scaleLerpTime = 0.1f;
    [SerializeField] float baseScale = 8;
    [SerializeField] float scaleMultiplier = 5;

    private Camera cameraComponent;
    private Vector2 lastFramePlayerPosition;
    Vector3 targetPosition = Vector3.zero;
    float targetCameraSize = 0;

    void Start()
    {
        //playerRigidBody = player.GetComponent<Rigidbody2D>();
        lastFramePlayerPosition = player.transform.position;
        cameraComponent = gameObject.GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        targetCameraSize = baseScale + Mathf.Abs((((Vector2)player.transform.position - lastFramePlayerPosition) * scaleMultiplier).magnitude);
        targetPosition = player.transform.position + Vector3.back;

        //Debug.Log(targetScale);

        cameraComponent.orthographicSize = Mathf.Lerp(cameraComponent.orthographicSize, targetCameraSize, scaleLerpTime);
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPosition, posLerpTime);

        Debug.Log(gameObject.transform.localScale);

        lastFramePlayerPosition = player.transform.position;
    }
}
