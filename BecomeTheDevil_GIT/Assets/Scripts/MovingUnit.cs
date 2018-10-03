using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingUnit : MonoBehaviour {
    public float moveTime = 0.1f; // 오브젝트를 움직이게 할 시간 단위
    public LayerMask blockingLayer; // (적 벽 플레이어 유닛 모두 이 레이어)


    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime; // 움직임을 효율적으로




	// Use this for initialization
	protected virtual void Start () {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1 / moveTime;
	}

    protected bool Move(int xDir, int yDir, out RaycastHit2D hit){
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        boxCollider.enabled = false; 
        //ray를 사용할 때 자기 자신의 충돌체에 부딫히는 것을 막기 위해 잠시 boxcollider 끔
        hit = Physics2D.Linecast(start, end, blockingLayer);
        //시작점과 가야하는 점에 blocking layer가 있나를 검사 null일 경우 움직일 수 있음
        boxCollider.enabled = true;
        //다시 boxCollider 사용 


        if(hit.transform == null){
            StartCoroutine(SmoothMovement(end));
            return true;
        }//갈 수 있는 경우

        return false;
        //이동 실패할 경우
    } //움직일 수 있느냐 없느냐를 확인하여 움직일 수 있는 경우 smoothmovement 호출하여

    protected virtual void AttempMove<T>(int xDir, int yDir)
        where T : Component{
        RaycastHit2D hit;

        bool canMove = Move(xDir, yDir, out hit);

    }// 움직임을 시도한다. 움직일 수 있으면 움직인다


    protected IEnumerator SmoothMovement(Vector3 end){
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        //magintude - 벡터 길이 sqrMagintude - 벡터 길이 제곱a

        while(sqrRemainingDistance > float.Epsilon){
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            //moveToward 함수는 직선상에 물체 이동
            //inverseMoveTime * Time.deltaTime라는 단위 동안 end에 얼마나 가깝게 갔는 지를 리턴한다
            rb2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null; //루프를 갱신하기 전에 다음 프레임을 기다린다
        }
    }


}// 실제로 움직이는 함수 
