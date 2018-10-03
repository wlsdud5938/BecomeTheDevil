using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingUnit {
  
    public int dir; // 0: front, 1: right, 2: back, 3: left
    public float  movingSpeed = 0.01f;
    private Animator animator;
   
    private enum Direction {FRONT,RIGHT,BACK,LEFT};
	// Use this for initialization
	protected override  void Start () {
        //animator = GetComponent<Animator>;
        dir = -1;
        GetComponent<Animator>().SetInteger("dir", dir);
        base.Start();

		
	}


    private int CalcDirection(int horizontal, int vertical){
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
        dir = -1;
        GetComponent<Animator>().SetInteger("dir", dir);

        horizontal = (int)Input.GetAxisRaw("Horizontal"); 
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0){
            vertical = 0;
        }
 
        if(horizontal!=0 || vertical!=0){
            dir = CalcDirection(horizontal, vertical);
            GetComponent<Animator>().SetInteger("dir", dir);
            AttemptMove(horizontal*movingSpeed, vertical*movingSpeed);
        
        }



    }


    protected override void AttemptMove(float xDir, float yDir){ 
        base.AttemptMove(xDir, yDir);
    }
}
