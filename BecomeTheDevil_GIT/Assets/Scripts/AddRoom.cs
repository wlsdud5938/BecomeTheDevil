using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour {

	private RoomTemplates templates;
    public int path;

	void Start(){

		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		templates.rooms.Add(this.gameObject);
        if(path == 1)
        {
            templates.bList.Add(this.gameObject);
        }
        else if (path == 2)
        {
            templates.tList.Add(this.gameObject);
        }
        else if (path == 3)
        {
            templates.lList.Add(this.gameObject);
        }
        else if (path == 4)
        {
            templates.rList.Add(this.gameObject);
        }
    }
}
