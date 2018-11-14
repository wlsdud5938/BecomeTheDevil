using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChildTrigger : MonoBehaviour {
    public int doorNum;
    private RoomTemplates templates;

    // Use this for initialization
    void Start () {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

        if (gameObject.name == "TDoor")
            doorNum = 1;
        else if (gameObject.name == "BDoor")
            doorNum = 2;
        else if (gameObject.name == "LDoor")
            doorNum = 3;
        else if (gameObject.name == "RDoor")
            doorNum = 4;
    }

    // Update is called once per frame
    void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (templates.doorTrigger == false && other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            templates.ChangeCurrentRoom(doorNum, other.gameObject);
            templates.doorTrigger = true;
        }
    }
}
