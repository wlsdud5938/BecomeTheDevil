using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{

    // Use this for initialization
    void Start () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(BattleManager.instance.humanPlayerBulletDirX, BattleManager.instance.humanPlayerBulletDirY);
	}
	
	// Update is called once per frame
	void Update () {
     
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Enemy"))
        { //부딪힌 객체가 적인지 검사합니다.
            //Destroy(other.gameObject); //부딪힌 적을 지웁니다.
            Destroy(this.gameObject); //자기 자신을 지웁니다.

        }
    }

}
