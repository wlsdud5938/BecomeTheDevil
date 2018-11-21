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

    public GameObject[] TBLR;
    public GameObject[] TLR;
    public GameObject[] TBL;
    public GameObject[] TBR;
    public GameObject[] TB;
    public GameObject[] TL;
    public GameObject[] TR;
    public GameObject[] T;
    public GameObject[] BLR;
    public GameObject[] BL;
    public GameObject[] BR;
    public GameObject[] B;
    public GameObject[] LR;
    public GameObject[] L;
    public GameObject[] R;
    

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
                        /*Instantiate(boss, rooms[i].transform.position, Quaternion.identity);

                        int targetIndex = Random.Range(1, rooms.Count - 2);
                        Instantiate(potal, rooms[targetIndex].transform.position, Quaternion.identity);
                        potalPosition = targetIndex;*/
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
            currentMapnode.GetComponent<MapNode>().realMap.transform.Find("MapCamera").gameObject.SetActive(true);
        }
        if (doorTrigger == true)
            doorTrigger = false;
    }

    public void ChangeCurrentRoom(int doorNum, GameObject other)
    {
        if (doorNum == 1)
        {
            currentMapnode.GetComponent<MapNode>().realMap.transform.Find("MapCamera").gameObject.SetActive(false);
            currentMapnode.GetComponent<MapNode>().upNode.GetComponent<MapNode>().realMap.transform.Find("MapCamera").gameObject.SetActive(true);
            currentMapnode = currentMapnode.GetComponent<MapNode>().upNode;
            other.transform.position = new Vector3(currentMapnode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("BDoor").transform.position.x
                , currentMapnode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("BDoor").transform.position.y + 1.5F, 0);
        }
        else if (doorNum == 2)
        {
            currentMapnode.GetComponent<MapNode>().realMap.transform.Find("MapCamera").gameObject.SetActive(false);
            currentMapnode.GetComponent<MapNode>().downNode.GetComponent<MapNode>().realMap.transform.Find("MapCamera").gameObject.SetActive(true);
            currentMapnode = currentMapnode.GetComponent<MapNode>().downNode;
            other.transform.position = new Vector3(currentMapnode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("TDoor").transform.position.x
                , currentMapnode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("TDoor").transform.position.y - 1.5F, 0);
        }
        else if (doorNum == 3)
        {
            currentMapnode.GetComponent<MapNode>().realMap.transform.Find("MapCamera").gameObject.SetActive(false);
            currentMapnode.GetComponent<MapNode>().leftNode.GetComponent<MapNode>().realMap.transform.Find("MapCamera").gameObject.SetActive(true);
            currentMapnode = currentMapnode.GetComponent<MapNode>().leftNode;
            other.transform.position = new Vector3(currentMapnode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("RDoor").transform.position.x - 1
                , currentMapnode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("RDoor").transform.position.y, 0);
        }
        else if (doorNum == 4)
        {
            currentMapnode.GetComponent<MapNode>().realMap.transform.Find("MapCamera").gameObject.SetActive(false);
            currentMapnode.GetComponent<MapNode>().rightNode.GetComponent<MapNode>().realMap.transform.Find("MapCamera").gameObject.SetActive(true);
            currentMapnode = currentMapnode.GetComponent<MapNode>().rightNode;
            other.transform.position = new Vector3(currentMapnode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("LDoor").transform.position.x + 1
                , currentMapnode.GetComponent<MapNode>().realMap.transform.Find("07_DoorTrigger").gameObject.transform.Find("LDoor").transform.position.y, 0);
        }
    }
}
