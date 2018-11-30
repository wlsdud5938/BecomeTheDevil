using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyAITest : MonoBehaviour
{
    public bool isMove = false;
    public GameObject path;
    public List<GameObject> enemyPathDoor;
    public List<GameObject> enemyPath;
    public GameManager manager;
    public RoomTemplates temp;
    bool isCopy = false;
    public GameObject nowRoom;
    public Vector2 w;
    public GameObject player;
    public bool findPlayer = false;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        temp = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        //map = GameObject.FindGameObjectWithTag("Rooms").GetComponent<MapPath>();
        nowRoom = temp.enemyPath[0];
    }
    void Update()
    {
        if (temp.spawnedBoss && isCopy == false)
        {
            enemyPathDoor = temp.enemyPathDoor.ToList();
            enemyPath = temp.enemyPath.ToList();
            player = GameObject.FindGameObjectWithTag("Player");
            isCopy = true;
        }
        if (isMove)
        {
            isMove = false;
            gameObject.AddComponent<NavMeshAgent2D>();

        }
        if(nowRoom.GetComponent<MapNode>().realMap.GetComponent<RoomCode>().inPlayer)
        {
            GetComponent<NavMeshAgent2D>().destination = player.transform.position;
            findPlayer = true;
        } 
        if (!nowRoom.GetComponent<MapNode>().realMap.GetComponent<RoomCode>().inPlayer && enemyPathDoor[0])
        {
            w = new Vector2 (enemyPathDoor[0].GetComponent<BoxCollider2D>().bounds.center.x, enemyPathDoor[0].GetComponent<BoxCollider2D>().bounds.center.y);
            GetComponent<NavMeshAgent2D>().destination = w;
            findPlayer = false;
            //Debug.Log(w.ToString());
        }

    }

}