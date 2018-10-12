using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    public GameObject bullet;
    public GameObject player;
    public static BattleManager instance = null;

    public int humanPlayerBulletDamage = 20;      // 플레이어 공격력
    public const float humanPlayerBulletSpeed = 0.45f;  // 플레이어 총알 속도
    public const float humanPlayerChopSpeed = 0.5f;    //플레이어 공격 속도
    public int humanPlayerBulletDirX = 0;              // 플레이어 공격 방향
    public int humanPlayerBulletDirY = 1;
    

    private float timer;
    


    private void Awake()
    {
        instance = this;

    }
    // Use this for initialization

    void Start () {
        timer += 0.0f;

		
	}
   
    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;

	}
    public void HumanPlayerChop(int ver, int hor)
    {
        if (timer > humanPlayerChopSpeed) { 
            humanPlayerBulletDirY = ver;
            humanPlayerBulletDirX = hor;
            timer = 0; //player 공격 막기위함
            Instantiate(bullet, player.transform.position, Quaternion.identity);
        }
 
    }



}
