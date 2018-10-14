using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUI : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //don't forget to set one as active either in the Start() method 
            //or deactivate 1 camera in the Editor before playing 
            if (gameObject.transform.GetChild(0).gameObject.activeSelf == true)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }

            else
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
