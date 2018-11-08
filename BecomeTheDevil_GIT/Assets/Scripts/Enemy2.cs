using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour {
    [SerializeField]
    private string projectileType; // 발사체를 배틀매니져에서 고름.

    [SerializeField]
    private float projectileSpeed;              // 발사체 속도.

    [SerializeField]
    private int damage;                         // 데미지

    // 공격 속도
    private bool canAttack = true; // 공격속도를 세팅하기 위해
    public float attackTimer = 0.1f;
    public float attackCooldown = 5.0f;

    public GameObject target;   // 타겟. 유니티에서 설정.
    bool isSearch = false;      // 적이 타겟을 발견했을 때.

    public GameObject Target
    {
        get
        {
            return target;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Search();       // 타겟을 찾음.
        if (isSearch)   // 타겟이 일정거리에 들어오면
        {
            Targetting();   // 타게팅 후 공격.
        }
	}

    void Search()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        //거리가 가까워지면 탐색에 걸림
        if (distance <= 10)
            isSearch = true;
        else if (distance > 10)
            isSearch = false;
    }
    
    private void Targetting()
    {
        if (!canAttack)     // 공격할 수 없다면
        {
            attackTimer += Time.deltaTime;  // attackTimer 증가.

            if (attackTimer >= attackCooldown)  // attackTimer가 쿨타임보다 커지면
            {
                canAttack = true;               // 공격 가능.
                attackTimer = 0;
            }
        }

        if (target != null /*&& target.IsActive*/)
        {
            if (canAttack)
            {
                Attack();
                canAttack = false;
            }
        }
    }

    private void Attack()
    {
        // 발사체 생성
        Projectile projectile = BattleManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();
        // 발사체 위치를 타워의 위치로.
        projectile.transform.position = transform.position;
        // 발사체 초기화
        projectile.Initialize(this);
    }
}
