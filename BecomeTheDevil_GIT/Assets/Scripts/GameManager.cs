using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //유니티 창에서 조절해야 하는 값
    public GameObject enemy; 
    public float nextWaveTime;  //현재 웨이브 다 잡고 다음 웨이브까지 시간 
    public int numOfEnemyPerWave; //웨이브당 생성되는 적 숫자
    public GameObject entryRoom;
    public float[] maxHpOfEnemy; // 기본 에너미 hp
    public float levelBalanceConst = 1.1f; // 다음 웨이브에서 강해지는 기본 스탯 비율






    public int idxOfWave = 0;  //웨이브 라운드 넘버
    public int currNumOfEnemyes=0; //현재 맵에 총 적 숫자 Tag로 찾을 경우 성능 저하


    float enemySpawnTimer = 0.0f;
   
    Dictionary<int, Enemy> enemyInstanceMap = new Dictionary<int, Enemy>();

    // Use this for initialization
    void Awake()
    {
        Screen.SetResolution(1920, 1080,true);
        
    }

    // Update is called once per frame
    void Update()
    {

        if(currNumOfEnemyes==0)
            enemySpawnTimer += Time.deltaTime;
        //씬에 에너미 0일 때 enemySpawnTimer 증가 
        // 적 다 잡고 유저한테 일정 시간 후에 적 스폰

        if(enemySpawnTimer >= nextWaveTime)
        {
            currNumOfEnemyes = numOfEnemyPerWave; //웨이브 생성 순간, 현재 적 숫자를 웨이브당 적 생성 숫자로 초기화
            enemySpawnTimer = 0.0f;
            for (int i = 0; i < numOfEnemyPerWave; i++)
            {
                //GameObject clone = Instantiate(enemy, entryRoom.transform.GetChild(0).transform.position, Quaternion.identity);
                //clone.GetComponent<Statu>().versionType = Random.Range(0, 3); //생성한 오브젝트에 script를 가져와 변수에 접근해서 0~2 랜덤하게 초기화.
            }
            idxOfWave++; // 추후 신마다 초기화하는 기능 추가
        }
    }
 
}