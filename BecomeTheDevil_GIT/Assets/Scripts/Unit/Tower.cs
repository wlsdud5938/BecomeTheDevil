using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Tower : MonoBehaviour {
    [SerializeField]        // 유니티 에디터에서 보임.
    private string projectileType;     // 유닛에 따라 공격을 다르게.

    [SerializeField]
    private ProjectTile projectilePrefab;           // 발사체 프리팹

    [SerializeField]
    private float projectileSpeed;              // 발사체 속도.

    [SerializeField]
    private int damage;                         // 데미지

    private SpriteRenderer mySpriteRenderer;        // 사정거리를 보여줌.

    private Enemy target;
    private Queue<Enemy> enemy = new Queue<Enemy>();
    private bool canAttack = true;
    [SerializeField]
    private float attackTimer = 0.1f;
    [SerializeField]
    private float attackCooldown = 0.5f;
    private Animator myAnimator;

    public Element ElementType { get; protected set; }

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
    void Awake () {
        myAnimator = transform.parent.GetComponent<Animator>();

        mySpriteRenderer = transform.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Targetting();
        Debug.Log(target);
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

        if (enemy.Count > 0 && target == null)   // target == null 한 Saitama가 원 밖으로 나갈 때.
        {
            target = enemy.Dequeue();      // enemy Queue에 있는 걸 빼면서 아직 원 안에 있는 Saitama를 타겟으로 지정.
        }

        if (target != null)     // 타겟이 원 안에 살아 있다면
        {
            if (canAttack)
            {
                //Attack();
                myAnimator.SetTrigger("Attack");
                canAttack = false;
            }
        }
    }
    
    private void Attack()
    {
        /*
        //Creates the projectile
        ProjectTile projectile = GameManager.Instance.Pool.GetObject(projectilePrefab.name).GetComponent<ProjectTile>();

        //Sets the projectiles position
        projectile.transform.position = transform.position;

        //Initializes the projectile
        projectile.Initialize(this);        // projectile에 target 을 알려줌.
        */
        ProjectTile projetile = GameManager.Instance.Pool.GetObject(projectileType).GetComponent<ProjectTile>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            enemy.Enqueue(other.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            target = null;      // range 밖으로 나가면 target 해제
        }
        //Unit.Instance.StopAttack();
        //Unit.Instance.animator.SetBool("Attack", false);
    }


}