using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingUnit {
    public float restartLevelDelay = 1f;


    private Animator animator;

	// Use this for initialization
	protected override  void Start () {
        //animator = GetComponent<Animator>;
        base.Start();
		
	}
	
	// Update is called once per frame
	void Update () {
        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if(horizontal != 0){
            vertical = 0;
        }
        if(horizontal!=0 || vertical!=0){
      
            AttempMove(horizontal, vertical);
        }


	}


     protected override void AttempMove(int xDir, int yDir)
    {
       base.AttempMove(xDir, yDir);
    }
}
