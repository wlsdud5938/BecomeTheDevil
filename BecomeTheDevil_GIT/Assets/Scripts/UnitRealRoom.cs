using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRealRoom : MonoBehaviour {
    public GameObject realRoom;
    RoomTemplates temp;
	// Use this for initialization
	void Start () {
        temp = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        realRoom = temp.currentMapnode.GetComponent<MapNode>().realMap;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
