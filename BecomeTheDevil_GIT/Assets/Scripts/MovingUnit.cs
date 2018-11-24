using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUnit : MonoBehaviour {

    int horizontal = 0;
    int vertical = 0;
    float speed = 3f;
    private void Update()
    {
        Move();
    }
    void Move()
    {
        float xMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime; //x축으로 이동할 양 
        float yMove = Input.GetAxis("Vertical") * speed * Time.deltaTime; //y축으로 이동할양 
        this.transform.Translate(new Vector3(xMove, yMove, 0));  //이동


    }
}// 실제로 움직이는 함수 
