using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxBackground : MonoBehaviour
{
    [Range(0f, 1f)] public float parallax;

    private float startPos;
    private float width;
    private GameObject cam;

    private void Start()
    {
        startPos = transform.position.x;
        cam = Camera.main.gameObject;
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float dist = cam.transform.position.x * parallax;
        float movement = cam.transform.position.x * (1 - parallax);

        transform.position = new Vector3(startPos + dist, transform.position.y);

        if (movement > startPos + width)
        {
            startPos += width;
        }
        else if (movement< startPos - width)
        {
            startPos -= width;
        }
    }
}
