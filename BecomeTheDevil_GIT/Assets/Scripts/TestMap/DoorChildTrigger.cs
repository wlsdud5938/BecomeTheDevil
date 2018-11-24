using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChildTrigger : MonoBehaviour {
    public int doorNum;
    private RoomTemplates templates;          //적 ai테스트를 위해 코드 수정했습니다. mainplay에서 작업하고 싶으시면 
                                                //roomtemplates와 mappath의 주석을 스왑하면됩니다. 차후 테스트 완성후 합쳐집니다.
    //MapPath map;

    // Use this for initialization
    void Start () {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        //map = GameObject.FindGameObjectWithTag("Rooms").GetComponent<MapPath>();

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
        /*if (map.doorTrigger == false && other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject.GetComponent<NavMeshAgent2D>());
            map.ChangeCurrentRoom(doorNum, other.gameObject);
            map.doorTrigger = true;
        }*/
    }
}
