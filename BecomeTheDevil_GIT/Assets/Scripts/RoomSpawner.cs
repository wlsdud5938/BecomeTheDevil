using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSpawner : MonoBehaviour
{

    public int openingDirection;
    private GameObject room;
    private GameObject parentroom;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door

    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;

    public float waitTime = 1f;
    public bool isPass = false;

    void Start()
    {
        parentroom = transform.parent.transform.parent.gameObject;
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
                Maching(room);

            }
            else if (openingDirection == 2)
            {
                // Need to spawn a room with a TOP door.
                rand = Random.Range(0, templates.topRooms.Length);
                room = Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                Maching(room);

            }
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a LEFT door.
                rand = Random.Range(0, templates.leftRooms.Length);
                room = Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                Maching(room);

            }
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a RIGHT door.
                rand = Random.Range(0, templates.rightRooms.Length);
                room = Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                Maching(room);

            }
            spawned = true;
        }
        else        //10개 이후 맵을 닫아 주는 방 생성
        {
            if (openingDirection == 1)
            {
                // Need to spawn a room with a BOTTOM door.
                room = Instantiate(templates.closedRoom_Bottom, transform.position, templates.closedRoom_Bottom.transform.rotation);
                Maching(room);

            }
            else if (openingDirection == 2)
            {
                // Need to spawn a room with a TOP door.
                room = Instantiate(templates.closedRoom_Top, transform.position, templates.closedRoom_Top.transform.rotation);
                Maching(room);

            }
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a LEFT door.
                room = Instantiate(templates.closedRoom_Left, transform.position, templates.closedRoom_Left.transform.rotation);
                Maching(room);

            }
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a RIGHT door.
                room = Instantiate(templates.closedRoom_Right, transform.position, templates.closedRoom_Right.transform.rotation);
                Maching(room);

            }
            spawned = true;
        }
        NodeSpawn();
        parentroom.GetComponent<AddRoom>().centry = templates.rooms.Count;

    }

    void NodeSpawn()
    {
        if (openingDirection == 2)
        {
            if (parentroom.GetComponent<MapNode>().downNode != null && templates.nodeTable[parentroom.GetComponent<MapNode>().x, parentroom.GetComponent<MapNode>().y + 1] == 1)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            parentroom.GetComponent<MapNode>().downNode = room;
            room.GetComponent<MapNode>().upNode = parentroom.gameObject;
            room.GetComponent<MapNode>().y = parentroom.GetComponent<MapNode>().y + 1;
            room.GetComponent<MapNode>().x = parentroom.GetComponent<MapNode>().x;
            templates.nodeTable[room.GetComponent<MapNode>().x, room.GetComponent<MapNode>().y]++;
            if (templates.nodeTable[room.GetComponent<MapNode>().x, room.GetComponent<MapNode>().y] > 1)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (openingDirection == 1)
        {
            if (parentroom.GetComponent<MapNode>().upNode != null && templates.nodeTable[parentroom.GetComponent<MapNode>().x, parentroom.GetComponent<MapNode>().y - 1] == 1)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            parentroom.GetComponent<MapNode>().upNode = room;
            room.GetComponent<MapNode>().downNode = parentroom.gameObject;
            room.GetComponent<MapNode>().y = parentroom.GetComponent<MapNode>().y - 1;
            room.GetComponent<MapNode>().x = parentroom.GetComponent<MapNode>().x;
            templates.nodeTable[room.GetComponent<MapNode>().x, room.GetComponent<MapNode>().y]++;
            if (templates.nodeTable[room.GetComponent<MapNode>().x, room.GetComponent<MapNode>().y] > 1)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (openingDirection == 4)
        {
            if (parentroom.GetComponent<MapNode>().leftNode != null && templates.nodeTable[parentroom.GetComponent<MapNode>().x - 1, parentroom.GetComponent<MapNode>().y] == 1)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            parentroom.GetComponent<MapNode>().leftNode = room;
            room.GetComponent<MapNode>().rightNode = parentroom.gameObject;
            room.GetComponent<MapNode>().y = parentroom.GetComponent<MapNode>().y;
            room.GetComponent<MapNode>().x = parentroom.GetComponent<MapNode>().x - 1;
            templates.nodeTable[room.GetComponent<MapNode>().x, room.GetComponent<MapNode>().y]++;
            if (templates.nodeTable[room.GetComponent<MapNode>().x, room.GetComponent<MapNode>().y] > 1)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (openingDirection == 3)
        {
            if (parentroom.GetComponent<MapNode>().rightNode != null && templates.nodeTable[parentroom.GetComponent<MapNode>().x + 1, parentroom.GetComponent<MapNode>().y] == 1)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            parentroom.GetComponent<MapNode>().rightNode = room;
            room.GetComponent<MapNode>().leftNode = parentroom.gameObject;
            room.GetComponent<MapNode>().y = parentroom.GetComponent<MapNode>().y;
            room.GetComponent<MapNode>().x = parentroom.GetComponent<MapNode>().x + 1;
            templates.nodeTable[room.GetComponent<MapNode>().x, room.GetComponent<MapNode>().y]++;
            if (templates.nodeTable[room.GetComponent<MapNode>().x, room.GetComponent<MapNode>().y] > 1)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    void Maching(GameObject room)
    {
        if(room.GetComponent<MapNode>() != null && room.GetComponent<MapNode>().mapCode == 1)
        {
            rand = Random.Range(0, templates.R.Length);
            room.GetComponent<MapNode>().realMap = templates.R[rand];
        }
        else if (room.GetComponent<MapNode>().mapCode == 10)
        {
            rand = Random.Range(0, templates.L.Length);
            room.GetComponent<MapNode>().realMap = templates.L[rand];
        }
        else if (room.GetComponent<MapNode>().mapCode == 11)
        {
            rand = Random.Range(0, templates.LR.Length);
            room.GetComponent<MapNode>().realMap = templates.LR[rand];
        }
        else if (room.GetComponent<MapNode>().mapCode == 100)
        {
            rand = Random.Range(0, templates.B.Length);
            room.GetComponent<MapNode>().realMap = templates.B[rand];
        }
        else if (room.GetComponent<MapNode>().mapCode == 101)
        {
            rand = Random.Range(0, templates.BR.Length);
            room.GetComponent<MapNode>().realMap = templates.BR[rand];
        }
        else if (room.GetComponent<MapNode>().mapCode == 110)
        {
            rand = Random.Range(0, templates.BL.Length);
            room.GetComponent<MapNode>().realMap = templates.BL[rand];
        }
        else if (room.GetComponent<MapNode>().mapCode == 111)
        {
            rand = Random.Range(0, templates.BLR.Length);
            room.GetComponent<MapNode>().realMap = templates.BLR[rand];
        }
        else if (room.GetComponent<MapNode>().mapCode == 1000)
        {
            rand = Random.Range(0, templates.T.Length);
            room.GetComponent<MapNode>().realMap = templates.T[rand];
        }
        else if (room.GetComponent<MapNode>().mapCode == 1001)
        {
            rand = Random.Range(0, templates.TR.Length);
            room.GetComponent<MapNode>().realMap = templates.TR[rand];
        }
        else if (room.GetComponent<MapNode>().mapCode == 1010)
        {
            rand = Random.Range(0, templates.TL.Length);
            room.GetComponent<MapNode>().realMap = templates.TL[rand];
        }
        else if (room.GetComponent<MapNode>().mapCode == 1011)
        {
            rand = Random.Range(0, templates.TLR.Length);
            room.GetComponent<MapNode>().realMap = templates.TLR[rand];
        }
        else if (room.GetComponent<MapNode>().mapCode == 1100)
        {
            rand = Random.Range(0, templates.TB.Length);
            room.GetComponent<MapNode>().realMap = templates.TB[rand];
        }
        else if (room.GetComponent<MapNode>().mapCode == 1101)
        {
            rand = Random.Range(0, templates.TBR.Length);
            room.GetComponent<MapNode>().realMap = templates.TBR[rand];
        }
        else if (room.GetComponent<MapNode>().mapCode == 1110)
        {
            rand = Random.Range(0, templates.TBL.Length);
            room.GetComponent<MapNode>().realMap = templates.TBL[rand];
        }
        else if (room.GetComponent<MapNode>().mapCode == 1111)
        {
            rand = Random.Range(0, templates.TBLR.Length);
            room.GetComponent<MapNode>().realMap = templates.TBLR[rand];
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            spawned = true;
        }
        if (other.CompareTag("CenterPoint"))
        {
            if (openingDirection == 2)
            {
                if (!other.transform.parent.GetComponent<MapNode>().openUp)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
            else if (openingDirection == 1)
            {
                if (!other.transform.parent.GetComponent<MapNode>().openDown)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
            else if (openingDirection == 4)
            {
                if (!other.transform.parent.GetComponent<MapNode>().openRight)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
            else if (openingDirection == 3)
            {
                if (!other.transform.parent.GetComponent<MapNode>().openLeft)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
        }
            spawned = true;
        Destroy(gameObject);
    }
}
