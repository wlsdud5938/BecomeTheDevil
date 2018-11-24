using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCode : MonoBehaviour {
    public int mapCode;
    public MapPath path;
    // Use this for initialization
    void Start () {
        path = GameObject.FindGameObjectWithTag("Rooms").GetComponent<MapPath>();
    }

    /*private void Update()
    {
        //테스트용입니다.
        if (path.currentMapnode == gameObject)
        {
            transform.Find("MapCamera").gameObject.SetActive(true);
        }
        else
            transform.Find("MapCamera").gameObject.SetActive(false);
    }*/

}
