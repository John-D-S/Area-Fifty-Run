using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderGround : MonoBehaviour
{
    private void Awake()
    {
        transform.localScale = new Vector2(1, transform.position.y + 30);
    }
}
