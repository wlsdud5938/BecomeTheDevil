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
        Pool = GetComponent<ObjectPool>();      // Pool 에 BattleManager의 컴포넌트인 ObjectPool 스크립트를 초기화 시켜줌.
    }
    // Use this for initialization

    void Start () {

       
		
	}
   
    void Update () {


	}
    
    
    
}
