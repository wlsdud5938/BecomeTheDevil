using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTemplates : MonoBehaviour
{
    public List<Transform> centerPoints;
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject closedRoom;
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
    private Vector3 playerPosition;
    void Awake()
    {
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
                        SceneManager.LoadScene("ChoiJinyoung");
                    else
                    {
                        Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                        playerPosition.Set(rooms[i].transform.position.x + 2, rooms[i].transform.position.y, rooms[i].transform.position.z);
                        Instantiate(player, playerPosition, Quaternion.identity);
                        spawnedBoss = true;
                    }
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
        if(spawnedBoss == true && spawnedItem==false)
        {
            int targetIndex = Random.Range(1, rooms.Count-2);
            Instantiate(items, rooms[targetIndex].transform.position, Quaternion.identity);

            spawnedItem = true;
        }
        if (spawnedBoss == true && spawnedKeyItem == false && timer > 180.0f)
        {
            int targetIndex = Random.Range(1, rooms.Count - 2);
            Instantiate(keyItems, rooms[targetIndex].transform.position, Quaternion.identity);

            spawnedKeyItem = true;
        }
    }
}
