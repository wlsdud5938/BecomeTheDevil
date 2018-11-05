using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawn : MonoBehaviour
{

    public int openingDirection;
    private GameObject room;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door


    private MapGenerator generator;
    private int rand;
    public bool spawned = false;

    public float waitTime = 0.1f;

    void Start()
    {
        Destroy(gameObject, waitTime);
        generator = GameObject.FindGameObjectWithTag("Map").GetComponent<MapGenerator>();
        Spawn();
    }


    void Spawn()
    {
        if (spawned == false && gameObject.transform.parent.transform.parent.GetComponent<AddRoom>().path == -1)
        {
            if (openingDirection == 1)
            {
                // Need to spawn a room with a BOTTOM door.
                rand = Random.Range(0, generator.bottomRooms.Length);
                room = Instantiate(generator.bottomRooms[rand], transform.position, generator.bottomRooms[rand].transform.rotation);

                room.GetComponent<AddRoom>().path = 1;
            }
            else if (openingDirection == 2)
            {
                // Need to spawn a room with a TOP door.
                rand = Random.Range(0, generator.topRooms.Length);
                room = Instantiate(generator.topRooms[rand], transform.position, generator.topRooms[rand].transform.rotation);
                room.GetComponent<AddRoom>().path = 2;

            }
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a LEFT door.
                rand = Random.Range(0, generator.leftRooms.Length);
                room = Instantiate(generator.leftRooms[rand], transform.position, generator.leftRooms[rand].transform.rotation);
                room.GetComponent<AddRoom>().path = 3;

            }
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a RIGHT door.
                rand = Random.Range(0, generator.rightRooms.Length);
                room = Instantiate(generator.rightRooms[rand], transform.position, generator.rightRooms[rand].transform.rotation);
                room.GetComponent<AddRoom>().path = 4;

            }
            spawned = true;
        }
        if (spawned == false && generator.rooms.Count <= 14)
        {
            if (openingDirection == 1)
            {
                // Need to spawn a room with a BOTTOM door.
                rand = Random.Range(0, generator.bottomRooms.Length);
                room = Instantiate(generator.bottomRooms[rand], transform.position, generator.bottomRooms[rand].transform.rotation);
                room.GetComponent<AddRoom>().path = gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<AddRoom>().path;
            }
            else if (openingDirection == 2)
            {
                // Need to spawn a room with a TOP door.
                rand = Random.Range(0, generator.topRooms.Length);
                room = Instantiate(generator.topRooms[rand], transform.position, generator.topRooms[rand].transform.rotation);
                room.GetComponent<AddRoom>().path = gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<AddRoom>().path;
            }
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a LEFT door.
                rand = Random.Range(0, generator.leftRooms.Length);
                room = Instantiate(generator.leftRooms[rand], transform.position, generator.leftRooms[rand].transform.rotation);
                room.GetComponent<AddRoom>().path = gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<AddRoom>().path;
            }
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a RIGHT door.
                rand = Random.Range(0, generator.rightRooms.Length);
                room = Instantiate(generator.rightRooms[rand], transform.position, generator.rightRooms[rand].transform.rotation);
                room.GetComponent<AddRoom>().path = gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<AddRoom>().path;
            }
            spawned = true;
        }
        if (generator.rooms.Count >= 15)
        {
            if (openingDirection == 1)
            {
                if (!spawned)
                    Instantiate(generator.closedRoom, transform.position, generator.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                if (!spawned)
                    Instantiate(generator.closedRoom, transform.position, generator.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                if (!spawned)
                    Instantiate(generator.closedRoom, transform.position, generator.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                if (!spawned)
                    Instantiate(generator.closedRoom, transform.position, generator.rightRooms[rand].transform.rotation);
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
                Instantiate(generator.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
