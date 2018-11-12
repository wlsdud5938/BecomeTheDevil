using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    // Use this for initialization
    private bool isOn = false;
    private RoomTemplates templates;


    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isOn == false && templates.spawnedBoss == true)
        {
            transform.Find("MiniMapCam").gameObject.SetActive(true);
            isOn = true;
        }
    }
}
