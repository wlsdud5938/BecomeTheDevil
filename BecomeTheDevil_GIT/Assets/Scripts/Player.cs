using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MovingUnit {
    public int countItem = 0;
    public bool haveKey = false;



    bool isHuman=true;
    Statu statu; // 스탯 
    int cur_hor=0, cur_ver=1; // 총쏠 방향 결정하기 위함
    enum Direction {FRONT,RIGHT,BACK,LEFT,FRONTLEFT,FRONTRIGHT,BACKRIGHT,BACKLEFT};
    float attTimer; //어택 타이머
    Animator animator;
   





    protected override  void Start () {
        statu = GetComponent<Statu>();
        animator = GetComponent<Animator>();
        animator.SetFloat("DirX", cur_hor);
        animator.SetFloat("DirY", cur_ver);
        animator.SetBool("isHuman", isHuman);
        base.Start();
        attTimer += 0.0f;
    }

	
	// Update is called once per frame
	void Update () {
        int horizontal = 0;
        int vertical = 0;
        attTimer += Time.deltaTime;
        animator.SetBool("isIdle", true);
        animator.SetBool("isMoving", false);


        horizontal = (int)Input.GetAxisRaw("Horizontal"); 
        vertical = (int)Input.GetAxisRaw("Vertical");



        if (Input.GetKeyDown(KeyCode.F)&&attTimer>statu.attackSpeed)
        {
            attTimer = 0;
            animator.SetTrigger("Chop");
            animator.SetBool("isIdle", false);
            if (isHuman)
            {
                BattleManager.instance.HumanPlayerChop(cur_ver, cur_hor);
            }

            else
            {
                BattleManager.instance.SlimePlayerChop( cur_ver, cur_hor);
            }
            

        }
        if (Input.GetKeyDown("space"))
        {
            isHuman = !isHuman;
            animator.SetTrigger("Change");
            animator.SetBool("isIdle", false);
            animator.SetBool("isHuman", isHuman);
        }
        if (horizontal != 0 || vertical != 0)
        {
            cur_hor = horizontal;
            cur_ver = vertical;
            animator.SetFloat("DirX", cur_hor);
            animator.SetFloat("DirY", cur_ver);
            animator.SetBool("isMoving", true);
            animator.SetBool("isIdle", false);

            AttemptMove(horizontal * statu.movoingSpeed * Time.deltaTime, vertical * statu.movoingSpeed * Time.deltaTime);

        }

       
    }
   

    protected override void AttemptMove(float xDir, float yDir){ 
        base.AttemptMove(xDir, yDir);
    }



}
