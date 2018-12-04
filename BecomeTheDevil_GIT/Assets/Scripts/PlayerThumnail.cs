using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerThumnail : MonoBehaviour {

    public Sprite[] thumnail_sprites; // 1: 인간형 와꾸 0: 슬라임형 와꾸


    Image thumnail;
    bool isHuman = true;
    bool find = false;
    Player player;
    RoomTemplates temp;
    // Use this for initialization
    void Start () {
        temp = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        thumnail = GetComponent<Image>();
        thumnail.sprite = thumnail_sprites[1];
    }
	
	// Update is called once per frame
    void Update (){
        if(temp.spawnedBoss == true && !find)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            find = true;
        }
        if (Input.GetKeyDown("space"))
        {

            isHuman = player.isHuman;
            int idx = isHuman ? 1 : 0;
            thumnail.sprite = thumnail_sprites[idx];
        }
    }
}
