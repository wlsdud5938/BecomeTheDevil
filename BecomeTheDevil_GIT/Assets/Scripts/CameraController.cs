﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Camera camera1;
    public Camera camera2;
    public bool camera11;
    public bool camera22;
    // Use this for initialization
    void Start () {
        camera2.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Tab))
        {
            //don't forget to set one as active either in the Start() method 
            //or deactivate 1 camera in the Editor before playing 
            if (camera1.gameObject.activeSelf == true)
            {
                camera1.gameObject.SetActive(false);
                camera2.gameObject.SetActive(true);
            }

            else
            {
                camera1.gameObject.SetActive(true);
                camera2.gameObject.SetActive(false);
            }
            camera11 = camera1.gameObject.activeSelf;
            camera22 = camera2.gameObject.activeSelf;

        }
    }
}
