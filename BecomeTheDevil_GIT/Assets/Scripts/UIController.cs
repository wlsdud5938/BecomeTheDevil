using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    //타워 버튼 클릭하면 유닛 버튼 생서
    public Button towerButton;   // 타워 버튼
    public Button[] unitButtons; // 유닛 버튼
    public Image arrowImage; //타워버튼 위에 삼각형
    public GameObject[] units;  //생성되어야 하는 unit 프리팹 어레이
    public float buttonInterval = 8f; // 유닛 버튼 간격
    public float arrowInterval = 10f; // 애로우 사이의 간격
    public float xPosMax = 47, xPosMin = 33; //맵 최대 최소 x,y 좌표 (맵크기)
    public float yPosMax = -10, yPosMin = -19;

    //마우스 바꿔야함
    public Texture2D inMapCursor;  //맵 안에서 커서
    public Texture2D outMapCursor; //맵 밖에서 커서
    public Texture2D[] canBuildUnitCursor;  //유닛 생성하려고 좌표 찍으려 할 때 가능한 위치일 때 커서
    public Texture2D[] cantBuildUnitCursor; //유닛 생성하려고 좌표 찍으려 할 때 불가능한 위치일 때 커서

    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public Vector2 unitSpawnSpot = Vector2.zero;
    public Vector2 mouseTarget = Vector2.zero;


   
    GameManager gameManager;
    Camera camera;

    BulletController playerBC; // 플레이어 불렛 컨트롤러

    bool isClickedTB = false; //towerButton 눌린 상태냐고
    bool isClickedUB = false; //unitButton 눌린 상태인지
    int idxOfClickedUB; //생성할 유닛에 인덱스
    Button[] cloneUnitButton; //생성한 유닛 버튼 그 자체
    Button cloneTowerButton; //생성한 타워 버튼 
    Image cloneTriangleImage; //생성한 트라이앵글 이미지

    Vector2 spawnPos = Vector2.zero;


    // Use this for initialization
    void Start () {
        playerBC = GameObject.FindWithTag("Player").GetComponent<BulletController>(); //플레이어 불렛 컨트롤러
        cloneUnitButton = new Button[unitButtons.Length];
        //타워버튼 생성
        cloneTowerButton = Instantiate(towerButton, towerButton.transform.position, Quaternion.identity); 
        cloneTowerButton.onClick.AddListener(()=>GenerateUnitButtons()); //click 이벤트 연결
        cloneTowerButton.transform.SetParent(gameObject.transform,false);

        gameManager = GameManager. Instance; //게임 매니저 캐싱


    }
    // Update is called once per frame
    void Update()
    {
        camera = Camera.main;
        unitSpawnSpot = camera.ScreenToWorldPoint(Input.mousePosition); // 진짜 유닛이 생성될 좌표
        mouseTarget = camera.ScreenToWorldPoint(Input.mousePosition)-camera.transform.position; //유닛이 생성될 좌표 맵안인지 확인할 좌표

        if (isClickedUB)//유저가 유닛을 생성하기위해 유닛 버튼을 클릭한 상황
        {
            playerBC.canAttack = false;
            unitSpawnSpot.x = (int)unitSpawnSpot.x + 0.5f;
            unitSpawnSpot.y = (int)unitSpawnSpot.y - 0.5f;
          
            //마우스에서 가장 가까운 셀 중앙 좌표
            Ray2D ray = new Ray2D(unitSpawnSpot, Vector2.zero);
            Collider2D[] hit = Physics2D.OverlapBoxAll(ray.origin,Vector2.one,0);
            //충돌되는 콜라이더 있는지 (객체가 있는 좌표인지) 확인하기 위해 현재 좌표에 z축으로 레이 쏨
            bool isBuild = true;
            foreach(Collider2D i in hit){
                if (!i.isTrigger) isBuild = false; //트리러가 아니면
            }
            if (isBuild&&IsInMap(mouseTarget))
            {//충돌 되는게 없고 마우스 좌표가 맵 안일 때, 유닛 만들 수 있을 때 

                hotSpot.x = canBuildUnitCursor[idxOfClickedUB].width / 2;
                hotSpot.y = canBuildUnitCursor[idxOfClickedUB].height / 2;
                Cursor.SetCursor(canBuildUnitCursor[idxOfClickedUB], hotSpot, cursorMode);
                //유닛 생성할 수 있는 커서로 렌더링, 핫스팟은 유저가 클릭했을때 찍힐 좌표

                if (Input.GetMouseButton(0))
                {
                    GenerateUnit(unitSpawnSpot);
                }//유닛 생성
                if (Input.GetMouseButton(1))
                {
                    isClickedUB = false; //clickedUB 상태 false로 바꿈
                    DestroyUnitButtons(); //유닛을 생성한 후에는 유닛 버튼 삭제
                    isClickedTB = false; //유닛 버튼을 삭제한 후에는 towerButton이 다시 눌릴 수 있도록 false
                                      
                }//유저가 유닛생성취
            }

            else
            {//유닛 생성 불가능 한 상태

                hotSpot.x = cantBuildUnitCursor[idxOfClickedUB].width / 2;
                hotSpot.y = cantBuildUnitCursor[idxOfClickedUB].height / 2;
                Cursor.SetCursor(cantBuildUnitCursor[idxOfClickedUB], hotSpot, cursorMode);
                //유닛 생성 불가능 커서a로 렌더링
            }

        } //눌린 unitbutton이 있으면 해당 유닛 생성

        else 
        { 
            if (IsInMap(mouseTarget))
            {
                playerBC.canAttack = true; //마우스 맵안에 있고 유닛 버튼 클릭한 상황 아니라 공격 가능
                hotSpot.x = inMapCursor.width / 2;
                hotSpot.y = inMapCursor.height / 2;
                Cursor.SetCursor(inMapCursor, hotSpot, cursorMode);
                // 마우스 좌표 맵안에 일 때, 커서는 과녁 커서
            }
            else
            {
                playerBC.canAttack = false; //마우스 맵 밖에 있으므로 사용자 공격 못함
                hotSpot.x = 0;
                hotSpot.y = 0;
                Cursor.SetCursor(outMapCursor, hotSpot, cursorMode);
                //마우스 좌표 맵 밖일 때, 커서는 ui 마우스 커서a
            }
        }//유닛 버튼이 클릭이 아닌 상황
        
	}
    bool IsInMap(Vector3 target){
        return xPosMin < target.x && target.x < xPosMax && yPosMin < target.y && target.y < yPosMax;
        //return true;
    }//맵 안에 범위인지 리턴하는 함수

    void GenerateUnit(Vector3 target){
        target.z = 0; //z를 0으로 만들어야함, screenToWorld가 z를 -10으로 만듬
        GameObject cloneUnit = Instantiate(units[idxOfClickedUB], target, Quaternion.identity);
        if (cloneUnit != null)
        {
            isClickedUB = false; //clickedUB 상태 false로 바꿈
            DestroyUnitButtons(); //유닛을 생성한 후에는 유닛 버튼 삭제
            isClickedTB = false; //유닛 버튼을 삭제한 후에는 towerButton이 다시 눌릴 수 있도록 false
                                 //여기서 마우스 모양 제대로
        }
    }//유닛을 생성하는 함수

    void DestroyUnitButtons(){
        Destroy(cloneTriangleImage);
        for (int i = 0; i < cloneUnitButton.Length; i++){
            Destroy(cloneUnitButton[i].gameObject);
        }
    } //유닛 버튼들을 삭제하는 함수

    void GenerateUnitButtons()
    {
        if (isClickedTB)
        {
            DestroyUnitButtons();
            isClickedTB = false;
            return;
        }
        //click이 되있는 상태이면  유닛 버튼 삭제

        isClickedTB = true; //상태 변화
  
        spawnPos.x = towerButton.transform.position.x + towerButton.transform.GetComponent<RectTransform>().rect.width / 2;
        spawnPos.y = towerButton.transform.position.y + towerButton.transform.GetComponent<RectTransform>().rect.height + arrowInterval+arrowImage.GetComponent<RectTransform>().rect.height/2;
        //x,y 계산 법 arrow 이미지 피봇은 정중앙점 

        cloneTriangleImage = Instantiate(arrowImage, spawnPos, Quaternion.identity);
        cloneTriangleImage.transform.SetParent(gameObject.transform, false);

        //Towerbutton 정보값 코드양 줄이려고 캐싱
        spawnPos.x = towerButton.transform.position.x;
        spawnPos.y += (arrowInterval + arrowImage.GetComponent<RectTransform>().rect.height / 2);
        //현재 생성되어야 하는 버튼의 ypos는 전 버튼의 y좌표 + height/2 + 버튼 간격
        //height/2 이여야 하는 이유는 position이 중점이기 때문에a

        for (int i = 0; i < unitButtons.Length; i++)
        {
            cloneUnitButton[i] = Instantiate(unitButtons[i], spawnPos, Quaternion.identity);
            spawnPos.y+= (unitButtons[i].transform.GetComponent<RectTransform>().rect.height+buttonInterval);
            int idx = i;
            cloneUnitButton[i].onClick.AddListener(() => ClickedUB(idx));  //idx에 맞는 유닛 생성
            cloneUnitButton[i].transform.SetParent(gameObject.transform,false);

        }
    }//유닛 버튼을 생성하는 함수

    void ClickedUB(int idx){
        if (isClickedUB)
            return;
        //UnitButton눌린 상태이면 이벤트 막기

        isClickedUB = true; //unit button 눌린상태
        idxOfClickedUB = idx; //눌린 unit button index
    }//유닛을 생성하기 위한 유닛 버튼 클릭 이벤트 함수


}
