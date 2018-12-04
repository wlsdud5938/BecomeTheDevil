using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float bulletSpeed = 10f; // 총알 속도
    public float bulletTime = 0f;
    public float bulletTime2 = 0f;
    public float deleteBulletTime = 1f;
    public float attackDamage;
    public float attackRange;
    Rigidbody2D rigidBody;

    public BulletController parent;
    public Statu statu;
    public Vector2 mousePosition;  // 오브젝트풀링특성상 업데이트에서 불값을 확인하면서 마우스포지션을 받아야 함.

    public Vector2 movePos;
    private Vector2 vector2Position;// 회전 연산때문에 Vector2형태의 playerPosition
    private Vector3 playerPosition; // 총알이 발사될 때의 플레이어 위치.
    public float distance;  // 총알과 플레이어 사이의 거리, 사거리에 사용.
    
	// Use this for initialization
	public void Start () {      // Awake로 하면 에러.
        // 마우스 위치를 받아옴
        rigidBody = GetComponent<Rigidbody2D>();
        movePos = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        statu = parent.GetComponent<Statu>();
        attackDamage = statu.attackDamage;
        attackRange = statu.attackRange;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mousePosition);
        // 총알이 생성될 때 플레이어 위치 저장.
        playerPosition = parent.transform.position;
        vector2Position.x = transform.position.x;
        vector2Position.y = transform.position.y;
        // 크기도 받아옴.
        transform.localScale = parent.bulletPrefab.transform.localScale;
        
        // 총알 마우스 방향으로 rotate.
        Vector2 dir = mousePosition - vector2Position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.Find("Sprite").transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.Find("Sprite").transform.Rotate(Vector3.forward, angle);
    }
	
	// Update is called once per frame
	void Update () {
        // 마우스 방향대로 이동
        //movePos = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);

        // 오브젝트 풀링은 SetActive를 껐다 켰다 하는거기 때문에 마우스 위치를
        // Update에서 받아와야 함.
        /*
        if (mousePosition)      
        {
            movePos = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            mousePosition = false;
        }*/

        //GetComponent<Transform>().transform.Translate(movePos.normalized * bulletSpeed * Time.deltaTime); 

        MoveToMouse();
        //transform.position = Vector3.MoveTowards(transform.position, mousePosition, Time.deltaTime * bulletSpeed);
        //rigidBody.AddForce(movePos);

        distance = Vector3.Distance(playerPosition, transform.position);
        if (attackRange < distance)    // 사거리보다 길어지면.
        {
            //transform.localScale = new Vector3(1f, 1f, 1f); //크기 초기화.
            BattleManager.Instance.Pool.ReleaseObject(gameObject);
        }
        /*
        Vector2 dir = movePos - vector2Position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        */
        //bulletTime += Time.deltaTime;
        //bulletTime2 += Time.deltaTime;
        /*
        // 총알 쿨타임이 돌아오면 mousePosition 다시 받을 준비
        if (bulletTime2 > parent.GetComponent<Statu>().attackCoolTime)
        {
            mousePosition = true;
            bulletTime2 = 0;
        }*/
        /*
        if (bulletTime > deleteBulletTime)     // 지정 시간이 지나면 총알 없어짐.
        {
            BattleManager.Instance.Pool.ReleaseObject(gameObject);
            //Destroy(gameObject);
            bulletTime = 0;
        }*/



        // 총알 크기 증가.
    }

    public void MoveToMouse()
    {
        transform.Translate(movePos.normalized * bulletSpeed * Time.deltaTime);
    }

    public void Initialize(BulletController parent)
    {
        this.parent = parent;
        //this.attackDamage = parent.GetComponent<Statu>().attackDamage;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Wall"))
        {
            //Destroy(this.gameObject);
            BattleManager.Instance.Pool.ReleaseObject(gameObject);
        }
            
        if (col.tag == "EnemyHitCollider")
        {
            
            col.transform.parent.GetComponent<Statu>().TakeDamage(attackDamage);
            //Destroy(this.gameObject);
            //transform.localScale = new Vector3(1f, 1f, 1f); // 적맞추면 크기 초기화.
            BattleManager.Instance.Pool.ReleaseObject(gameObject);
            //Debug.Log("Player가 적을 맞춤");
            // IceBullet일 경우 적을 느리게 함.
            if (parent.projectileType == "PlayerIceBullet")
            {
                //Debug.Log("아이스 공격!");
                col.transform.parent.GetComponent<Statu>().IceDamage(3f);
            }
            //bulletTime = 0;
        }
    }
    /*
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PlayerRange")
        {
            //Debug.Log("적 나감");
            //Debug.Log(enemy.Count);
            Debug.Log("플레이어 총알이 범위를 벗어남");
            BattleManager.Instance.Pool.ReleaseObject(gameObject);
        }
    }*/

}
