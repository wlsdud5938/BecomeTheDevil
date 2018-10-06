using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingUnit {
  

    public float movingSpeed = 0.01f;
    public GameObject bullet;


    private int dir; // 0: front, 1: right, 2: back, 3: left
    private bool isChop;
    private enum Direction {FRONT,RIGHT,BACK,LEFT,FRONTLEFT,FRONTRIGHT,BACKRIGHT,BACKLEFT};
	// Use this for initialization





	protected override  void Start () {
        //animator = GetComponent<Animator>;
        dir = 0;
        GetComponent<Animator>().SetInteger("dir", dir);
        GetComponent<Animator>().SetBool("isIdle", true);
        base.Start();

		
	}

    private int CalcDirection(int horizontal, int vertical){
        if (vertical > 0 && horizontal > 0) return (int)Direction.FRONTRIGHT;
        if (vertical > 0 && horizontal < 0) return (int)Direction.FRONTLEFT;
        if (vertical < 0 && horizontal > 0) return (int)Direction.BACKRIGHT;
        if (vertical < 0 && horizontal < 0) return (int)Direction.BACKLEFT;
        if (vertical > 0) return (int)Direction.FRONT;
        if (vertical < 0) return (int)Direction.BACK;
        if (horizontal > 0) return (int)Direction.RIGHT;
        if (horizontal < 0) return (int)Direction.LEFT;
        return -1; // 잘못된 경우 안됌
    }

	
	// Update is called once per frame
	void Update () {
        int horizontal = 0;
        int vertical = 0;
        isChop = false;
        GetComponent<Animator>().SetBool("isChop", isChop);
        GetComponent<Animator>().SetBool("isIdle", true);

        horizontal = (int)Input.GetAxisRaw("Horizontal"); 
        vertical = (int)Input.GetAxisRaw("Vertical");

    
        if (horizontal != 0 || vertical != 0)
        {

            dir = CalcDirection(horizontal, vertical);
            GetComponent<Animator>().SetInteger("dir", dir);
            GetComponent<Animator>().SetBool("isIdle", false);
            AttemptMove(horizontal * movingSpeed, vertical * movingSpeed);

        }
        if (Input.GetMouseButton(0))
        {
            isChop = true;
            GetComponent<Animator>().SetBool("isIdle", false);
            GetComponent<Animator>().SetBool("isChop", isChop);
            Fire();
        }
    }
   
    void Fire()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }


    protected override void AttemptMove(float xDir, float yDir){ 
        base.AttemptMove(xDir, yDir);
    }
}
