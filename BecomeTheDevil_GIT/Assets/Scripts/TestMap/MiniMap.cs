using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    // Use this for initialization
    private bool isOn = false;
    private RoomTemplates templates;
    public bool zoom = false;

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
            transform.Find("BigMiniMapCam").gameObject.SetActive(true);

            isOn = true;
        }
        if(Input.GetKeyDown(KeyCode.M) && zoom == false)
        {
            transform.Find("Canvas").transform.Find("bMini").gameObject.SetActive(true);
            zoom = true;
        }
        else if((Input.GetKeyDown(KeyCode.M) && zoom == true))
        {
            transform.Find("Canvas").transform.Find("bMini").gameObject.SetActive(false);
            zoom = false;
        }

    }
}
