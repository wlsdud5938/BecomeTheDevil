using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statu : MonoBehaviour {
    public float movoingSpeed = 1f;  //이동속도
    public float attackDamage = 10f;  //공격력
    public float attackRange = 10f;   //공격 사거리
    public float attackSpeed = 1f;   //공격 속도
    public float HP = 100;            //최대 hp
    public float currentHP;            //현재 hp

	// Use this for initialization
	void Start () {
        currentHP = 60;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void TakeDamage(float otherDamage)
    {
        currentHP -= otherDamage;
    }
}
