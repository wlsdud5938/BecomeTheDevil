using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour
{
    [SerializeField]        // 유니티 에디터에서 보임.
    private string projectileType;     // 유닛에 따라 발사체를 다르게.

    /*[SerializeField]
    private Projectile projectilePrefab;           // 발사체 프리팹
    */
    [SerializeField]
    private float projectileSpeed;              // 발사체 속도.

    [SerializeField]
    private int damage;                         // 데미지

    //private SpriteRenderer mySpriteRenderer;        // 사정거리를 보여줌.

    private Enemy target;
    private Queue<Enemy> enemy = new Queue<Enemy>();    // Enemy 들을 저장해놓는 Queue
    private bool canAttack = true;                      // 공격 쿨타임 때 사용할 bool 변수
    [SerializeField]
    private float attackTimer = 0.1f;
    [SerializeField]
    private float attackCooldown = 0.5f;    // 공격 쿨타임
    private Animator myAnimator;

    public Enemy Target
    {
        get
        {
            return target;
        }
    }

    public int Damage
    {
        get
        {
            return damage;
        }
    }


    public float ProjectileSpeed
    {
        get
        {
            return projectileSpeed;
        }
    }

    // Use this for initialization
    void Awake()
    {     // 이거 Start로 하니까 안때림 무엇???
        myAnimator = transform.parent.GetComponent<Animator>();

        //mySpriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Targetting();

        //Debug.Log(target);
    }

    public void Select()
    {

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

        if (enemy.Count > 0 && target == null)   // target == null 한 적이 원 밖으로 나갈 때.
        {
            target = enemy.Dequeue();      // enemy Queue에 있는 걸 빼면서 아직 원 안에 있는 적을 타겟으로 지정.
            //Debug.Log(target);
        }
        //Debug.Log(target.IsActive);
        if (target != null && target.IsActive)     // 타겟이 원 안에 살아 있다면
        {
            if (canAttack)
            {
                Debug.Log("공격");
                Attack();
                myAnimator.SetTrigger("Attack");
                canAttack = false;
            }
        }
    }

    private void Attack()
    {
        // 발사체 생성
        Projectile projectile = BattleManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();
        // 발사체 위치를 타워의 위치로.
        projectile.transform.position = transform.parent.GetComponent<Statu>().middlePoint.position;
        // 발사체 초기화
        projectile.Initialize(this);
        //Debug.Log("공격!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemy.Enqueue(other.transform.GetChild(0).GetComponent<Enemy>());
            Debug.Log("적 들어옴");
            Debug.Log(enemy.Count);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("적 나감");
            Debug.Log(enemy.Count);
            target = null;      // range 밖으로 나가면 target 해제
        }
    }
}