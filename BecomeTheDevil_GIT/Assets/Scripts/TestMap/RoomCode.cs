using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCode : MonoBehaviour {
    public int mapCode;
    public RoomTemplates  temp;
    public bool inPlayer = false;
    public bool inUnit = false;
    // Use this for initialization
    void Start () {
        temp = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        transform.Find("MapCamera").gameObject.SetActive(false);
    }
    

}
