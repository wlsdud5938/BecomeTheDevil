using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveSound : MonoBehaviour {
    public AudioSource[] audios;
    private AudioClip audio1;
    private AudioClip audio2;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        audios[0].Play();
    }

    // Update is called once per frame

    void Update()
    {
        if (animator.GetBool("isMoving") == true)
        {
            audios[0].mute = false;
        }
        if (animator.GetBool("isMoving") == false)
        {
            audios[0].mute = true;
        }
        if (Input.GetKeyDown("space"))
        {
            audios[1].Play();
        }
    }
}