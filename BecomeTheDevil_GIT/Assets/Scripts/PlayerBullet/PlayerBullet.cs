﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float bulletSpeed = 5f; // 총알 속도
    [SerializeField]
    private Player parent;

    Vector2 movePos;

    
	// Use this for initialization
	void Start () {
        movePos = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position); // 마우스 위치를 받아옴

    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Transform>().transform.Translate(movePos.normalized * bulletSpeed * Time.deltaTime); // 마우스 방향대로 이동
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag.Equals("Wall"))
            Destroy(this.gameObject);
        if (col.tag == "Enemy")
        {
            col.GetComponent<Statu>().TakeDamage(parent.GetComponent<Statu>().attackDamage);
            Destroy(this.gameObject);
            Debug.Log("Player가 적을 맞춤");
        }
    }

}
