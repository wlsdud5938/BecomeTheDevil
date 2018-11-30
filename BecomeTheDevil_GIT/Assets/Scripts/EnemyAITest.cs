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
    public RoomTemplates temp;
    bool isCopy = false;
    public GameObject nowRoom;
    Vector3 w;

    private void Start()
    {
        temp = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        //map = GameObject.FindGameObjectWithTag("Rooms").GetComponent<MapPath>();
        nowRoom = temp.currentMapnode;
    }
    void Update()
    {
        if (temp.spawnedBoss && isCopy == false)
        {
            enemyPathDoor = temp.enemyPathDoor.ToList();
            enemyPath = temp.enemyPath.ToList();
            isCopy = true;
        }
        if (isMove)
        {
            isMove = false;
            gameObject.AddComponent<NavMeshAgent2D>();

        }
        /*if (map.currentMapnode.transform.Find("Path"))
        {
            path = map.currentMapnode.transform.Find("Path").gameObject;
            w = map.currentMapnode.transform.Find("Path").gameObject.transform.position;
            GetComponent<NavMeshAgent2D>().destination = w;
            //Debug.Log(w.ToString());
        }*/      
        if (enemyPathDoor[0])
        {
            GetComponent<NavMeshAgent2D>().destination = enemyPathDoor[0].GetComponent<BoxCollider2D>().bounds.center;
            //Debug.Log(w.ToString());
        }

    }

}