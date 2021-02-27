using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxProp : MonoBehaviour
{
    private Vector2 originalPosition;
    private float scale;//a value from 0.1f to 0.9f
    private GameObject mainCamera;

    //these three variables will be used for Background images
    [SerializeField, Tooltip("if true, scale will be randomized on initialization, Otherwise, it will be set to Default Scale")]
    private bool randomScale = true;
    [SerializeField, Tooltip("0 is infinitely far away, 1 is on the player's plane")]
    private float defaultScale = 0.05f;
    [SerializeField, Tooltip("if true, the gameobject's scale will be applied to its transform")]
    private bool applyScaleToTransform = true;


    [SerializeField]
    private float maxScale = 0.9f;
    [SerializeField]
    private float minScale = 0.1f;
    [SerializeField]
    private SpriteRenderer spriteRenderer; //set this manually in the prefab to avoid using getcomponent

    // Start is called before the first frame update
    void Start()
    {
        if (randomScale)
        {
            scale = Random.Range(maxScale, minScale);
        }
        else
        {
            scale = defaultScale;
        }

        if (applyScaleToTransform)
        {
            gameObject.transform.localScale = Vector3.one * scale;
        }

        //TODO: find a way to add a blue haze to the sprite proportional to its scale, to simulate atmospheric perspective

        spriteRenderer.sortingOrder = Mathf.RoundToInt(scale * 100) - 100;
        originalPosition = gameObject.transform.position;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera != null)
        {
            Vector2 camPos = (Vector2)mainCamera.transform.position; //campos is shorthand for the position of the camera
            Vector2 newPosition = (originalPosition - camPos) * scale + camPos;//setting the position of the prop to simulate perspective;
            gameObject.transform.position = newPosition;
        }
    }
}
