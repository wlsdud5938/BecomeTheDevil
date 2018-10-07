using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    [SerializeField]
    public Unit unit;

    private SpriteRenderer mySpriteRenderer;

    private Saitama target;
    private Queue<Saitama> enemy = new Queue<Saitama>();
    private bool canAttack = true;
    private float attackTimer = 0.1f;

	// Use this for initialization
	void Start () {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Attack();
        Debug.Log(target);
	}

    public void Select()
    {

    }

    private void Attack()
    {
        if(target == null && enemy.Count > 0)   // target == null 한 Saitama가 원 밖으로 나갈 때.
        {
            target = enemy.Dequeue();      // enemy Queue에 있는 걸 빼면서 아직 원 안에 있는 Saitama를 타겟으로 지정.
        }

        if(target != null && target.isActiveAndEnabled)     // 타겟이 원 안에 살아 있다면
        {
            if (canAttack)
            {
                Shoot();
                canAttack = false;
            }
        }
    }

    private void Shoot()
    {
        unit.GoAttack();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            enemy.Enqueue(other.GetComponent<Saitama>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            target = null;
        }
        unit.StopAttack();
    }


}
