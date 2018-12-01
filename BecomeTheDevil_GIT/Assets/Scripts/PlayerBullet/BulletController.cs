using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    public string projectileType;   // 오브젝트 풀에서 불러올 총알 이름.
    public string projectileType1;
    public string projectileType2;
    public string fakeBulletName;   // 오브젝트 풀에서 불러올 가짜 총알 이름.
    public GameObject bulletPrefab; //발사할 총알
    public GameObject fakeBulletPrefab; // 가짜 총알
    public bool canAttack = true;  //이거 마우스 위치나 유닛 버튼 눌른 상황이면 공격 못하게 할 변수
    
    bool canShoot = true; //총알을 쏠 수 있는 상태 확인

    public bool isClick = false;
    public bool mouseButtonUp = false;



    // bulletSpawnTime > deleBulletTime 이어야 함!
    //public float bulletSpawnTime = 5f; //총알의 재생성 시간
    public float deletBulletTime = 3f; // 총알이 발사되고 삭제되는 시간
    public float bulletDelay = 0.2f;

    float bulletTimer = 0f; // 마지막 총알이 발사되고 경과한 시간
    float clickTimer = 0f; // 마우스를 누르고 있는 시간

    // 공격 스탯.
    Statu stat;
    float temp;
    

    //총알 스폰 포인트
    public GameObject spawnBulletPoint1;
    public GameObject spawnBulletPoint2;
    public GameObject spawnBulletPoint3;

    private GameObject fakeBullet1;
    private GameObject fakeBullet2;
    private GameObject fakeBullet3;
    
    private GameObject bullet1;
    private GameObject bullet2;
    private GameObject bullet3;
    

        
    // Use this for initialization
    void Start () {
        stat = GetComponent<Statu>();
        temp = stat.attackDamage;     // 저장시켜놨다 나중에 초기화
    }
	
	// Update is called once per frame
	void Update () {

        mouseButtonUp = false;

        // 전 프레임에서 마우스를 클릭하고 있었을 경우, ShootBullet()을 호출한다.

        //전 프레임에서 마우스를 클릭하고 있지 않았을 경우,
        //총알을 현재 쏠 수 있는 상태이며 마우스 클릭을 하고 있으면 isClick 값을 true로 변경한다.
        if (isClick)
        {
            clickTimer += Time.deltaTime; // 마우스를 누르고 있는 시간 증가
            
            // 총알 크기 증가. 데미지 증가
            if(bulletPrefab.transform.localScale.x < 1.5) // 최대 1.5배까지 증가.
            {
                stat.attackDamage += 0.2f;
                bulletPrefab.transform.localScale += new Vector3(0.01f, 0.01f, 0);
            }
            
            if (fakeBullet1.transform.localScale.x < 1.5) // 최대 1.5배까지 증가.
            {
                fakeBullet1.transform.localScale += new Vector3(0.01f, 0.01f, 0);
                fakeBullet2.transform.localScale += new Vector3(0.01f, 0.01f, 0);
                fakeBullet3.transform.localScale += new Vector3(0.01f, 0.01f, 0);
            }
            
            /*
            Debug.Log("눌리고있따");
            if (fake1.gameObject.transform.localScale.x < 2)
            {
                fake1.gameObject.transform.localScale += new Vector3(0.005f, 0.005f, 0);
                fake2.gameObject.transform.localScale += new Vector3(0.005f, 0.005f, 0);
                fake3.gameObject.transform.localScale += new Vector3(0.005f, 0.005f, 0);
            }*/
            if (Input.GetMouseButtonUp(0))  // 마우스 버튼을 떼면.
            {
                ShootBullet();      // 총알 발사.
            }
        }
        else if(Input.GetMouseButtonDown(0) && canShoot && canAttack && !InvenVisible.isInvenOpen) 
        {
            isClick = true;
            SpawnBullet();
        }

        

        //총알이 발사되고 경과된 시간을 측정한다.
        if (canShoot == false)
        {
            bulletTimer += Time.deltaTime;
        }

        // 총알이 발사 되고 경과한 시간이 재생성 시간 보다 크거나 같을 경우 총알 생성
        if (bulletTimer >= stat.attackCoolTime) 
        {
            bulletTimer = 0f;
            canShoot = true;
        }
        /*
        //총알이 발사되고 경과된 시간이 총알 삭제시간보다 크거나 같으면 발사된 총알을 삭제한다.
        if (bulletTimer >= deletBulletTime)
        {
            Destroy(bullet1);
            Destroy(bullet2);
            Destroy(bullet3);
        }*/

    }

    // 마우스를 클릭하지 않고 있을 경우, 총알을 발사한다.
    void ShootBullet()
    {
        
            // 머리 위에 떠있는 가짜 총알 삭제
            
            Destroy(fakeBullet1);
            Destroy(fakeBullet2);
            Destroy(fakeBullet3);
            
            // 날아가는 총알을 생성 후, 발사
            //bullet1 = Instantiate<GameObject>(bulletPrefab, spawnBulletPoint1.transform.position, Quaternion.identity);
            //bullet2 = Instantiate<GameObject>(bulletPrefab, spawnBulletPoint2.transform.position, Quaternion.identity);
            //bullet3 = Instantiate<GameObject>(bulletPrefab, spawnBulletPoint3.transform.position, Quaternion.identity);
            

            //mouseButtonUp = true;
        
        // 오브젝트풀에서 총알을 불러와 생성 및 초기화.
        PlayerBullet bullet1 = BattleManager.instance.Pool.GetObject(projectileType).GetComponent<PlayerBullet>();
        bullet1.transform.position = spawnBulletPoint1.transform.position;
        bullet1.Initialize(this);
        bullet1.Start(); // 중요함. 오브젝트 풀 특성상 마우스 포지션을 다시 Start를 실행해 받아와야 함.
        /*PlayerBullet bullet2 = BattleManager.instance.Pool.GetObject(projectileType).GetComponent<PlayerBullet>();
        bullet2.transform.position = spawnBulletPoint3.transform.position;
        bullet2.Initialize(this);
        bullet2.Start();*/
        /*PlayerBullet bullet3 = BattleManager.instance.Pool.GetObject(projectileType).GetComponent<PlayerBullet>();
        bullet3.transform.position = spawnBulletPoint2.transform.position;
        bullet3.Initialize(this);
        bullet3.Start();*/
        StartCoroutine(ShootBullet2());
        StartCoroutine(ShootBullet3());

        /*
        bullet1 = BattleManager.instance.Pool.PopFromPool(projectileType);
        bullet1.transform.position = spawnBulletPoint1.transform.position;
        bullet2 = BattleManager.instance.Pool.PopFromPool(projectileType);
        bullet1.transform.position = spawnBulletPoint1.transform.position;
        bullet3 = BattleManager.instance.Pool.PopFromPool(projectileType);
        bullet1.transform.position = spawnBulletPoint1.transform.position;
        */

         canShoot = false; // 총알을 발사할 수 있는 상태가 아님으로 false로 값 변경
         isClick = false; // 마우스를 누르지 않는 상태이므로 false로 값 변경 
         clickTimer = 0f; // 마우스를 누르고 있는 시간 초기화

        StartCoroutine(InitialSize());
         // 크기, 데미지 초기화.
         /*fakeBulletPrefab.transform.localScale = new Vector3(1f, 1f, 1f);
         bulletPrefab.transform.localScale = new Vector3(1f, 1f, 1f);
         stat.attackDamage = temp;*/
    }

    IEnumerator ShootBullet2()
    {
        yield return new WaitForSeconds(bulletDelay);
        PlayerBullet bullet2 = BattleManager.instance.Pool.GetObject(projectileType).GetComponent<PlayerBullet>();
        bullet2.transform.position = spawnBulletPoint3.transform.position;
        bullet2.Initialize(this);
        bullet2.Start();
    }

    IEnumerator ShootBullet3()
    {
        yield return new WaitForSeconds(2*bulletDelay);
        PlayerBullet bullet3 = BattleManager.instance.Pool.GetObject(projectileType).GetComponent<PlayerBullet>();
        bullet3.transform.position = spawnBulletPoint2.transform.position;
        bullet3.Initialize(this);
        bullet3.Start();
    }

    IEnumerator InitialSize()
    {
        yield return new WaitForSeconds(2*bulletDelay+0.05f);
        fakeBulletPrefab.transform.localScale = new Vector3(1f, 1f, 1f);
        bulletPrefab.transform.localScale = new Vector3(1f, 1f, 1f);
        stat.attackDamage = temp;
    }

    //총알을 spawnBulletPoint에 생성한다.
    void SpawnBullet()
    {
        fakeBullet1 = Instantiate<GameObject>(fakeBulletPrefab, spawnBulletPoint1.transform.position, Quaternion.identity);
        fakeBullet1.transform.parent = transform;
        fakeBullet2 = Instantiate<GameObject>(fakeBulletPrefab, spawnBulletPoint2.transform.position, Quaternion.identity);
        fakeBullet2.transform.parent = transform;
        fakeBullet3 = Instantiate<GameObject>(fakeBulletPrefab, spawnBulletPoint3.transform.position, Quaternion.identity);
        fakeBullet3.transform.parent = transform;

        /*
        FakeBullet fake1 = BattleManager.instance.Pool.GetObject(fakeBulletName).GetComponent<FakeBullet>();
        fake1.transform.position = spawnBulletPoint1.transform.position;
        fake1.transform.parent = transform; // 플레이어를 따라다니게.
        fake1.Initialize(this);
        FakeBullet fake2 = BattleManager.instance.Pool.GetObject(fakeBulletName).GetComponent<FakeBullet>();
        fake2.transform.position = spawnBulletPoint2.transform.position;
        fake2.transform.parent = transform;
        fake2.Initialize(this);
        FakeBullet fake3 = BattleManager.instance.Pool.GetObject(fakeBulletName).GetComponent<FakeBullet>();
        fake3.transform.position = spawnBulletPoint3.transform.position;
        fake3.transform.parent = transform;
        fake3.Initialize(this);
        isSpawn = true;
        
        if (Input.GetMouseButton(0) && canShoot)   // 마우스 버튼이 눌리고 있을 때 
        {
            Debug.Log("눌리고있따");
            if (fake1.gameObject.transform.localScale.x < 2)
            {
                fake1.gameObject.transform.localScale += new Vector3(0.005f, 0.005f, 0);
                fake2.gameObject.transform.localScale += new Vector3(0.005f, 0.005f, 0);
                fake3.gameObject.transform.localScale += new Vector3(0.005f, 0.005f, 0);
            }
        }
        */

    }

}
