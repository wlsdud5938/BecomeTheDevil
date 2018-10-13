using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour {

    private RoomTemplates templates;
    private bool isSpwan = false;
    // Use this for initialization
    void Start () {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }
	
	// Update is called once per frame
	void Update () {
        if (templates.spawnedBoss == true && isSpwan == false) 
        {

            isSpwan = true;
        }
	}
}
