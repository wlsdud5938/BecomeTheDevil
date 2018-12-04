using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // 타워 관련.
    private Enemy target;
    private Tower parent;   // 발사체를 가지고 있는 타워.

    // 사거리 관련.
    private Vector3 unitPosition; // 총알이 발사될 때의 유닛 위치.
    public float distance;  // 총알과 유닛 사이의 거리, 사거리에 사용.
    public float attackRange;

    public Statu statu;

    // Use this for initialization
    void Start()
    {
        statu = parent.transform.parent.GetComponent<Statu>();
        unitPosition = parent.transform.position;
        attackRange = statu.attackRange;
    }
    


    // Update is called once per frame
    void Update()
    {
        MoveToTarget();     // 적을 쫓아가는 함수.

        distance = Vector3.Distance(unitPosition, transform.position);
        if (attackRange < distance)    // 사거리보다 길어지면.
        {
            //transform.localScale = new Vector3(1f, 1f, 1f); //크기 초기화.
            BattleManager.Instance.Pool.ReleaseObject(gameObject);
        }
    }

    public void Initialize(Tower parent)
    {
        // 타워에서 정해놓은 타겟으로 초기화.
        this.target = parent.Target;
        this.parent = parent;
        
    }

    private void MoveToTarget()
    {
        if (target != null /*&& target.IsActive*/)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.GetComponent<Statu>().middlePoint.position, Time.deltaTime * parent.ProjectileSpeed);
            // 방향이 있는 발사체일 경우 적 방향으로 회전시킴. 
            Vector2 dir = target.GetComponent<Statu>().middlePoint.position - transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            // Projectile 이 오른쪽으로 향해 있어야 앞이 적쪽으로 향함.
        }
        
        else if (/*!target.IsActive ||*/ target == null)  // 타겟이 죽으면 발사체 없앰.
        {
            BattleManager.Instance.Pool.ReleaseObject(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 적과 충돌하면
        if (other.tag == "EnemyHitCollider")
        {
            //Debug.Log("tag == Enemy 확인");
            if (/*target.gameObject == other.transform.parent.gameObject &&*/ target != null)
            {
                //Debug.Log("오예");
                // 타겟에게 데미지를 주고 발사체를 없앰.
                Statu target = other.transform.parent.GetComponent<Statu>();
                target.TakeDamage(parent.transform.parent.GetComponent<Statu>().attackDamage);
                BattleManager.Instance.Pool.ReleaseObject(gameObject);

                if (parent.projectileType == "UnitIceBullet")
                {
                    //Debug.Log("아이스 공격!");
                    other.transform.parent.GetComponent<Statu>().IceDamage(3f);
                }
            }
        }
    }




}
