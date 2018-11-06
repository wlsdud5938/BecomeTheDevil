using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public float waitTime = 1f;

    void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }


    void Spawn()
    {
        if (spawned == false && templates.rooms.Count < 10)     //10개 이전까지 랜덤생성
        {
            if (openingDirection == 1)
            {
                // Need to spawn a room with a BOTTOM door.
                rand = Random.Range(0, templates.bottomRooms.Length);
                room = Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                
            }
            else if (openingDirection == 2)
            {
                // Need to spawn a room with a TOP door.
                rand = Random.Range(0, templates.topRooms.Length);
                room = Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a LEFT door.
                rand = Random.Range(0, templates.leftRooms.Length);
                room = Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a RIGHT door.
                rand = Random.Range(0, templates.rightRooms.Length);
                room = Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            room.GetComponent<AddRoom>().centry = templates.rooms.Count;
            spawned = true;
        }
        else        //10개 이후 맵을 닫아 주는 방 생성
        {
            if (openingDirection == 1)
            {
                // Need to spawn a room with a BOTTOM door.
                room = Instantiate(templates.closedRoom_Bottom, transform.position, templates.closedRoom_Bottom.transform.rotation);
            }
            else if (openingDirection == 2)
            {
                // Need to spawn a room with a TOP door.
                room = Instantiate(templates.closedRoom_Top, transform.position, templates.closedRoom_Top.transform.rotation);
            }
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a LEFT door.
                room = Instantiate(templates.closedRoom_Left, transform.position, templates.closedRoom_Left.transform.rotation);
            }
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a RIGHT door.
                room = Instantiate(templates.closedRoom_Right, transform.position, templates.closedRoom_Right.transform.rotation);
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("SpawnPoint") || other.CompareTag("CenterPoint") || other.CompareTag("Room"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

                //Destroy(other.gameObject);
               // Destroy(gameObject);
            }
            spawned = true;
        }

        spawned = true;
        Destroy(gameObject);


    }
}
