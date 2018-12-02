using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit2AniBroken : MonoBehaviour {
    private Animator animator;
    Statu statu;
    public float percent; // 애니메이션 변화를 위한 기준치 설정 (*4, *3, *2, *1이 되므로 가장 부서진 상태의 에니메이션이 되는 수치를 입력)


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        statu = GetComponent<Statu>();
    }
	
	// Update is called once per frame
	void Update () {
		if (this.statu.currentHP >= (percent*3))
        {
            animator.SetFloat("Hp", 0); // 0, 0.3, 0.6, 1 : 1로 갈 수록 부서진 애니메이션.
        }
        else if (this.statu.currentHP >= (percent * 2) && (this.statu.currentHP < (percent * 3)))
        {
            animator.SetFloat("Hp", 0.33f); // 0, 0.3, 0.6, 1 : 1로 갈 수록 부서진 애니메이션.
        }
        else if (this.statu.currentHP >= (percent * 1) && (this.statu.currentHP < (percent * 2)))
        {
            animator.SetFloat("Hp", 0.66f); // 0, 0.3, 0.6, 1 : 1로 갈 수록 부서진 애니메이션.
        }
        else if (this.statu.currentHP >= (percent * 0) && (this.statu.currentHP < (percent * 1)))
        {
            animator.SetFloat("Hp", 1); // 0, 0.3, 0.6, 1 : 1로 갈 수록 부서진 애니메이션.
        }
    }
}
