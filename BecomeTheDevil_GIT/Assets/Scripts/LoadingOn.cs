using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingOn : MonoBehaviour {
    // Use this for initialization
    private bool isOn = false;
    public RoomTemplates temp;
	void Start () {
        temp = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
	}
	
	// Update is called once per frame
	void Update () {
        if (temp.spawnedBoss)
            isOn = true;
 
        if (isOn == true)
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
