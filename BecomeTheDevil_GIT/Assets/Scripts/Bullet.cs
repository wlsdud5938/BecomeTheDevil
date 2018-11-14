using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    public float bulletDamage = 20f;      // 플레이어 공격력
    public float bulletSpeed = 3f;  // 플레이어 총알 속도
    public float bulletRange = 3f;

    // Use this for initialization
    protected void Start () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(BattleManager.instance.humanPlayerBulletDirX*bulletSpeed, BattleManager.instance.humanPlayerBulletDirY*bulletSpeed);
       
        Destroy(this.gameObject, bulletRange / bulletSpeed);
	}
	
	// Update is called once per frame
	void Update () {
     
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Enemy") || other.tag.Equals("Unit"))
        { //부딪힌 객체가 적인지 검사합니다.
            if(other.tag.Equals("Enemy"))
            {
                //other.GetComponent<Enemy>().TakeDamage(bulletDamage);
                other.GetComponent<Statu>().TakeDamage(bulletDamage);
            }
            Destroy(this.gameObject); //자기 자신을 지웁니다.

        }
    }

}
