using UnityEngine;
using System.Collections;

public class EnemyAITest : MonoBehaviour
{
    public bool isMove = false;
    MapPath map;

    private void Start()
    {
        map = GameObject.FindGameObjectWithTag("Rooms").GetComponent<MapPath>();

    }
    void Update()
    {
        if (map.currentMapnode.transform.Find("Path"))
        {
            Vector3 w = map.currentMapnode.transform.Find("Path").gameObject.transform.position;
            GetComponent<NavMeshAgent2D>().destination = w;
            Debug.Log(w.ToString());
        }
        if (isMove)
        {
            isMove = false;
            gameObject.AddComponent<NavMeshAgent2D>();

        }
    }

}