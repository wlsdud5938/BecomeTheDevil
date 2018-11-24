using UnityEngine;
using System.Collections;

public class EnemyAITest : MonoBehaviour
{
    public bool isMove = false;
    public GameObject path;
    public MapPath map;
    Vector3 w;

    private void Start()
    {
        map = GameObject.FindGameObjectWithTag("Rooms").GetComponent<MapPath>();

    }
    void Update()
    {
        if (isMove)
        {
            isMove = false;
            gameObject.AddComponent<NavMeshAgent2D>();

        }
        if (map.currentMapnode.transform.Find("Path"))
        {
            path = map.currentMapnode.transform.Find("Path").gameObject;
            w = map.currentMapnode.transform.Find("Path").gameObject.transform.position;
            GetComponent<NavMeshAgent2D>().destination = w;
            //Debug.Log(w.ToString());
        }
        
    }

}