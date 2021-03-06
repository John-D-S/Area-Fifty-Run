using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxProp : Removable
{
    private static int lastSpriteSortOrder = 0;

    private Vector2 originalPosition;
    private float scale;//a value from 0.1f to 0.9f
    private GameObject mainCamera;
    [SerializeField]
    Color AtmosphereColor = Color.HSVToRGB(0.55f, 0.25f, 0.75f);
    //these three variables will be used for Background images
    [SerializeField, Tooltip("if true, scale will be randomized on initialization, Otherwise, it will be set to Default Scale")]
    private bool randomScale = true;
    [SerializeField, Tooltip("0 is infinitely far away, 1 is on the player's plane")]
    private float defaultScale = 0.05f;
    [SerializeField, Tooltip("Good for mountains and stuff.")]
    private float sizeMultiplier = 1;
    [SerializeField, Tooltip("if true, the gameobject's scale will be applied to its transform")]
    private bool applyScaleToTransform = true;


    [SerializeField]
    private float maxScale = 0.9f;
    [SerializeField]
    private float minScale = 0.1f;
    [SerializeField]
    private SpriteRenderer spriteRenderer; //set this manually in the prefab to avoid using getcomponent
    [SerializeField]
    SpriteRenderer atmosphereSpriteRenderer;

    private int GetSpriteSortOrder(float _scale)
    {
        return (Mathf.RoundToInt(_scale * 500) - 500) * 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetWallOfDeath();
        if (randomScale)
        {
            scale = Random.Range(maxScale, minScale);
            while (GetSpriteSortOrder(scale) == lastSpriteSortOrder)
            {
                scale = Random.Range(maxScale, minScale);
            }
        }
        else
        {
            scale = defaultScale;
        }

        if (applyScaleToTransform)
        {
            gameObject.transform.localScale = Vector3.one * scale * sizeMultiplier;
        }

        //TODO: find a way to add a blue haze to the sprite proportional to its scale, to simulate atmospheric perspective
        int spriteSortOrder = GetSpriteSortOrder(scale);
        spriteRenderer.sortingOrder = spriteSortOrder;
        atmosphereSpriteRenderer.sortingOrder = spriteSortOrder + 1;
        originalPosition = gameObject.transform.position;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        Color sprColor = atmosphereSpriteRenderer.color;
        atmosphereSpriteRenderer.color = new Color(sprColor.r, sprColor.g, sprColor.b, Mathf.Abs(scale - 1) * Mathf.Abs(scale - 1) * Mathf.Abs(scale - 1)) * 0.5f;

        lastSpriteSortOrder = spriteSortOrder;
    }

    // Update is called once per frame
    void Update()
    {
        DestroyOldObject();
        if (mainCamera != null)
        {
            Vector2 camPos = (Vector2)mainCamera.transform.position; //campos is shorthand for the position of the camera
            Vector2 newPosition = (originalPosition - camPos) * scale + camPos;//setting the position of the prop to simulate perspective;
            gameObject.transform.position = newPosition;
        }
    }
}
