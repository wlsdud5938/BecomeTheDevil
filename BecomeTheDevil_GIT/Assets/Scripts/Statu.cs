using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statu : MonoBehaviour {
    public float movoingSpeed = 1f;  //이동속도
    public float attackDamage = 10f;  //공격력
    public float attackRange = 10f;   //공격 사거리
    public float attackSpeed = 1f;   //공격 속도
    public float maxHP = 100;            //최대 hp
    public float currentHP;            //현재 hp
    public Image healthBarFilled;    // 현재 체력 Image.
    public int versionType;          // 적 version

    // Use this for initialization
    void Awake () {
        maxHP = GameManager.Instance.maxHpOfEnemy[versionType];
        healthBarFilled.fillAmount = (float)currentHP / maxHP;
        healthBarFilled.fillAmount = 1;
    }
	
	// Update is called once per frame
	void Update () {
        //TakeDamage(0.1f); // 실험용.
        if (currentHP <= 0)
            Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        healthBarFilled.fillAmount = (float)currentHP / maxHP;
    }


}
