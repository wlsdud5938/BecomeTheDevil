﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 1.0f; // 이동속도
    public float attSpeed = 0.3f;
    public float attackRange = 1.0f; // 무기의 Range, 작을 수록 가까이 달라붙습니다.
    public float distanceOfTile = 1.0f; // 애니메이터를 좌,우 -> 상 하로 전환시킬 최소한의 거리입니다. 
    public float damage = 5f;
    public float attTimer = 0.0f;
    private bool isAtt = false;
    public List<GameObject> pathList;


    public float hp = 100f;
    public float currentHp;
    private Transform target; // 쫓아갈 대상
    private Animator animation; // Animator 선언
    private Vector3 testTarget; // 2d baked target test
    public float dist; // AI관련 거리변수
    public float AiRange = 1.0f; // AI의 Player, Tower를 찾는 거리입니다. 이 안에선 Player와 Tower를 우선 공격합니다. 그 외에선 core를 찾습니다.
    public float AiTime = 1.0f; // Ai가 반복적으로 path를 찾는 시간입니다.
    public bool DebugButton = true; // debug 테스트를 위한 버튼입니다.

    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidbodys; // test
    private float adv; //enemy와 player사이의 distance입니다.

    private int dir; // animator의 방향인식 int값입니다. 아래에 상술.
    //Animator에서, Int0 = Front / 1 = Back / 2 = Left / 3 = Right / 4 = None
    private string animators; // animator의 motion 값입니다. 아래에 상술
                              //animators = {EnemyIdle, EnemyRun, EnemyChop, EnemyHit(미구현), EnemyDie(미구현)}

    /* // 김윤성이 추가했어요.
     [SerializeField]
     private float monsterHealth = 10f;
     [SerializeField]
     private Stat health;
     // 여기까지*/

    private bool isActive = true;
    public bool IsActive { get; set; }

    public Image healthBarFilled;

    RoomTemplates templates;
    float distanceLength;
