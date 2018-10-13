using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingUnit {
  

    public float movingSpeed = 0.01f;

    public bool haveKey = false;
    public int countItem = 0;
    
    private bool isChop=false,isIdle=true,isHuman=true,isMoving=false,isChange=false;
    private int cur_hor=0, cur_ver=1; // 총쏠 방향 결정하기 위함
    private enum Direction {FRONT,RIGHT,BACK,LEFT,FRONTLEFT,FRONTRIGHT,BACKRIGHT,BACKLEFT};
   

	// Use this for initialization





	protected override  void Start () {
        GetComponent<Animator>().SetFloat("DirX", cur_hor);
        GetComponent<Animator>().SetFloat("DirY", cur_ver);
        GetComponent<Animator>().SetBool("isIdle", isIdle);
        GetComponent<Animator>().SetBool("isHuman", isHuman);
        GetComponent<Animator>().SetBool("isMoving", isMoving);
        GetComponent<Animator>().SetBool("isChange", isChange);
        base.Start();

		
	}

	
	// Update is called once per frame
	void Update () {
        int horizontal = 0;
        int vertical = 0;
        isChop = false;
        isIdle = true;
        isMoving = false;
        isChange = false;
        GetComponent<Animator>().SetBool("isChop", isChop);
        GetComponent<Animator>().SetBool("isIdle", isIdle);
        GetComponent<Animator>().SetBool("isMoving", isMoving);
        GetComponent<Animator>().SetBool("isChange", isChange);


        horizontal = (int)Input.GetAxisRaw("Horizontal"); 
        vertical = (int)Input.GetAxisRaw("Vertical");

    
        if (horizontal != 0 || vertical != 0)
        {
            cur_hor = horizontal;
            cur_ver = vertical;
            isIdle = false;
            isMoving = true;
            GetComponent<Animator>().SetFloat("DirX", cur_hor);
            GetComponent<Animator>().SetFloat("DirY", cur_ver);
            GetComponent<Animator>().SetBool("isIdle", isIdle);
            GetComponent<Animator>().SetBool("isMoving", isMoving);
            AttemptMove(horizontal * movingSpeed, vertical * movingSpeed);

        }
        if (Input.GetMouseButton(0))
        {
            isChop = true;
            isIdle = false;
            GetComponent<Animator>().SetBool("isIdle", isIdle);
            GetComponent<Animator>().SetBool("isChop", isChop);
            if(isHuman) BattleManager.instance.HumanPlayerChop(cur_ver, cur_hor); //current direction의 반대 방향이여야 함
        }
        if(Input.GetKeyDown("space"))
        {

            isHuman = !isHuman;
            isChange = true;
            GetComponent<Animator>().SetBool("isHuman", isHuman);
            GetComponent<Animator>().SetBool("isChange", isChange);
           
        }
    }
   
 

    protected override void AttemptMove(float xDir, float yDir){ 
        base.AttemptMove(xDir, yDir);
    }
}
