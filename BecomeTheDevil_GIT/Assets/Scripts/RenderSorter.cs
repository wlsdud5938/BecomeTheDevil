using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderSorter : MonoBehaviour
{
    public float offset;
    Renderer myRenderer;
    Transform middle;
    float height;
    float baseOrder = 5000;


    // Use this for initialization
    void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
        height = GetComponent<SpriteRenderer>().bounds.size.y;

    }

    // Update is called once per frame
    void Update()
    {
        myRenderer.sortingOrder = 10000-(int)(transform.position.y);
    }
}