// Use this for initialization
void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        if (templates.rooms[templates.rooms.Count - 1].gameObject.GetComponent<AddRoom>().path == 1)
        {
            for(int i = 0; i<templates.bList.Count;i++)
                pathList.Add(templates.bList[i]);
        }
        else if (templates.rooms[templates.rooms.Count - 1].gameObject.GetComponent<AddRoom>().path == 2)
        {
            for (int i = 0; i < templates.tList.Count; i++)
                pathList.Add(templates.tList[i]);
        }
        else if (templates.rooms[templates.rooms.Count - 1].gameObject.GetComponent<AddRoom>().path == 3)
        {
            for (int i = 0; i < templates.lList.Count; i++)
                pathList.Add(templates.lList[i]);
        }
        else if (templates.rooms[templates.rooms.Count - 1].gameObject.GetComponent<AddRoom>().path == 4)
        {
            for (int i = 0; i < templates.rList.Count; i++)
                pathList.Add(templates.rList[i]);
        }
        if (GameObject.FindGameObjectWithTag("Player"))
            {
            rigidbodys = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
            InvokeRepeating("getClosestEnemy", 0, AiTime);
            InvokeRepeating("GotoPlayer", 0, AiTime);

            
            IsActive = true;
            /*// 김윤성이 추가했습니다.
            this.health.MaxVal = 10;
            this.health.CurrentValue = this.health.MaxVal;
            health.Initialize();*/
            // 여기까지.
            currentHp = hp;
            healthBarFilled.fillAmount = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAtt && attTimer >= attSpeed)
            isAtt = false;
        else if (isAtt)
            attTimer += 0.1f * Time.deltaTime;

        if (currentHp <= 0)
            Destroy(gameObject);
        if (target != null)//debug용
        {
            /*Debug.DrawLine(transform.position, target.position,
                           Color.green); // debug용.

           /* if (Input.GetKeyDown(KeyCode.R)) // R을 누르면 현재 마우스 위치로 이동합니다. (카메라에 잡힐태니...)
            {
                Vector3 w = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GetComponent<NavMeshAgent2D>().destination = w;
                DebugButton = !DebugButton;
            }*/

            adv = Vector2.Distance(transform.position, target.position); // adv에 값을 넣어줍니다.
            Vector2 myPos = transform.position;
            Vector2 targetPos = target.position;
            if ((attackRange <= adv) && (target != null))
            {
                if (targetPos.x <= myPos.x) //플레이어가 Enemy(자신)보다 좌측이면,
                {
                    if (myPos.y >= (targetPos.y - distanceOfTile) && myPos.y <= (targetPos.y + distanceOfTile)) //DOT를 기점으로 일정 구간내에서는 좌측을 보도록 고정한다.
                    {
                        AnimatorOfEnemy(2, "EnemyRun");
                        AnimatorOfEnemy(4, "EnemyChop");
                        AnimatorOfEnemy(4, "EnemyIdle");

                    }
                    else if (myPos.y > targetPos.y) // DOT 구간 내에 없으면서 Player가 나보다 아래에 있으면, 아래를 보도록 고정한다.
                    {
                        AnimatorOfEnemy(1, "EnemyRun");
                        AnimatorOfEnemy(4, "EnemyChop");
                        AnimatorOfEnemy(4, "EnemyIdle");

                    }
                    else if (myPos.y <= targetPos.y) // DOT 구간 내에 없으면서 Player가 나보다 위에 있으면 위를 보도록 고정한다.
                    {
                        AnimatorOfEnemy(0, "EnemyRun");
                        AnimatorOfEnemy(4, "EnemyChop");
                        AnimatorOfEnemy(4, "EnemyIdle");

                    }
                }
                if (targetPos.x > myPos.x) //플레이어가 Enemy(자신)보다 우측이면,
                {
                    if (myPos.y >= (targetPos.y - distanceOfTile) && myPos.y <= (targetPos.y + distanceOfTile)) //DOT를 기점으로 일정 구간내에서는 우측을 보도록 고정한다.
                    {
                        AnimatorOfEnemy(3, "EnemyRun");
                        AnimatorOfEnemy(4, "EnemyChop");
                        AnimatorOfEnemy(4, "EnemyIdle");

                    }
                    else if (myPos.y > targetPos.y) // DOT 구간 내에 없으면서 Player가 나보다 아래에 있으면, 아래를 보도록 고정한다.
                    {
                        AnimatorOfEnemy(1, "EnemyRun");
                        AnimatorOfEnemy(4, "EnemyChop");
                        AnimatorOfEnemy(4, "EnemyIdle");

                    }
                    else if (myPos.y <= targetPos.y) // DOT 구간 내에 없으면서 Player가 나보다 위에 있으면 위를 보도록 고정한다.
                    {
                        AnimatorOfEnemy(0, "EnemyRun");
                        AnimatorOfEnemy(4, "EnemyChop");
                        AnimatorOfEnemy(4, "EnemyIdle");

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
        GotoCore();
    }


    void GotoCore()
    {
        if (pathList.Count != 0)
        {
            distanceLength = Vector2.Distance(gameObject.transform.position, pathList[0].gameObject.transform.position);
            gameObject.transform.position = Vector2.MoveTowards(transform.position, pathList[0].gameObject.transform.position, moveSpeed * Time.deltaTime);
        }
    }



    void GotoPlayer()
    {
        /*if (DebugButton == true) //test용입니다. 두번 누르면 안되요!
        {
            Vector3 w = target.position;
            GetComponent<NavMeshAgent2D>().destination = w;
        }*/
    }

    void getClosestEnemy()
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
    protected virtual void AnimatorOfEnemy(int dir, string animators)
    {
        animation = GetComponent<Animator>();
        animation.SetInteger(animators, dir);
        //dir => Int // 0 = Front / 1 = Back / 2 = Left / 3 = Right 
        //animators = {EnemyIdle, EnemyRun, EnemyChop, EnemyHit(미구현), EnemyDie(미구현)}

    }
    public void TakeDamage(float damage)
    {

        currentHp -= damage;

        healthBarFilled.fillAmount = (float)currentHp / hp;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals("CenterPoint"))
            pathList.Remove(pathList[0]);
        if(other.tag.Equals("Boss"))
            SceneManager.LoadScene("Defeat");

    }
}