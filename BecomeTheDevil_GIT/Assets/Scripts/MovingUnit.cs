using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingUnit : MonoBehaviour {
    public float moveTime = 0.1f; // 오브젝트를 움직이게 할 시간 단위
    public LayerMask blockingLayer; // (적 벽 플레이어 유닛 모두 이 레이어)
    private BoxCollider2D[] boxCollider; // 배열로 미리 선언해둡니다. 11/12 김성철 변경점.
    private Rigidbody2D rb2D;
  



	// Use this for initialization
	protected virtual void Start () {
        boxCollider = GetComponents<BoxCollider2D>(); // s 추가해서 컴포넌트 "들"을 받아왔습니다. boxCollider는 배열이 됩니다.
        rb2D = GetComponent<Rigidbody2D>();
        blockingLayer = 1 << 10;
      
	}

    protected bool Move(float xDir, float yDir, out RaycastHit2D hit){
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);


        boxCollider[0].enabled = false;// 배열로 받은 collider 2개를 모두 처리합니다.
        boxCollider[1].enabled = false;
        //ray를 사용할 때 자기 자신의 충돌체에 부딫히는 것을 막기 위해 잠시 boxcollider 끔
        hit = Physics2D.Linecast(start, end, blockingLayer);
        //시작점과 가야하는 점에 blocking layer가 있나를 검사 null일 경우 움직일 수 있음
        boxCollider[0].enabled = true;
        boxCollider[1].enabled = true; // 2개 뿐이라 for문 안돌렸는데, 많아지면 for문 돌려야할 것 같습니다.
        //다시 boxCollider 사용 

        if (hit.transform == null){
            transform.position = end;
            return true;
        }//갈 수 있는 경우

        return false;
        //이동 실패할 경우
    } //움직일 수 있느냐 없느냐를 확인하여 움직일 수 있는 경우 smoothmovement 호출하여

    protected virtual void AttemptMove(float xDir, float yDir)
    {
        RaycastHit2D hit;

        bool canMove = Move(xDir, yDir, out hit);

    }// 움직임을 시도한다. 움직일 수 있으면 움직인다
   


}// 실제로 움직이는 함수 
