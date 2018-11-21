using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLeftAttack : MonoBehaviour {

    private Animator myAnimator;

    // Use this for initialization
    void Awake()
    {
        myAnimator = transform.parent.parent.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Unit")
        {
            myAnimator.SetFloat("AttackX", -1.0f);
            myAnimator.SetFloat("AttackY", 0.0f);
        }
    }
}
