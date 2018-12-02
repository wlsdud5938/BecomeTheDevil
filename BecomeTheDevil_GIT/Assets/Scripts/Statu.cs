﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statu : MonoBehaviour {
    public float movoingSpeed = 1f;  //이동속도
    public float attackDamage = 10f;  //공격력
    public float attackRange = 10f;   //공격 사거리
    public float attackSpeed = 1f;   //공격 속도
    public float attackCoolTime = 1f;
    public Slider HPSlider;
    public float maxHP = 100;            //최대 hp
    public  int versionType;          // tag==Enemy인 경우에만 사용. gameManager에서 instantiate할때 초기화
    public float currentHP;          // 현재 채력 

    // 애니메이션의 pivot이 bottom center로 잡혀있어서 오브젝트의 변경된 중심점.
    //public Vector3 offset = new Vector3(0, 0.5f, 0);
    //public Vector3 changeTransform;     

    public Transform middlePoint;       // transform.position 대신 이걸 쓰도록.
    bool isPlayer = false;

 
    bool isEnemy;             //tag가 적인지 확인
    GameManager gameManager; //코드량 줄이기위해 instance 캐싱
    RoomTemplates temp;

    void Awake () 
    {
        gameManager = GameManager.Instance;
        isEnemy = CompareTag("Enemy");
        isPlayer = CompareTag("Player");
        temp = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        if (isEnemy)
        {
            //gameManager.currNumOfEnemyes++;
            maxHP = gameManager.maxHpOfEnemy[versionType]; //버전에 맞는 hp 초기화
            maxHP += gameManager.idxOfWave * (maxHP * gameManager.levelBalanceConst - maxHP); //최대 체력 levelbalance와 웨이브에 맞게 올려
            attackDamage += gameManager.idxOfWave * (attackDamage * gameManager.levelBalanceConst - attackDamage);
        }//에너미인 경우, 웨이브 라운드에 숫자에 따라 강해짐 최대체력이 달라짐
        currentHP = maxHP;


        if (!isPlayer)
        {
            HPSlider.maxValue = maxHP;
            HPSlider.value = currentHP;
        }
    }
	
	// Update is called once per frame
	void Update () {
       if(isPlayer)
        {
            HPSlider = GameObject.FindGameObjectWithTag("PlayerSlider").GetComponent<Slider>();
            HPSlider.maxValue = maxHP;

            HPSlider.value = currentHP;
            isPlayer = false;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        HPSlider.value = currentHP;
        if (currentHP <= 0)
        {
            if (isEnemy)
            {
                gameManager.currNumOfEnemyes--;
                if (gameManager.currNumOfEnemyes <= 0)
                {
                    gameManager.currNumOfEnemyes = 0;
                    gameManager.ClosePotal();
                }

                int random = Random.Range(0, 10);
                if (random <= 2)
                {
                    gameObject.GetComponent<Enemy>().DropItem();
                }
                transform.parent.GetComponent<EnemyAITest>().Destroy();
            }
            if (CompareTag("Unit"))
            {
                gameObject.GetComponent<UnitRealRoom>().realRoom.GetComponent<RoomCode>().units.Remove(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
