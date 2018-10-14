using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    public float moveSpeed = 1.0f; // 이동속도
    public float attackRange = 1.0f; // 무기의 Range, 작을 수록 가까이 달라붙습니다.
    public float distanceOfTile = 1.0f; // 애니메이터를 좌,우 -> 상 하로 전환시킬 최소한의 거리입니다. 
    public float hp = 100f;
    private Transform target; // 쫓아갈 대상
    private Animator animation; // Animator 선언

    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidbodys; // test

    private float adv; //enemy와 player사이의 distance입니다.

    private int dir; // animator의 방향인식 int값입니다. 아래에 상술.
    //Animator에서, Int0 = Front / 1 = Back / 2 = Left / 3 = Right / 4 = None
    private string animators; // animator의 motion 값입니다. 아래에 상술
    //animators = {EnemyIdle, EnemyRun, EnemyChop, EnemyHit(미구현), EnemyDie(미구현)}

    // 김윤성이 추가했어요.
    [SerializeField]
    private float monsterHealth = 10f;
    [SerializeField]
    private Stat health;
    private bool isActive = true;
    public bool IsActive { get; set; }
    // 여기까지


    // Use this for initialization
    // 18.10.14 Start() -> Awake(), 김윤성 Awake로 바꾸니까 에러뜨네요. 다시 Start로 수정했습니다.
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //boxCollider.GetComponent<BoxCollider2D>();
        animation = GetComponent<Animator>();

        // 김윤성이 추가했습니다.
        IsActive = true;
        this.health.MaxVal = 10;
        this.health.CurrentValue = this.health.MaxVal;
        health.Initialize();
        // 여기까지.
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 targetPos = target.position;
        Vector2 myPos = transform.position;
        adv = Vector2.Distance(transform.position, target.position); // adv에 값을 넣어줍니다.
        
        if (adv >= attackRange) // Range보다 멀리 있으므로 Run해야합니다.
        {
            if (targetPos.x <= myPos.x) //플레이어가 Enemy(자신)보다 좌측이면,
            {
                if (myPos.y >= (targetPos.y - distanceOfTile) && myPos.y <= (targetPos.y + distanceOfTile)) //DOT를 기점으로 일정 구간내에서는 좌측을 보도록 고정한다.
                {
                    AnimatorOfEnemy(2,"EnemyRun");
                    AnimatorOfEnemy(4, "EnemyChop");
                    AnimatorOfEnemy(4, "EnemyIdle");
                    moveEnemy(targetPos, myPos, moveSpeed, adv);
                }
                else if(myPos.y>targetPos.y) // DOT 구간 내에 없으면서 Player가 나보다 아래에 있으면, 아래를 보도록 고정한다.
                {
                    AnimatorOfEnemy(1, "EnemyRun");
                    AnimatorOfEnemy(4, "EnemyChop");
                    AnimatorOfEnemy(4, "EnemyIdle");
                    moveEnemy(targetPos, myPos, moveSpeed, adv);
                }
                else if (myPos.y <= targetPos.y) // DOT 구간 내에 없으면서 Player가 나보다 위에 있으면 위를 보도록 고정한다.
                {
                    AnimatorOfEnemy(0, "EnemyRun");
                    AnimatorOfEnemy(4, "EnemyChop");
                    AnimatorOfEnemy(4, "EnemyIdle");
                    moveEnemy(targetPos, myPos, moveSpeed, adv);
                }
            }
            if (targetPos.x > myPos.x) //플레이어가 Enemy(자신)보다 우측이면,
            {
                if (myPos.y >= (targetPos.y - distanceOfTile) && myPos.y <= (targetPos.y + distanceOfTile)) //DOT를 기점으로 일정 구간내에서는 우측을 보도록 고정한다.
                {
                    AnimatorOfEnemy(3, "EnemyRun");
                    AnimatorOfEnemy(4, "EnemyChop");
                    AnimatorOfEnemy(4, "EnemyIdle");
                    moveEnemy(targetPos, myPos, moveSpeed, adv);
                }
                else if (myPos.y > targetPos.y) // DOT 구간 내에 없으면서 Player가 나보다 아래에 있으면, 아래를 보도록 고정한다.
                {
                    AnimatorOfEnemy(1, "EnemyRun");
                    AnimatorOfEnemy(4, "EnemyChop");
                    AnimatorOfEnemy(4, "EnemyIdle");
                    moveEnemy(targetPos, myPos, moveSpeed, adv);
                }
                else if (myPos.y <= targetPos.y) // DOT 구간 내에 없으면서 Player가 나보다 위에 있으면 위를 보도록 고정한다.
                {
                    AnimatorOfEnemy(0, "EnemyRun");
                    AnimatorOfEnemy(4, "EnemyChop");
                    AnimatorOfEnemy(4, "EnemyIdle");
                    moveEnemy(targetPos, myPos, moveSpeed, adv);
                }
            }
        }
        else if (adv < attackRange) // Range 안의 적을 공격합니다.
        {
            if (targetPos.x <= myPos.x) //플레이어가 Enemy(자신)보다 좌측이면,
            {
                if (myPos.y >= (targetPos.y - distanceOfTile) && myPos.y <= (targetPos.y + distanceOfTile)) //DOT를 기점으로 일정 구간내에서는 좌측을 공격하도록 고정.
                {
                    AnimatorOfEnemy(2, "EnemyChop");
                    AnimatorOfEnemy(4, "EnemyRun");
                    AnimatorOfEnemy(4, "EnemyIdle");
                    //AttackEnemy(targetPos, myPos, adv);
                }
                else if (myPos.y > targetPos.y) // DOT 구간 내에 없으면서 Player가 나보다 아래에 있으면, 아래를 보며 공격.
                {
                    AnimatorOfEnemy(1, "EnemyChop");
                    AnimatorOfEnemy(4, "EnemyRun");
                    AnimatorOfEnemy(4, "EnemyIdle");
                    //moveEnemy(targetPos, myPos, adv);
                }
                else if (myPos.y <= targetPos.y) // DOT 구간 내에 없으면서 Player가 나보다 위에 있으면 위를 보며 공격.
                {
                    AnimatorOfEnemy(0, "EnemyChop");
                    AnimatorOfEnemy(4, "EnemyRun");
                    AnimatorOfEnemy(4, "EnemyIdle");
                    //moveEnemy(targetPos, myPos, adv);
                }
            }
            if (targetPos.x > myPos.x) //플레이어가 Enemy(자신)보다 우측이면,
            {
                if (myPos.y >= (targetPos.y - distanceOfTile) && myPos.y <= (targetPos.y + distanceOfTile)) //DOT를 기점으로 일정 구간내에서는 우측을 보도록 고정한다.
                {
                    AnimatorOfEnemy(3, "EnemyChop");
                    AnimatorOfEnemy(4, "EnemyRun");
                    AnimatorOfEnemy(4, "EnemyIdle");
                    //moveEnemy(targetPos, myPos, adv);
                }
                else if (myPos.y > targetPos.y) // DOT 구간 내에 없으면서 Player가 나보다 아래에 있으면, 아래를 보도록 고정한다.
                {
                    AnimatorOfEnemy(1, "EnemyChop");
                    AnimatorOfEnemy(4, "EnemyRun");
                    AnimatorOfEnemy(4, "EnemyIdle");
                    //moveEnemy(targetPos, myPos, adv);
                }
                else if (myPos.y <= targetPos.y) // DOT 구간 내에 없으면서 Player가 나보다 위에 있으면 위를 보도록 고정한다.
                {
                    AnimatorOfEnemy(0, "EnemyChop");
                    AnimatorOfEnemy(4, "EnemyRun");
                    AnimatorOfEnemy(4, "EnemyIdle");
                    //moveEnemy(targetPos, myPos, adv);
                }
            }
        }

    }

    public void TakeDamage(float Damage)
    {
        hp -= Damage;
    }


    protected virtual void AnimatorOfEnemy(int dir, string animators)
    {
        animation = GetComponent<Animator>();
        animation.SetInteger(animators, dir);
        //dir => Int // 0 = Front / 1 = Back / 2 = Left / 3 = Right 
        //animators = {EnemyIdle, EnemyRun, EnemyChop, EnemyHit(미구현), EnemyDie(미구현)}

    }

    protected virtual void moveEnemy(Vector2 xpos, Vector2 ypos, float speed, float adv)
    {
        rigidbodys = GetComponent<Rigidbody2D>();
        //transform.position = Vector2.MoveTowards(ypos, xpos, speed * Time.deltaTime); // 이대로만하면 컴퓨터 성능마다 이동속도가 차이가납니다.
        rigidbodys.position = Vector2.MoveTowards(ypos, xpos, speed * Time.deltaTime);
    }


    // 김윤성이 추가
    public void TakeDamage(int damage)
    {
        if (IsActive)
        {
            health.CurrentValue -= damage;
        }
    }
}
