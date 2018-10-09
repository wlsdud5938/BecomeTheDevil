﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    [SerializeField]        // 유니티 에디터에서 보임.
    private string projecttileType;     // 유닛에 따라 공격을 다르게.

    private SpriteRenderer mySpriteRenderer;        // 사정거리를 보여줌.

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
        Targetting();
        Debug.Log(target);
	}

    public void Select()
    {

    }

    private void Targetting()
    {
        if( enemy.Count > 0 && target == null )   // target == null 한 Saitama가 원 밖으로 나갈 때.
        {
            target = enemy.Dequeue();      // enemy Queue에 있는 걸 빼면서 아직 원 안에 있는 Saitama를 타겟으로 지정.
        }

        if(target != null)     // 타겟이 원 안에 살아 있다면
        {
            if (canAttack)
            {
                //Attack();
                Unit.Instance.animator.SetBool("Attack", true);
                canAttack = false;
            }
        }
    }
    
    private void Attack()
    {
        //Unit unit = GameObject.FindObjectOfType<Unit>();
        //unit.GoAttack();
        Unit.Instance.GoAttack();
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
        //Unit.Instance.StopAttack();
        Unit.Instance.animator.SetBool("Attack", false);
    }


}