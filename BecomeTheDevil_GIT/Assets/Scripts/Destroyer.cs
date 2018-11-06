using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {
    private RoomTemplates templates;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Room" || other.gameObject.tag == "SpawnPoint")
        {
            Destroy(other.gameObject);
            transform.parent.GetChild(0).gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
	}
}
