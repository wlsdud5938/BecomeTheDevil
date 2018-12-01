using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCode : MonoBehaviour {
    public int mapCode;
    public RoomTemplates  temp;
    public bool inPlayer = false;
    public int inUnit = 0;
    public List<GameObject> units;
    // Use this for initialization
    void Start () {
        temp = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        transform.Find("MapCamera").gameObject.SetActive(false);
    }
    

}
