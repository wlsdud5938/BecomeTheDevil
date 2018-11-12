using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour {
    public RoomTemplates templates;
	// Use this for initialization
	void Start () {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }

    // Update is called once per frame
    void Update () {
        if (templates.spawnedBoss == true)
            gameObject.SetActive(false);
	}
}
