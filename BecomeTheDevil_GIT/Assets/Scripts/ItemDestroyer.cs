using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroyer : MonoBehaviour {
    public float timer = 0;
    private RoomTemplates templates;
    private Player player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
            {
                templates.spawnedItem = false;
                player.countItem++;
            }
            if (gameObject.tag == "KeyItem")
            {
                player.haveKey = true;
            }

            Destroy(gameObject);
        }
    }
}
