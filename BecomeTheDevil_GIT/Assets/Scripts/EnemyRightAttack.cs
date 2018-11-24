using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRightAttack : MonoBehaviour {

    private Animator myAnimator;

    // Use this for initialization
    void Awake()
    {
        myAnimator = transform.root.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.root.tag == "Player" || other.transform.root.tag == "Unit")
        {
            myAnimator.SetFloat("AttackX", 1.0f);
            //myAnimator.SetFloat("AttackY", 0.0f);
        }
    }
}
