using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    public GameObject bullet;
    public GameObject player;
    public GameObject sword;
    public static BattleManager instance = null;

 

    public int humanPlayerBulletDirX = 0;              // 플레이어 공격 방향
    public int humanPlayerBulletDirY = 1;
    


    


    private void Awake()
    {
        instance = this;

    }
    // Use this for initialization

    void Start () {
       

		
	}
   
    void Update () {


	}
    public void HumanPlayerChop(int ver, int hor)
    {
        humanPlayerBulletDirY = ver; humanPlayerBulletDirX = hor;
        Instantiate(bullet, player.transform.position, Quaternion.identity);

    }
    public void SlimePlayerChop(int ver, int hor)
    {
        humanPlayerBulletDirY = ver; humanPlayerBulletDirX = hor;
        Instantiate(sword, player.transform.position, Quaternion.identity);
    }



}
