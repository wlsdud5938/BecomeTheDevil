using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Camera camera1;
    public Camera camera2;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //don't forget to set one as active either in the Start() method 
            //or deactivate 1 camera in the Editor before playing 
            if (camera1.gameObject.active == true)
            {
                camera1.gameObject.SetActive(false);
                camera2.gameObject.SetActive(true);
            }

            else
            {
                camera1.gameObject.SetActive(true);
                camera2.gameObject.SetActive(false);
            }
        }
    }
}
