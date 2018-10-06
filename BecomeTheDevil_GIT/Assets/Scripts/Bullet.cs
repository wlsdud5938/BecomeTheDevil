using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    public int bulletDamage = 20;
    public const float bulletSpeed = 0.45f;
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        float moveY = bulletSpeed * Time.deltaTime;
        //이동할 거리를 지정해 줍시다.
        transform.Translate(0, moveY, 0);
        //이동을 반영해줍시다.
    }
}
