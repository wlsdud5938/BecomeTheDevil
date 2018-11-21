using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingOn : MonoBehaviour {
    // Use this for initialization
    private bool isOn = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("Player"))
            isOn = true;
 
        if (isOn == true)
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
	}
}
