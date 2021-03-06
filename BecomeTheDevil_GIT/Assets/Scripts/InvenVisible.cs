﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InvenVisible : MonoBehaviour {

    public static bool isInvenOpen = false;   //아이템창이 열려 있으면 true, 아니면 false
    public GameObject inven;    //아이템창
    public AudioClip closeInventorySound;
    public AudioClip openInventorySound;
    public AudioSource audio;
    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isInvenOpen)
            {
                audio.PlayOneShot(openInventorySound);
                OpenInven();
            }
            else
            {
                audio.PlayOneShot(closeInventorySound);
                CloseInven();
            }
        }

        if (!inven.activeSelf)
        {
            isInvenOpen = false;
        }
    }

    void OpenInven()
    {
        inven.SetActive(true);  //인벤토리가 보이게

        isInvenOpen = true;
    }

    void CloseInven()
    {
        inven.SetActive(false); //인벤토리 감추기

        isInvenOpen = false;
    }
}
