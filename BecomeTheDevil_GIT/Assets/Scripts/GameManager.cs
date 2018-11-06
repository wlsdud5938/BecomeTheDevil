using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public GameObject enemy;
    public float nextWaveTime;
    public int numOfEnemyPerWave;
    public GameObject entryRoom;
    public float[] maxHpOfEnemy; // 기본 에너미 hp


    private float enemySpawnTimer = 0.0f;
    private Dictionary<int, Enemy> enemyInstanceMap = new Dictionary<int, Enemy>();
    // Use this for initialization
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemySpawnTimer += Time.deltaTime;
        if(enemySpawnTimer >= nextWaveTime)
        {
            enemySpawnTimer = 0.0f;
            for (int i = 0; i < numOfEnemyPerWave; i++)
            {
                GameObject clone = Instantiate(enemy, entryRoom.transform.GetChild(0).transform.position, Quaternion.identity);
                clone.GetComponent<Enemy>().versionType = Random.Range(0, 3); //생성한 오브젝트에 script를 가져와 변수에 접근해서 0~2 랜덤하게 초기화.
            }


        }
    }
}