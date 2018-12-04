using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoItem : MonoBehaviour {
    float timer = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 5)
            transform.GetChild(0).gameObject.SetActive(false);
	}
    public void OnText()
    {
        timer = 0;
        transform.GetChild(0).gameObject.SetActive(true);

    }
}
