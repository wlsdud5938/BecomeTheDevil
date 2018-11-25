using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour {
    private Player playerTarget;
    private Statu unitTarget;
    private Enemy2 parent;   // 발사체를 가지고 있는 적. 나중에 공격기능과 AI를 합치면 Enemy2 -> Enemy로 고치자.
    private Enemy enemy;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(unitTarget != null)
        {
            MoveToUnitTarget();
        }
    }

    public void Initialize(Enemy2 parent)
    {
        // 총알을 쏜 적으로 초기화.
        this.parent = parent;
        this.unitTarget = parent.unitTarget;
        //bulletTime = parent.Range / parent.BulletSpeed;
    }
    
    private void MoveToUnitTarget()
    {
        if (unitTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, unitTarget.middlePoint.position, Time.deltaTime * parent.projectileSpeed);
            // 방향이 있는 발사체일 경우 적 방향으로 회전시킴. 
            Vector2 dir = unitTarget.middlePoint.position - transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            // Projectile 이 오른쪽으로 향해 있어야 앞이 적쪽으로 향함.
        }
        else if (/*!playerTarget.IsActive ||*/ unitTarget == null)  // 타겟이 죽으면 발사체 없앰.
        {
            BattleManager.Instance.Pool.ReleaseObject(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어 또는 유닛과 충돌하면
        if (other.tag == "PlayerHitCollider" || other.tag == "UnitHitCollider")
        {
            //if (unitTarget.gameObject == other.gameObject/*&& unitTarget != null*/)
            //{
            
                // 타겟에게 데미지를 주고 발사체를 없앰.
                other.transform.parent.GetComponent<Statu>().TakeDamage(parent.transform.parent.GetComponent<Statu>().attackDamage);
                BattleManager.Instance.Pool.ReleaseObject(gameObject);
            //}
        }
        
    }
    /*
    private void OnTriggerExit2D(Collider2D other)
    {
        BattleManager.Instance.Pool.ReleaseObject(gameObject);
    }*/
}
