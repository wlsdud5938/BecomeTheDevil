using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statu : MonoBehaviour {
    public float movoingSpeed = 1f;  //이동속도
    public float attackDamage = 10f;  //공격력
    public float attackRange = 10f;   //공격 사거리
    public float attackSpeed = 1f;   //공격 속도
    public Image healthBarFilled;    // 현재 체력 Image.
    public float maxHP = 100;            //최대 hp
    public  int versionType;          // tag==Enemy인 경우에만 사용. gameManager에서 instantiate할때 초기화





    float currentHP;          // 현재 채력 
    bool isEnemy;             //tag가 적인지 확인
    GameManager gameManager; //코드량 줄이기위해 instance 캐싱

    void Awake () 
    {
        gameManager = GameManager.Instance;
        isEnemy = CompareTag("Enemy");
        if (isEnemy)
        {
            maxHP = gameManager.maxHpOfEnemy[versionType]; //버전에 맞는 hp 초기화
            maxHP += gameManager.idxOfWave * (maxHP * gameManager.levelBalanceConst - maxHP); //최대 체력 levelbalance와 웨이브에 맞게 올려
            attackDamage += gameManager.idxOfWave * (attackDamage * gameManager.levelBalanceConst - attackDamage);
        }//에너미인 경우, 웨이브 라운드에 숫자에 따라 강해짐 최대체력이 달라짐

        currentHP = maxHP;          // 처음 생성됐을 때 최대 체력을 갖고 태어남.
        healthBarFilled.fillAmount = 1; // 체력바를 가득 채움.
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        healthBarFilled.fillAmount = (float)currentHP / maxHP;
        if (currentHP <= 0)
            Destroy(gameObject);
    }
}
