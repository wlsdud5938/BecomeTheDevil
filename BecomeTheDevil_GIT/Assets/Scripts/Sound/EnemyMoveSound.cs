using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveSound : MonoBehaviour
{
    public AudioSource audio1;
    public AudioClip audio;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        audio1 = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        audio1.Play();
    }

    // Update is called once per frame

    void Update()
    {
        if (animator.GetBool("EnemyRun") == true)
        {
            audio1.mute = false;
        }
        if (animator.GetBool("EnemyRun") == false)
        {
            audio1.mute = true;
        }
    }
}
