using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager> {
    public GameObject bullet;
   
    public GameObject sword;
    public static BattleManager instance = null;
    public ObjectPool Pool;



    public int humanPlayerBulletDirX = 0;              // 플레이어 공격 방향
    public int humanPlayerBulletDirY = 1;
   
    private GameObject player;





    private void Awake()
    {
        instance = this;
        Pool = GetComponent<ObjectPool>();      // 이것 땜에 고생했다;;;
    }
    // Use this for initialization

    void Start () {

       
		
	}
   
    void Update () {


	}
    public void HumanPlayerChop(int ver, int hor)
    {
        player = GameObject.FindWithTag("Player");
        humanPlayerBulletDirY = ver; humanPlayerBulletDirX = hor;
        Instantiate(bullet, player.transform.position, Quaternion.identity);

    }
    public void SlimePlayerChop(int ver, int hor)
    {
        player = GameObject.FindWithTag("Player");
        humanPlayerBulletDirY = ver; humanPlayerBulletDirX = hor;
        Instantiate(sword, player.transform.position, Quaternion.identity);
    }
    
}
