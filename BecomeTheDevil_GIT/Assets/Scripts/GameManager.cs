using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    //유니티 창에서 조절해야 하는 값
    public GameObject enemy; 
    public float nextWaveTime =3f;  //현재 웨이브 다 잡고 다음 웨이브까지 시간 
    public int numOfEnemyPerWave; //웨이브당 생성되는 적 숫자
    public GameObject entryRoom;
    public float[] maxHpOfEnemy; // 기본 에너미 hp
    public float levelBalanceConst = 1.1f; // 다음 웨이브에서 강해지는 기본 스탯 비율
    public GameObject stoneItemImage; //돌 아이템
    public GameObject iceItemImage; //얼음 아이템
    public Slider itemSlider;
    public int idxOfWave = 0;  //웨이브 라운드 넘버
    public int currNumOfEnemyes=0; //현재 맵에 총 적 숫자 Tag로 찾을 경우 성능 저하
    public float enemySpawnTimer = 0.0f;
    RoomTemplates temp;
    Dictionary<int, Enemy> enemyInstanceMap = new Dictionary<int, Enemy>();
    public int coin;
    public Text coinText;
    public float playTime = 0f;
    public float potalTime = 500f;
    public GameObject clearPotal;   //클리어포탈
    int random;
    public bool spawnKey = false;
    // 적 스폰 시간
    private float enemySpawnTerm = 2.0f;  //  스폰 term.
    private float spawnTime = 0f;       // 
    // Use this for initialization
    void Awake()
    {
        Screen.SetResolution(1920, 1080,true);
        temp = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = coin.ToString();
        if(temp.spawnedBoss)
            playTime += Time.deltaTime;
        if (currNumOfEnemyes <= 0)
        {
            enemySpawnTimer += Time.deltaTime;
        }
        //씬에 에너미 0일 때 enemySpawnTimer 증가 
        // 적 다 잡고 유저한테 일정 시간 후에 적 스폰
        if(temp.spawnedBoss == true && enemySpawnTimer >= nextWaveTime&&currNumOfEnemyes<=0)
        {
            if (playTime >= potalTime)
                SpawnPotal();
            enemySpawnTimer = 0.0f;
            currNumOfEnemyes = 0;
            for (int i = 0; i < numOfEnemyPerWave; i++)
            {
                spawnTime += enemySpawnTerm;
                StartCoroutine(MakeClone(spawnTime));
            }

            
            
            idxOfWave++; // 추후 신마다 초기화하는 기능 추가
        }
    }

    public void SpawnPotal()
    {
        clearPotal.SetActive(true);
        random = Random.Range(1, temp.rooms.Count-1);
        clearPotal.transform.position = temp.rooms[random].GetComponent<MapNode>().realMap.transform.position;
        
    }

    public void ClosePotal()
    {
        clearPotal.SetActive(false);
    }

    public void EquipStoneItem(int valueOfSlider){
        DequipItem();
        stoneItemImage.SetActive(true); // 돌아이템 이미지 띄우고
        itemSlider.value = valueOfSlider;

    }
    public void EquipIceItem(int valueOfSlider){
        DequipItem();
        iceItemImage.SetActive(true); //얼음 아이템 이미지 끄고
        itemSlider.value = valueOfSlider;
    }

    public void DecreaseItemSlider(){
        float value = itemSlider.value;
        itemSlider.value = value--;
    }
    public void DequipItem(){
        stoneItemImage.SetActive(false);
        iceItemImage.SetActive(false);
        itemSlider.value = 0;
    }



    IEnumerator MakeClone(float time)
    {
        yield return new WaitForSeconds(time);
        //Debug.Log("적생성!");
        GameObject clone = Instantiate(enemy, entryRoom.transform.position, Quaternion.identity);
        clone.transform.Find("Enemy").GetComponent<Statu>().versionType = Random.Range(0, 3); //생성한 오브젝트에 script를 가져와 변수에 접근해서 0~2 랜덤하게 초기화.
        currNumOfEnemyes++;
    }


}