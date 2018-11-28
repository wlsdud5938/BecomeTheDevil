using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererSorter : MonoBehaviour {
    public float offset;
    Renderer myRenderer;
    float height;
    float baseOrder = 5000;


    // Use this for initialization
    void Awake () {
        myRenderer = gameObject.GetComponent<Renderer>();
        height = GetComponent<SpriteRenderer>().bounds.size.y;
        Debug.Log(height);



    }
	
	// Update is called once per frame
	void Update () {



        myRenderer.sortingOrder = (int)(-(transform.position.y+height/2)*1000);        
	}
}
