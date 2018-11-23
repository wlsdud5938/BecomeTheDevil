using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    Statu statu; // 스탯 
    public float attTimer = 0.0f; //attTimer 0으로 초기화해줘야
    private bool isAtt = false;  //isAtt 공격할 수 있냐 true로 만드는 코드 없음. 왜 필요한거죠
    
    //Ai 관련 선언//
    private float adv; //Ai 관련 enemy와 player사이의 distance입니다.
    private float dist; // AI관련 거리변수
    public float AiRange = 1.0f; // AI의 Player, Tower를 찾는 거리입니다. 이 안에선 Player와 Tower를 우선 공격합니다. 그 외에선 core를 찾습니다.
    public float AiTime = 1.0f; // Ai가 반복적으로 path를 찾는 시간입니다.
    public float distanceOfTile = 1.0f; // DOT 애니메이터를 좌,우 -> 상 하로 전환시킬 최소한의 거리입니다. 

    //Start에 선언할 컴포넌트 관련 선언//
    private Transform target; // 쫓아갈 대상
    private Animator animation; // Animator 선언
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidbodys; // test
    private SpriteRenderer spriteRenderer;

    //Animator 관련 선언 //
    private float dir; //Animator에서, Int0 = Front / 0.3 = Back / 0.6 = Left / 1 = Right 
    private string animators; //animators = {EnemyIdle, EnemyRun, EnemyChop, EnemyHit(미구현), EnemyDie(미구현)}   

    //HP관련 선언 // 참조하는 것들 때문에 일시적으로 살려놨습니다.

    private bool isActive = true;  // 적이 살아있을 땐 true, 죽으면 false값을 가지는 변수입니다.

    public bool IsActive
    {
        get
        {
            return isActive;
        }
    }
    //public Image healthBarFilled;






    RoomTemplates templates;
    float distanceLength;
    // Use this for initialization
    void Start()
    {
        statu = GetComponent<Statu>();
        rigidbodys = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animation = GetComponent<Animator>();
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        InvokeRepeating("getClosestEnemy", 0, AiTime); //Player를 주기적으로 찾습니다. 임시 AI입니다.
        //스프라이트 버전에 맞게 렌더링
    }
    // Update is called once per frame
    void Update()
    {
        attTimer += Time.deltaTime;
        if (attTimer >= statu.attackSpeed)
            isAtt = true; 
        //공격하고 난 다음 attTimer 0으로 isAtt false로 만들어주세요

        if (target != null)
        {
            adv = Vector2.Distance(transform.position, target.position); // 적과 나 사이의 거리값 설정
            Vector2 myPos = transform.position;
            Vector2 targetPos = target.position;
            if ((statu.attackRange <= adv) && (target != null))
            {
                if (targetPos.x <= myPos.x) //플레이어가 Enemy(자신)보다 좌측이면,
                {
                    if (myPos.y >= (targetPos.y - distanceOfTile) && myPos.y <= (targetPos.y + distanceOfTile)) //DOT를 기점으로 일정 구간내에서는 좌측을 보도록 고정한다.
                    {
                        AnimatorOfEnemy(0.6f, "EnemyRun");
                    }
                    else if (myPos.y > targetPos.y) // DOT 구간 내에 없으면서 Player가 나보다 아래에 있으면, 아래를 보도록 고정한다.
                    {
                        AnimatorOfEnemy(0.3f, "EnemyRun");
                    }
                    else if (myPos.y <= targetPos.y) // DOT 구간 내에 없으면서 Player가 나보다 위에 있으면 위를 보도록 고정한다.
                    {
                        AnimatorOfEnemy(0, "EnemyRun");
                    }
                }
                if (targetPos.x > myPos.x) //플레이어가 Enemy(자신)보다 우측이면,
                {
                    if (myPos.y >= (targetPos.y - distanceOfTile) && myPos.y <= (targetPos.y + distanceOfTile)) //DOT를 기점으로 일정 구간내에서는 우측을 보도록 고정한다.
                    {
                        AnimatorOfEnemy(1, "EnemyRun");
                    }
                    else if (myPos.y > targetPos.y) // DOT 구간 내에 없으면서 Player가 나보다 아래에 있으면, 아래를 보도록 고정한다.
                    {
                        AnimatorOfEnemy(0.3f, "EnemyRun");
                    }
                    else if (myPos.y <= targetPos.y) // DOT 구간 내에 없으면서 Player가 나보다 위에 있으면 위를 보도록 고정한다.
                    {
                        AnimatorOfEnemy(0, "EnemyRun");
                    }
                }
            }
            else if (statu.attackRange > adv) // Range 안의 적을 공격합니다.
            {
                if (targetPos.x <= myPos.x) //플레이어가 Enemy(자신)보다 좌측이면,
                {
                    if (myPos.y >= (targetPos.y - distanceOfTile) && myPos.y <= (targetPos.y + distanceOfTile)) //DOT를 기점으로 일정 구간내에서는 좌측을 공격하도록 고정.
                    {
                        AnimatorOfEnemy(0.6f, "EnemyChop");
                        //AttackEnemy(targetPos, myPos, adv);
                    }
                    else if (myPos.y > targetPos.y) // DOT 구간 내에 없으면서 Player가 나보다 아래에 있으면, 아래를 보며 공격.
                    {
                        AnimatorOfEnemy(0.3f, "EnemyChop");
                        //moveEnemy(targetPos, myPos, adv);
                    }
                    else if (myPos.y <= targetPos.y) // DOT 구간 내에 없으면서 Player가 나보다 위에 있으면 위를 보며 공격.
                    {
                        AnimatorOfEnemy(0, "EnemyChop");
                        //moveEnemy(targetPos, myPos, adv);
                    }
                }
                if (targetPos.x > myPos.x) //플레이어가 Enemy(자신)보다 우측이면,
                {
                    if (myPos.y >= (targetPos.y - distanceOfTile) && myPos.y <= (targetPos.y + distanceOfTile)) //DOT를 기점으로 일정 구간내에서는 우측을 보도록 고정한다.
                    {
                        AnimatorOfEnemy(1, "EnemyChop");
                        //moveEnemy(targetPos, myPos, adv);
                    }
                    else if (myPos.y > targetPos.y) // DOT 구간 내에 없으면서 Player가 나보다 아래에 있으면, 아래를 보도록 고정한다.
                    {
                        AnimatorOfEnemy(0.3f, "EnemyChop");
                        //moveEnemy(targetPos, myPos, adv);
                    }
                    else if (myPos.y <= targetPos.y) // DOT 구간 내에 없으면서 Player가 나보다 위에 있으면 위를 보도록 고정한다.
                    {
                        AnimatorOfEnemy(0, "EnemyChop");
                        //moveEnemy(targetPos, myPos, adv);
                    }
                }
            }
        }
    }

    void getClosestEnemy() //Enemy와 해당 태그(지금은 플레이어만) 사이의 거리를 target으로 반환해줍니다.
        //지금 이 함수는 플레이어와 자신의 거리를 인식해서 방향을 확인하는 용도로 쓰이고 있습니다.
    {
        //비용이 큼 - 특정 태그의 오브젝트를 모두 찾음
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        GameObject[] taggedEnemys = GameObject.FindGameObjectsWithTag("Player");
        float closestDistSqr = Mathf.Infinity;//infinity 실제값?
        Transform closest = null;
        foreach (GameObject taggedEnemy in taggedEnemys)
        {
            Vector3 objectPos = taggedEnemy.transform.position;
            dist = (objectPos - transform.position).sqrMagnitude;
            // 특정 거리 안으로 들어올때
            if (dist < AiRange + 50000)
            {
                // 그 거리가 제곱한 최단 거리보다 작으면
                if (dist < closestDistSqr)
                {
                    closestDistSqr = dist;
                    closest = taggedEnemy.transform;
                }
            }
        }
        target = closest;
    }

    public void AnimatorOfEnemy(float direction, string animators)
    {     
        if (animators == "EnemyRun")
        {
            animation.SetFloat("Dir", direction); // 방향을 설정
            animation.SetBool("EnemyRun", true); //Run 활성화
            animation.SetBool("EnemyIdle", false);
            animation.SetBool("EnemyChop", false);
            animation.SetBool("EnemyHit", false);
        }
        if (animators == "EnemyIdle")
        {
            animation.SetFloat("Dir", direction); // 방향을 설정
            animation.SetBool("EnemyRun", false); 
            animation.SetBool("EnemyIdle", true); // Idle 활성화
            animation.SetBool("EnemyChop", false);
            animation.SetBool("EnemyHit", false);
        }
        if (animators == "EnemyChop")
        {
            animation.SetFloat("Dir", direction); // 방향을 설정
            animation.SetBool("EnemyRun", false);
            animation.SetBool("EnemyIdle", false); 
            animation.SetBool("EnemyChop", true); // Chop 활성화
            animation.SetBool("EnemyHit", false);
        }
        if (animators == "EnemyHit")
        {
            animation.SetFloat("Dir", direction); // 방향을 설정
            animation.SetBool("EnemyRun", false);
            animation.SetBool("EnemyIdle", false);
            animation.SetBool("EnemyChop", false);
            animation.SetBool("Enemyhit", true); // Hit 활성화
        }
        //direction => float // 0 => front // 0.3 => back // 0.6 => left // 1 => Right
        //animators = {EnemyIdle, EnemyRun, EnemyChop, EnemyHit(미구현), EnemyDie(미구현)}
    }


 
}
