using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownAttackScript : MonoBehaviour {

    private Animator myAnimator;

    // Use this for initialization
    void Awake()
    {
        myAnimator = transform.parent.parent.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            myAnimator.SetFloat("AttackX", 0.0f);
            myAnimator.SetFloat("AttackY", -1.0f);
        }
    }
    /*
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            myAnimator.SetFloat("AttackX", 0.0f);
            myAnimator.SetFloat("AttackY", 0.0f);
        }
    }*/
}
