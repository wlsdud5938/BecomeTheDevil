using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MovingUnit {
  

    public float movingSpeed = 0.01f;
    public float chopSpeed = 1f;    //플레이어 공격 속도
    public float hp = 100f;
    public int countItem = 0;
    public bool haveKey = false;
    
    private bool isHuman=true;
    private int cur_hor=0, cur_ver=1; // 총쏠 방향 결정하기 위함
    private enum Direction {FRONT,RIGHT,BACK,LEFT,FRONTLEFT,FRONTRIGHT,BACKRIGHT,BACKLEFT};
    private float timer;
    private Animator animator;
    private float currentHp;
    //public Image healthBarFilled;
    // Use this for initialization





    protected override  void Start () {
        animator = GetComponent<Animator>();
        animator.SetFloat("DirX", cur_hor);
        animator.SetFloat("DirY", cur_ver);
        animator.SetBool("isHuman", isHuman);

        base.Start();
        timer += 0.0f;
        currentHp = hp;
        //healthBarFilled.fillAmount = 1.0f;
    }

	
	// Update is called once per frame
	void Update () {
        int horizontal = 0;
        int vertical = 0;
        timer += Time.deltaTime;
        animator.SetBool("isIdle", true);
        animator.SetBool("isMoving", false);


        horizontal = (int)Input.GetAxisRaw("Horizontal"); 
        vertical = (int)Input.GetAxisRaw("Vertical");



        if (Input.GetKeyDown(KeyCode.F)&&timer>chopSpeed)
        {
            timer = 0;
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
            GetComponent<Animator>().SetBool("isHuman", isHuman);


        }
        if (horizontal != 0 || vertical != 0)
        {
            cur_hor = horizontal;
            cur_ver = vertical;

            GetComponent<Animator>().SetFloat("DirX", cur_hor);
            GetComponent<Animator>().SetFloat("DirY", cur_ver);
            animator.SetBool("isMoving", true);
            animator.SetBool("isIdle", false);

            AttemptMove(horizontal * movingSpeed*Time.deltaTime, vertical * movingSpeed * Time.deltaTime);

        }

       
    }
   
    public void TakeDamage(float Damage){
        hp -= Damage;
    }

    protected override void AttemptMove(float xDir, float yDir){ 
        base.AttemptMove(xDir, yDir);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag.Equals("Enemy"))
        {
            TakeDamage(other.gameObject.GetComponent<Enemy>().damage);
        }

    }

}
