using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public float speed = -1.0f;
    private Animator animator;
    [SerializeField]
    private float monsterHealth = 10f;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public bool Alive
    {
        get
        {
            return monsterHealth > 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);       // 거리를 주는게 아니라 속도와 delta time으로.
    }
}
