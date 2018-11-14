using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPath : MonoBehaviour {
    public GameObject[] pathRooms;
    public int[] pathRoomDoor = {4, 2, 3 };
    public GameObject currentMapnode;
    public bool doorTrigger = false;
    public GameObject enemy;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (doorTrigger == true)
            doorTrigger = false;
    }
    public void ChangeCurrentRoom(int doorNum, GameObject other)
    {
        if (doorNum == 1)
        {
            currentMapnode = currentMapnode.GetComponent<MapNode>().upNode;
            other.transform.position = new Vector3(currentMapnode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("BDoor").transform.position.x
                , currentMapnode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("BDoor").transform.position.y + 1.5F, 0);
        }
        else if (doorNum == 2)
        {
            currentMapnode = pathRooms[2];
            other.transform.position = new Vector3(currentMapnode.transform.Find("07_DoorTrigger").gameObject.transform.Find("TDoor").transform.position.x
                , currentMapnode.transform.Find("07_DoorTrigger").gameObject.transform.Find("TDoor").transform.position.y - 1.5F, 0);
        }
        else if (doorNum == 3)
        {
            currentMapnode = currentMapnode.GetComponent<MapNode>().leftNode;
            other.transform.position = new Vector3(currentMapnode.transform.Find("07_DoorTrigger").gameObject.transform.Find("RDoor").transform.position.x - 1
                , currentMapnode.transform.Find("07_DoorTrigger").gameObject.transform.Find("RDoor").transform.position.y, 0);
        }
        else if (doorNum == 4)
        {
            currentMapnode = pathRooms[1];
            other.transform.position = new Vector3(currentMapnode.transform.Find("07_DoorTrigger").gameObject.transform.Find("LDoor").transform.position.x + 1
                , currentMapnode.transform.Find("07_DoorTrigger").gameObject.transform.Find("LDoor").transform.position.y, 0);
        }
        other.gameObject.GetComponent<EnemyAITest>().isMove = true;

    }
}
