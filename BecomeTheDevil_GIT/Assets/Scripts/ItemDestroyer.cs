using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroyer : MonoBehaviour {
    public float timer = 0;
    private RoomTemplates templates;
    // Use this for initialization
    void Start () {

        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 15.0f && gameObject.tag == "Item")
        {
            Destroy(gameObject);
            templates.spawnedItem = false;
        }
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.tag == "Item")
                templates.spawnedItem = false;
            if (gameObject.tag == "KeyItem")
                templates.spawnedKeyItem = false;
        }
        Destroy(gameObject);
    }
}
