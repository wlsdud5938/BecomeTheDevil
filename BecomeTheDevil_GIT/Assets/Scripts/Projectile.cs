using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // 타워 관련.
    private Enemy target;
    private Tower parent;   // 발사체를 가지고 있는 타워.
    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();     // 적을 쫓아가는 함수.
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
            Debug.Log("tag == Enemy 확인");
            //if (target.gameObject == other.transform.parent.gameObject && target != null )
            //{
                Debug.Log("오예");
                // 타겟에게 데미지를 주고 발사체를 없앰.
                //other.transform.parent.GetComponent<Statu>().TakeDamage(parent.transform.parent.GetComponent<Statu>().attackDamage);
                //target.transform.parent.GetComponent<Statu>().TakeDamage(parent.Damage);
                //target.TakeDamage(parent.Damage);
                Statu target = other.transform.parent.GetComponent<Statu>();
                target.TakeDamage(parent.transform.parent.GetComponent<Statu>().attackDamage);
                BattleManager.Instance.Pool.ReleaseObject(gameObject);
            //}
            // Enemy hitInfo = other.GetComponent<Enemy>();
            //Debug.Log("Hit Enemy");
        }
    }




}
