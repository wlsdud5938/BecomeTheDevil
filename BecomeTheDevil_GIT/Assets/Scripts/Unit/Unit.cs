using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitState
{
    none,
    //move,
    attack,
    damaged,
    dead
}

public class Unit : Singleton<Unit> {

    public string unitName;
    public LayerMask enemyLayerMask;        // 타워가 적을 찾기 위한 mask
    [HideInInspector]
    public float speed;
    // LineCast에 사용될 위치.
    [HideInInspector]
    public Transform frontPosition;
    protected RaycastHit2D isRightUpObstacle;
    protected RaycastHit2D isRightDownObstacle;
    protected RaycastHit2D isLeftObstacle;
    protected RaycastHit2D isUpObstacle;
    protected RaycastHit2D isDownObstacle;
    // 유닛 상태.
    public UnitState currentState = UnitState.none;
    // 공격 가능여부 저장.
    protected bool enableAttack = true;

    private Vector3 vector;
    public Animator animator;
    private Transform target;   // Enemy의 위치를 저장 유닛이 어느 방향으로 공격할지 알려줌.


    // Use this for initialization
    void Awake () {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Enemy").transform;      // Enemy 태그의 위치 값 가져옴.
    }

    public void GoAttack()
    {
        animator.SetBool("Attack", true);
    }

    public void StopAttack()
    {
        animator.SetBool("Attack", false);
    }

    void FixedUpdate()
    {

    }

    public void AttackDir()
    {
        switch (currentState)
        {
            case UnitState.none:
                // 이동 중지.
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
            /*case UnitState.move:
                // 장애물이 있는지 Linecast로 검출.
                isObstacle = Physics2D.Linecast(
                    transform.position, frontPosition.position,
                    1 << LayerMask.NameToLayer("Obstacle"));

                if (isObstacle)
                {
                    // 장애물을 만나면 공격 애니메이션으로 전환.
                    if (enableAttack)
                    {
                        currentState = UnitState.attack;
                        // Animator에 등록한 attack Trigger를 작동.
                        animator.SetTrigger("attack");
                    }
                }

                else
                {
                    // 장애물이 없다면 이동.
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed,
                    GetComponent<Rigidbody2D>().velocity.y);
                }
                break;*/
            case UnitState.attack:
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                // 장애물이 있는지 Linecast로 검출.
                isRightUpObstacle = Physics2D.Linecast(
                    transform.position, frontPosition.position,
                    1 << LayerMask.NameToLayer("Enemy"));

                isRightDownObstacle = Physics2D.Linecast(
                    transform.position, frontPosition.position,
                    1 << LayerMask.NameToLayer("Enemy"));

                if (isRightUpObstacle || isRightDownObstacle)
                {
                    // 장애물을 만나면 공격 애니메이션으로 전환.
                    if (enableAttack)
                    {
                        currentState = UnitState.attack;
                        // Animator에 등록한 attack Trigger를 작동.
                        animator.SetFloat("AttackX", 1.0f);
                        animator.SetFloat("AttackY", 0.0f);
                    }
                }
                break;
            case UnitState.damaged:
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
            case UnitState.dead:
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
        }
    }
    
}
