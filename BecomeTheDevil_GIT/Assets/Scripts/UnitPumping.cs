using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPumping : MonoBehaviour {
    private Animator animator;
    Statu statu;
    private int rand;
    public int prequency; // 펌핑이 발생할 주기. 1~100까지이며 큰 수치 = 자주 발생.

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        statu = GetComponent<Statu>();
        animator.SetTrigger("Pump"); //처음 한 번 실행.
        InvokeRepeating("Pumping", 0, 5.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void Pumping()
    {
        rand = Random.Range(0, 100);
        if (rand >= prequency)
        {
            animator.SetTrigger("Pump");
        }
            
    }
}
