using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy2 : MonoBehaviour {
    [SerializeField]
    private string projectileType; // 발사체를 배틀매니져에서 고름.
    
    public float projectileSpeed = 10f;              // 발사체 속도.


    // 공격 스탯
    Statu stat;
    private bool canAttack = true; // 공격속도를 세팅하기 위해
    public float attackTimer = 0.1f;
    //public float attackCooldown = 1.0f;
    //public float attackSpeed = 3.0f;
    
    public Statu unitTarget;
    private Queue<Statu> unit = new Queue<Statu>();
    bool isSearch = false;      // 적이 타겟을 발견했을 때.
    private Animator myAnimator; // 적의 공격 애니메이션
    
    

    // Use this for initialization
    void Start () {
        myAnimator = transform.parent.GetComponent<Animator>();
        stat = transform.parent.GetComponent<Statu>();
	}
	
	// Update is called once per frame
	void Update () {
        Targetting();
	}
    private void Targetting()
    {
        if (!canAttack)     // 공격할 수 없다면
        {
            attackTimer += Time.deltaTime * stat.attackSpeed;  // attackTimer 증가.

            if (attackTimer >= stat.attackCoolTime)  // attackTimer가 쿨타임보다 커지면
            {
                canAttack = true;               // 공격 가능.
                attackTimer = 0;
            }
        }

        if (unit.Count > 0 && unitTarget == null)   // target == null 한 적이 원 밖으로 나갈 때.
        {
            unitTarget = unit.Dequeue();      // Queue에 있는 걸 빼면서 아직 원 안에 있는 적을 타겟으로 지정.
            //Debug.Log("타겟 지정");
            //Debug.Log(unitTarget);
        }

        if (unitTarget != null /*&& target.IsActive*/)
        {
            if (canAttack)
            {
                Attack();
                myAnimator.SetTrigger("EnemyChop");
                myAnimator.SetBool("EnemyIdle", false);
                myAnimator.SetBool("EnemyHit", false);
                myAnimator.SetBool("EnemyRun", true);
                canAttack = false;
                if(unitTarget.transform.position.x - transform.position.x > 0)
                {
                    myAnimator.SetFloat("Dir", 1f);
                }
                else
                    myAnimator.SetFloat("Dir", 0);
            }
        }
    }

    private void Attack()
    {
        // 발사체 생성
        EnemySword projectile = BattleManager.Instance.Pool.GetObject(projectileType).GetComponent<EnemySword>();
        // 발사체 위치를 타워의 위치로.
        projectile.transform.position = transform.parent.GetComponent<Statu>().middlePoint.position;
        // 발사체 초기화
        projectile.Initialize(this);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Unit" || other.tag == "Player" || other.tag == "Boss")
        {
            unit.Enqueue(other.GetComponent<Statu>());
            //Debug.Log("유닛 들어옴");
            //Debug.Log(unit.Count);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Unit" || other.tag == "Player" || other.tag == "Boss")
        {
            unitTarget = null;
            //Debug.Log("유닛 나감");
            //Debug.Log(unit.Count);
        }
    }
}
