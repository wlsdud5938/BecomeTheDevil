using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public GameObject bulletPrefab; //발사할 총알
    public GameObject fakeBulletPrefab; // 가짜 총알
    
    bool canShoot = true; //총알을 쏠 수 있는 상태 확인

    bool isClick = false;


    // bulletSpawnTime > deleBulletTime 이어야 함!
    public float bulletSpawnTime = 5f; //총알의 재생성 시간
    public float deletBulletTime = 3f; // 총알이 발사되고 삭제되는 시간

    float bulletTimer = 0f; // 마지막 총알이 발사되고 경과한 시간
    float clickTimer = 0f; // 마우스를 누르고 있는 시간

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

    }
	
	// Update is called once per frame
	void Update () {



        // 전 프레임에서 마우스를 클릭하고 있었을 경우, ShootBullet()을 호출한다.

        //전 프레임에서 마우스를 클릭하고 있지 않았을 경우,
        //총알을 현재 쏠 수 있는 상태이며 마우스 클릭을 하고 있으면 isClick 값을 true로 변경한다.
        if (isClick)
        {
            clickTimer += Time.deltaTime; // 마우스를 누르고 있는 시간 증가
            ShootBullet();
        }
        else if(Input.GetMouseButtonDown(0) && canShoot)
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
        if (bulletTimer >= bulletSpawnTime) 
        {
            bulletTimer = 0f;
            canShoot = true;
        }

        //총알이 발사되고 경과된 시간이 총알 삭제시간보다 크거나 같으면 발사된 총알을 삭제한다.
        if (bulletTimer >= deletBulletTime)
        {
            Destroy(bullet1);
            Destroy(bullet2);
            Destroy(bullet3);
        }

    }

    // 마우스를 클릭하지 않고 있을 경우, 총알을 발사한다.
    void ShootBullet()
    {

        if (Input.GetMouseButtonUp(0))
        {

            // 머리 위에 떠있는 가짜 총알 삭제
            Destroy(fakeBullet1);
            Destroy(fakeBullet2);
            Destroy(fakeBullet3);

            // 날아가는 총알을 생성 후, 발사
            bullet1 = Instantiate<GameObject>(bulletPrefab, spawnBulletPoint1.transform.position, Quaternion.identity);
            bullet2 = Instantiate<GameObject>(bulletPrefab, spawnBulletPoint2.transform.position, Quaternion.identity);
            bullet3 = Instantiate<GameObject>(bulletPrefab, spawnBulletPoint3.transform.position, Quaternion.identity);

            canShoot = false; // 총알을 발사할 수 있는 상태가 아님으로 false로 값 변경
            isClick = false; // 마우스를 누르지 않는 상태이므로 false로 값 변경 
            clickTimer = 0f; // 마우스를 누르고 있는 시간 초기화
        }

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
    }

}
