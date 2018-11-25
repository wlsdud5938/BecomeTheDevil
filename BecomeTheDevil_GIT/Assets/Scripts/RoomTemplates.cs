using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTemplates : MonoBehaviour
{
    public GameObject currentMapnode;
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject closedRoom_Bottom;
    public GameObject closedRoom_Top;
    public GameObject closedRoom_Right;
    public GameObject closedRoom_Left;

    public List <GameObject> TBLR;
    public List <GameObject> TLR;
    public List<GameObject> TBL;
    public List<GameObject> TBR;
    public List<GameObject> TB;
    public List<GameObject> TL;
    public List<GameObject> TR;
    public List<GameObject> T;
    public List<GameObject> BLR;
    public List<GameObject> BL;
    public List<GameObject> BR;
    public List<GameObject> B;
    public List<GameObject> LR;
    public List<GameObject> L;
    public List<GameObject> R;
    public List<GameObject> enemyPath;
    public List<GameObject> enemyPathDoor;

    public GameObject items;
    public GameObject keyItems;
    float timer = 0;

    public List<GameObject> rooms;

    public float waitTime;
    public bool spawnedBoss;
    public bool spawnedKeyItem;
    public bool spawnedItem;
    public GameObject boss;
    public GameObject player;
    public GameObject potal;
    public float keyTime = 180.0f;
    private Vector3 playerPosition;
    private int potalPosition;
    bool debugMd = false;
    public bool doorTrigger = false;
    GameObject nowPlayer;
    public GameObject currentPlayer;
    public GameObject currentBoss;
    public GameObject currentEnemy;
    GameObject bosss;
    bool isPathfind = false;

    public int[,] nodeTable = new int[30,30];

    void Awake()
    {
        nodeTable[15, 15] = 1;
    }
    void Update()
    {

        timer += Time.deltaTime;
        if (waitTime <= 0 && spawnedBoss == false)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 1)
                {
                    if (rooms.Count <= 8)
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    else
                    {
                        Instantiate(boss, rooms[rooms.Count-1].GetComponent<MapNode>().realMap.transform.position, Quaternion.identity);
                        bosss = Instantiate(currentBoss, rooms[rooms.Count-1].transform.position, Quaternion.identity);


                        playerPosition.Set(currentMapnode.GetComponent<MapNode>().realMap.transform.position.x, currentMapnode.GetComponent<MapNode>().realMap.transform.position.y, currentMapnode.GetComponent<MapNode>().realMap.transform.position.z);
                        nowPlayer = Instantiate(player, playerPosition, Quaternion.identity);

                        spawnedBoss = true;
                    }
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
        if(spawnedBoss == true && debugMd == false)     //맵이 이상이없다면 삭제될 구문입니다.
        {
            for(int i =0 ; i < 30 ; i++)
            {
               /* Debug.Log(nodeTable[0,i] + " " + nodeTable[1, i] + " " + nodeTable[2, i] + " " + nodeTable[3, i] + " " +
                nodeTable[4, i] + " " + nodeTable[5, i] + " " + nodeTable[6, i] + " " + nodeTable[7, i] + " " + nodeTable[8, i] + " " +
                    nodeTable[9, i] + " " + nodeTable[10, i] + " " + nodeTable[11, i] + " " + nodeTable[12, i] + " " + nodeTable[13, i] + " " +
                    nodeTable[ 14, i] + " " + nodeTable[15, i] + " " + nodeTable[16, i] + " " + nodeTable[17, i] + " " + nodeTable[18, i] + " " +
                    nodeTable[19, i] + " " + nodeTable[20, i] + " " + nodeTable[21, i] + " " + nodeTable[22, i] + " " + nodeTable[23, i] + " " +
                    nodeTable[24, i] + " " + nodeTable[25, i] + " " + nodeTable[26, i] + " " + nodeTable[27, i] + " " + nodeTable[28, i] + " " +
                    nodeTable[29, i] + " ");*/
            }
            debugMd = true;
        }
        if (spawnedBoss == true )
        {
            currentMapnode.transform.Find("mapimg").GetComponent<SpriteRenderer>().enabled = true;
            currentMapnode.GetComponent<MapNode>().realMap.transform.Find("MapCamera").gameObject.SetActive(true);
            if(currentMapnode == rooms[rooms.Count - 1])
                bosss.GetComponent<SpriteRenderer>().enabled = true;
            currentPlayer.transform.position = currentMapnode.transform.position;
        }
        if (doorTrigger == true)
            doorTrigger = false;
        if(spawnedBoss == true && isPathfind == false)
        {
            FindPath();
            isPathfind = true;
        }

    }

    void FindPath()
    {
        GameObject bossRoom = rooms[rooms.Count - 1];
        GameObject currentRoom = bossRoom;
        enemyPath.Add(bossRoom);
        int nowCentry = bossRoom.GetComponent<AddRoom>().realCentry;
        while (nowCentry != 0)
        {
            if (currentRoom.GetComponent<MapNode>().upNode != null && currentRoom.GetComponent<MapNode>().upNode.GetComponent<AddRoom>().realCentry == nowCentry - 1)
            {
                enemyPath.Insert(0, currentRoom.GetComponent<MapNode>().upNode);
                enemyPathDoor.Insert(0, currentRoom.GetComponent<MapNode>().upNode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").transform.Find("BDoor").gameObject);
                currentRoom = currentRoom.GetComponent<MapNode>().upNode;
            }
            else if (currentRoom.GetComponent<MapNode>().downNode != null && currentRoom.GetComponent<MapNode>().downNode.GetComponent<AddRoom>().realCentry == nowCentry - 1)
            {
                enemyPath.Insert(0, currentRoom.GetComponent<MapNode>().downNode);
                enemyPathDoor.Insert(0, currentRoom.GetComponent<MapNode>().downNode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").transform.Find("TDoor").gameObject);

                currentRoom = currentRoom.GetComponent<MapNode>().downNode;

            }
            else if (currentRoom.GetComponent<MapNode>().leftNode != null && currentRoom.GetComponent<MapNode>().leftNode.GetComponent<AddRoom>().realCentry == nowCentry - 1)
            {
                enemyPath.Insert(0, currentRoom.GetComponent<MapNode>().leftNode);
                enemyPathDoor.Insert(0, currentRoom.GetComponent<MapNode>().leftNode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").transform.Find("RDoor").gameObject);

                currentRoom = currentRoom.GetComponent<MapNode>().leftNode;

            }
            else if (currentRoom.GetComponent<MapNode>().rightNode != null && currentRoom.GetComponent<MapNode>().rightNode.GetComponent<AddRoom>().realCentry == nowCentry - 1)
            {
                enemyPath.Insert(0, currentRoom.GetComponent<MapNode>().rightNode);
                enemyPathDoor.Insert(0, currentRoom.GetComponent<MapNode>().rightNode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").transform.Find("LDoor").gameObject);

                currentRoom = currentRoom.GetComponent<MapNode>().rightNode;

            }
            nowCentry--;
        }
    }

    public void ChangeCurrentRoom(int doorNum, GameObject other)
    {
        if (doorNum == 1)
        {
            currentMapnode.GetComponent<MapNode>().realMap.transform.Find("MapCamera").gameObject.SetActive(false);
            currentMapnode.GetComponent<MapNode>().upNode.GetComponent<MapNode>().realMap.transform.Find("MapCamera").gameObject.SetActive(true);
            currentMapnode = currentMapnode.GetComponent<MapNode>().upNode;
            other.transform.position = new Vector3(currentMapnode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("BDoor").transform.position.x
                , currentMapnode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("BDoor").transform.position.y - 2F, 0);
        }
        else if (doorNum == 2)
        {
            currentMapnode.GetComponent<MapNode>().realMap.transform.Find("MapCamera").gameObject.SetActive(false);
            currentMapnode.GetComponent<MapNode>().downNode.GetComponent<MapNode>().realMap.transform.Find("MapCamera").gameObject.SetActive(true);
            currentMapnode = currentMapnode.GetComponent<MapNode>().downNode;
            other.transform.position = new Vector3(currentMapnode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("TDoor").transform.position.x
                , currentMapnode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("TDoor").transform.position.y + 2F, 0);
        }
        else if (doorNum == 3)
        {
            currentMapnode.GetComponent<MapNode>().realMap.transform.Find("MapCamera").gameObject.SetActive(false);
            currentMapnode.GetComponent<MapNode>().leftNode.GetComponent<MapNode>().realMap.transform.Find("MapCamera").gameObject.SetActive(true);
            currentMapnode = currentMapnode.GetComponent<MapNode>().leftNode;
            other.transform.position = new Vector3(currentMapnode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("RDoor").transform.position.x + 2f
                , currentMapnode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("RDoor").transform.position.y, 0);
        }
        else if (doorNum == 4)
        {
            currentMapnode.GetComponent<MapNode>().realMap.transform.Find("MapCamera").gameObject.SetActive(false);
            currentMapnode.GetComponent<MapNode>().rightNode.GetComponent<MapNode>().realMap.transform.Find("MapCamera").gameObject.SetActive(true);
            currentMapnode = currentMapnode.GetComponent<MapNode>().rightNode;
            other.transform.position = new Vector3(currentMapnode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("LDoor").transform.position.x - 2f
                , currentMapnode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("LDoor").transform.position.y, 0);
        }
    }
    public void ChangeCurrentRoomEnemy(int doorNum, GameObject other)
    {
        if (doorNum == 1)
        {
            other.GetComponent<EnemyAITest>().nowRoom = other.GetComponent<EnemyAITest>().nowRoom.GetComponent<MapNode>().upNode;
            other.transform.position = new Vector3(other.GetComponent<EnemyAITest>().nowRoom.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("BDoor").transform.position.x
                , other.GetComponent<EnemyAITest>().nowRoom.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("BDoor").transform.position.y/* + 0.3F*/, 0);
        }
        else if (doorNum == 2)
        {
            other.GetComponent<EnemyAITest>().nowRoom = other.GetComponent<EnemyAITest>().nowRoom.GetComponent<MapNode>().downNode;
            other.transform.position = new Vector3(other.GetComponent<EnemyAITest>().nowRoom.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("TDoor").transform.position.x
                , other.GetComponent<EnemyAITest>().nowRoom.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("TDoor").transform.position.y/* - 0.3F*/, 0);
        }
        else if (doorNum == 3)
        {
            other.GetComponent<EnemyAITest>().nowRoom = other.GetComponent<EnemyAITest>().nowRoom.GetComponent<MapNode>().leftNode;
            other.transform.position = new Vector3(other.GetComponent<EnemyAITest>().nowRoom.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("RDoor").transform.position.x/* - 0.3f*/
                , other.GetComponent<EnemyAITest>().nowRoom.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("RDoor").transform.position.y, 0);
        }
        else if (doorNum == 4)
        {
            other.GetComponent<EnemyAITest>().nowRoom = other.GetComponent<EnemyAITest>().nowRoom.GetComponent<MapNode>().rightNode;
            other.transform.position = new Vector3(other.GetComponent<EnemyAITest>().nowRoom.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("LDoor").transform.position.x/* + 0.3f*/
                , other.GetComponent<EnemyAITest>().nowRoom.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("LDoor").transform.position.y, 0);
        }
    }
}
