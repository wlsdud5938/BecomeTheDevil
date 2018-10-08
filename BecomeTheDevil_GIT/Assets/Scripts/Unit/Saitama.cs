using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saitama : MonoBehaviour
{
    public float speed = -1.0f;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);       // 거리를 주는게 아니라 속도와 delta time으로.
    }
}