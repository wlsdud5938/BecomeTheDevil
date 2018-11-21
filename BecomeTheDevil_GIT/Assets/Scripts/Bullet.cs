/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    //public float bulletDamage = 20f;      // 플레이어 공격력
    //public float bulletSpeed = 3f;  // 플레이어 총알 속도
    //public float bulletRange = 3f;

    private Player parent;
    private float bulletTime;   // 총알 지속 시간.
    private float second;     // 총알 지속 시간.

    // Use this for initialization
    void Awake () {
        
        //Destroy(this.gameObject, parent.Range / parent.BulletSpeed);
        // 이것땜에 총알이 재사용이 안되는데 뭐지?? 
    }
	
	// Update is called once per frame
	void Update () {
        second += Time.deltaTime;
        if(second > bulletTime)     // 지속시간을 넘어가면 총알을 없앰.
        {
            BattleManager.Instance.Pool.ReleaseObject(gameObject);
            second = 0f;
        }
    }

    public void Initialize(Player parent)
    {
        // 총알을 쏜 플레이어로 초기화.
        this.parent = parent;
        bulletTime = parent.GetComponent<Statu>().attackDamage / parent.GetComponent<Statu>().attackSpeed;
        // 타게팅을 마우스로 바꾸면 인자를 바껴야 함.
        GetComponent<Rigidbody2D>().velocity = new Vector2(parent.Cur_hor * parent.BulletSpeed, parent.Cur_ver * parent.BulletSpeed);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals("Enemy"))
        {
            //other.GetComponent<Enemy>().TakeDamage(bulletDamage);
            other.GetComponent<Statu>().TakeDamage(parent.Damage);
            //Destroy(this.gameObject); //자기 자신을 지웁니다.
            BattleManager.Instance.Pool.ReleaseObject(gameObject);
        }
    }

}*/
