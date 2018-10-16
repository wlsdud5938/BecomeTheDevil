using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    public int openingDirection;
    private GameObject room;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door


    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;

    public float waitTime = 4f;

    void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }


    void Spawn()
    {
        if (spawned == false && gameObject.transform.parent.transform.parent.GetComponent<AddRoom>().path == -1)
        {
            if (openingDirection == 1)
            {
                // Need to spawn a room with a BOTTOM door.
                rand = Random.Range(0, templates.bottomRooms.Length);
                room = Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                room.GetComponent<AddRoom>().path = 1;
            }
            else if (openingDirection == 2)
            {
                // Need to spawn a room with a TOP door.
                rand = Random.Range(0, templates.topRooms.Length);
                room = Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                room.GetComponent<AddRoom>().path = 2;

            }
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a LEFT door.
                rand = Random.Range(0, templates.leftRooms.Length);
                room = Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                room.GetComponent<AddRoom>().path = 3;

            }
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a RIGHT door.
                rand = Random.Range(0, templates.rightRooms.Length);
                room =Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                room.GetComponent<AddRoom>().path = 4;

            }
            spawned = true;
        }
        if (spawned == false && templates.rooms.Count <= 14)
        {
            if (openingDirection == 1)
            {
                // Need to spawn a room with a BOTTOM door.
                rand = Random.Range(0, templates.bottomRooms.Length);
                room = Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                room.GetComponent<AddRoom>().path = gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<AddRoom>().path;
            }
            else if (openingDirection == 2)
            {
                // Need to spawn a room with a TOP door.
                rand = Random.Range(0, templates.topRooms.Length);
                room = Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                room.GetComponent<AddRoom>().path = gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<AddRoom>().path;
            }
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a LEFT door.
                rand = Random.Range(0, templates.leftRooms.Length);
                room = Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                room.GetComponent<AddRoom>().path = gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<AddRoom>().path;
            }
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a RIGHT door.
                rand = Random.Range(0, templates.rightRooms.Length);
                room = Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                room.GetComponent<AddRoom>().path = gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<AddRoom>().path;
            }
            spawned = true;
        }
        if (templates.rooms.Count >= 15)
        {
            if (openingDirection == 1)
            {
                if(!spawned)
                    Instantiate(templates.closedRoom, transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                if (!spawned)
                    Instantiate(templates.closedRoom, transform.position, templates.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                if (!spawned)
                    Instantiate(templates.closedRoom, transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                if (!spawned)
                    Instantiate(templates.closedRoom, transform.position, templates.rightRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
