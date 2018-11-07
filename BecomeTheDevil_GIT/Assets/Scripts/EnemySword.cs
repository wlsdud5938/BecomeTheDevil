using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour {
    private Player target;
    private Tower parent;   // 발사체를 가지고 있는 타워.

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 적과 충돌하면
        if (other.tag == "Player")
        {
            if (target.gameObject == other.gameObject && target != null)
            {
                // 타겟에게 데미지를 주고 발사체를 없앰.
                other.GetComponent<Statu>().TakeDamage(parent.Damage);
                BattleManager.Instance.Pool.ReleaseObject(gameObject);
            }
        }
    }
}
