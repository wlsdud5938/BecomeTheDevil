using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{ 
    public GameObject rightNode = null;
    public GameObject leftNode = null;
    public GameObject upNode = null;
    public GameObject downNode = null;
    public bool openRight = false;
    public bool openLeft = false;
    public bool openUp = false;
    public bool openDown = false;
    public int x;
    public int y;
    public int mapCode;     //TBLR 1은 열림 0은 닫힘
    public GameObject realMap;
    private RoomTemplates templates;


    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

    }

    void Update()
    {

    }
}
