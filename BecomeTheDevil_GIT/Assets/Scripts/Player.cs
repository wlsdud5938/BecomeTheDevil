using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string projectileType;   // 오브젝트 풀에서 불러올 총알 이름.
    public string fakeBulletName;   // 오브젝트 풀에서 불러올 가짜 총알 이름.
    public GameObject bulletPrefab; //발사할 총알
    public GameObject fakeBulletPrefab; // 가짜 총알

    public int countItem = 0;
    public bool haveKey = false;

    int horizontal = 0;
    int vertical = 0;


    bool isHuman=true;
    Statu statu; // 스탯 
    int cur_hor=0, cur_ver=0; // 총쏠 방향 결정하기 위함
    enum Direction {FRONT,RIGHT,BACK,LEFT,FRONTLEFT,FRONTRIGHT,BACKRIGHT,BACKLEFT};
    float attTimer; //어택 타이머
    Animator animator;


    protected void Start () {
        statu = GetComponent<Statu>();
        animator = GetComponent<Animator>();
        animator.SetFloat("DirX", cur_hor);
        animator.SetFloat("DirY", cur_ver);
        animator.SetBool("isHuman", isHuman);

        attTimer += 0.0f;
    }

	
	// Update is called once per frame
	void Update () {
        attTimer += Time.deltaTime;
        animator.SetBool("isIdle", true);
        animator.SetBool("isMoving", false);


        horizontal = (int)Input.GetAxisRaw("Horizontal"); 
        vertical = (int)Input.GetAxisRaw("Vertical");


        /*
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
            

        }*/
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

        }

        Move();
    }

    void Move()
    {
        transform.Translate(Vector3.right * statu.movoingSpeed * Time.smoothDeltaTime * horizontal, 0);
        transform.Translate(Vector3.up * statu.movoingSpeed * Time.smoothDeltaTime * vertical, 0);

    }


}
