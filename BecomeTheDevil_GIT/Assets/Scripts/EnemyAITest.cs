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
    public Vector2 target;
    private Animator myAnimator;
    private float tempPosition;
    float dis = 10000f;
    public int idx = 0;
    int j = 0;
    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        temp = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        //map = GameObject.FindGameObjectWithTag("Rooms").GetComponent<MapPath>();
        nowRoom = temp.enemyPath[0];
        tempPosition = transform.position.x;
        myAnimator = transform.Find("Enemy").GetComponent<Animator>();
        myAnimator.SetBool("EnemyRun", true);
        myAnimator.SetBool("EnemyIdle", false);
        /*
        var go = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        agent = go.AddComponent<NavMeshAgent>();
        ProjectTo2D(agent.transform.position)
        transform.position = NavMeshUtils2D.ProjectTo2D(agent.transform.position);*/
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
        if (nowRoom.GetComponent<MapNode>().realMap.GetComponent<RoomCode>().units.Count > 0)
        {
            ClosedTarget();
            dis = 10000;
            idx = 0;
            j = 0;
            findPlayer = true;
        }
        else if(nowRoom.GetComponent<MapNode>().realMap.GetComponent<RoomCode>().inPlayer)
        {
            GetComponent<NavMeshAgent2D>().destination = player.transform.position;
            target = player.transform.position;
            findPlayer = true;

        }
        else if (!nowRoom.GetComponent<MapNode>().realMap.GetComponent<RoomCode>().inPlayer && enemyPathDoor[0])
        {
            w = new Vector2(enemyPathDoor[0].GetComponent<BoxCollider2D>().bounds.center.x, enemyPathDoor[0].GetComponent<BoxCollider2D>().bounds.center.y);
            GetComponent<NavMeshAgent2D>().destination = w;
            target = w;
            findPlayer = false;
            //Debug.Log(w.ToString());
        }
        /*
        Debug.Log(GetComponent<NavMeshAgent2D>().agent.transform.position.x);
        if (GetComponent<NavMeshAgent2D>().agent.transform.position.x > 0)
        {
            myAnimator.SetFloat("Dir", 1);
        }
        else if (GetComponent<NavMeshAgent2D>().agent.transform.position.x < 0)
        {
            myAnimator.SetFloat("Dir", 0);
        }*/
        if (transform.position.x - tempPosition > 0)
        {
            myAnimator.SetFloat("Dir", 1);
        }
        else if(transform.position.x - tempPosition < 0)
        {
            myAnimator.SetFloat("Dir", 0);
        }
        tempPosition = transform.position.x;

    }
    void ClosedTarget()
    {
        if (nowRoom.GetComponent<MapNode>().realMap.GetComponent<RoomCode>().units.Count <= 0)
        {
            return;
        }
        foreach (var i in nowRoom.GetComponent<MapNode>().realMap.GetComponent<RoomCode>().units)
        {

            if (dis > Vector2.Distance(i.transform.position, transform.position))
            {
                if(i.CompareTag("Boss"))
                {
                    dis = Vector2.Distance(i.transform.Find("CoreShadow").transform.position, transform.position);

                }
                else
                    dis = Vector2.Distance(i.transform.position, transform.position);

                idx = j;
                
            }
            j++;
        }
        if (dis > Vector2.Distance(player.transform.position, transform.position) && nowRoom.GetComponent<MapNode>().realMap.GetComponent<RoomCode>().inPlayer)
            GetComponent<NavMeshAgent2D>().destination = player.transform.position;
        else
            GetComponent<NavMeshAgent2D>().destination = nowRoom.GetComponent<MapNode>().realMap.GetComponent<RoomCode>().units[idx].transform.position;

    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}