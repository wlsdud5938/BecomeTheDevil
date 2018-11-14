using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject closedRoom;
    //public GameObject items;
    //public GameObject keyItems;
    //public List<GameObject> lList;
    //public List<GameObject> bList;
    //public List<GameObject> tList;
    //public List<GameObject> rList;
    float timer = 0;

    public int[,] nodeMap = new int[15,15];

    public List<GameObject> rooms;

    //public float waitTime;
    //public bool spawnedBoss;
    //public bool spawnedKeyItem;
    //public bool spawnedItem;
    //public GameObject boss;
    //public GameObject player;
    //public GameObject potal;
    //public float keyTime = 180.0f;
    //private Vector3 playerPosition;
    //private int potalPosition;

    void Awake()
    {
        
    }

    void Update()
    {
    }
}
