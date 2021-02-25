using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxProp : MonoBehaviour
{
    private Vector2 originalPosition;
    private float scale;//a value from 0.1f to 0.9f
    private int layer;//an int from 0 to 80, representing the order in layer that the sprite is on
    private GameObject camera;
    [SerializeField]
    private float maxScale = 0.9f;
    [SerializeField]
    private float minScale = 0.1f;
    [SerializeField]
    private SpriteRenderer spriteRenderer; //set this manually in the prefab to avoid using getcomponent

    // Start is called before the first frame update
    void Start()
    {
        scale = Random.Range(maxScale, minScale);
        gameObject.transform.localScale = Vector3.one * scale;
        //TODO: find a way to add a blue haze to the sprite proportional to its scale, to simulate atmospheric perspective
        spriteRenderer.sortingOrder = Mathf.RoundToInt(scale * 100);
        originalPosition = gameObject.transform.position;
    }

    public void SetCamera(GameObject cameraToSet)//This function is called in the script that instantiates this object.
    {
        camera = cameraToSet;
    }

    // Update is called once per frame
    void Update()
    {
        if (camera != null)
        {
            Vector2 camPos = (Vector2)camera.transform.position; //campos is shorthand for the position of the camera
            Vector2 newPosition = (originalPosition - camPos) * scale + camPos;//setting the position of the prop to simulate perspective;
            gameObject.transform.position = newPosition;
        }
    }
}
