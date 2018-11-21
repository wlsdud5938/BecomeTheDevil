using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour {

    //타워 버튼 클릭하면 유닛 버튼 생서
    public Button towerButton;   // 타워 버튼
    public Button[] unitButtons; // 유닛 버튼
    public GameObject[] units;  //생성되어야 하는 unit 프리팹 어레이
    public float buttonInterval = 1f; // 버튼 간격

    //마우스 바꿔야함
    public Texture2D cursorTexture; 
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    Camera camera; //메인카메라
    GameManager gameManager; 



    bool isClickedTB = false; //towerButton 눌린 상태냐고
    bool isClickedUB = false; //unitButton 눌린 상태인지
    int idxOfClickedUB; //생성할 유닛에 인덱스

    Button[] cloneUnitButton; //생성한 유닛 버튼 그 자체
    Button cloneTowerButton; //생성한 타워 버튼 

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    // Use this for initialization
    void Start () {

        cloneUnitButton = new Button[unitButtons.Length]; 
        camera = Camera.main; //메인 카메라 캐싱, 마우스 좌표를 메인카메라 기준 좌표로 변환하기 위해

        //타워버튼 생성
        cloneTowerButton = Instantiate(towerButton, new Vector3(towerButton.transform.position.x,towerButton.transform.position.y,0), Quaternion.identity); 
        cloneTowerButton.onClick.AddListener(()=>GenerateUnitButtons()); //click 이벤트 연결
        cloneTowerButton.transform.parent = gameObject.transform;  // canvas 밑에 두기 버튼 같은 경우 캔버스 안에 있어야 보임
        cloneTowerButton.transform.localScale = Vector3.one;  //패런트를 바꾸면 스캐일이 바뀜

        gameManager = GameManager. Instance; //게임 매니저 캐싱

    }
	// Update is called once per frame
	void Update () {
        if (isClickedUB)//유저가 유닛을 생성하기위해 유닛 버튼을 클릭한 상황
        {
            //1: 여기서 마우스 커서 모양 바꾸고
            if (Input.GetMouseButtonDown(0))  //유저가 유닛 버튼을 클릭하고, 생성하려는 좌표를 클릭한 상황
            {
                Vector3 target = camera.ScreenToWorldPoint(Input.mousePosition);  //마우스좌표 메인카메라 기준 좌표로 변환
                Ray2D ray = new Ray2D(target, Vector2.zero);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit.collider.isTrigger && hit.collider.CompareTag("Map")) // 부딪히는 콜라이더 없을 때, 즉 닿는 물체 없는 곳에서 생성 가능
                {
                    GenerateUnit(target);
                }

                else Debug.Log(hit.collider); //여기서 생성 못한다고 알려줌

            }

        } //눌린 unitbutton이 있으면 해당 유닛 생성
        
	}

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

        //Towerbutton 정보값 코드양 줄이려고 캐싱
        float TBXpos = cloneTowerButton.transform.position.x, TBYpos = cloneTowerButton.transform.position.y;
        float currButYpos = TBYpos + cloneTowerButton.transform.GetComponent<RectTransform>().rect.height / 2;
        //현재 생성되어야 하는 버튼의 ypos는 전 버튼의 y좌표 + height/2 + 버튼 간격
        //height/2 이여야 하는 이유는 position이 중점이기 때문에a

        for (int i = 0; i < unitButtons.Length; i++)
        {
            cloneUnitButton[i] = Instantiate(unitButtons[i], new Vector3(TBXpos, currButYpos + buttonInterval, 0), Quaternion.identity);
            currButYpos = cloneUnitButton[i].transform.position.y + cloneUnitButton[i].transform.GetComponent<RectTransform>().rect.height / 2;
            int idx = i;
            cloneUnitButton[i].onClick.AddListener(() => ClickedUB(idx));  //idx에 맞는 유닛 생성
            cloneUnitButton[i].transform.parent = gameObject.transform;  // canvas 밑에 두기 버튼 같은 경우 캔버스 안에 있어야 보임
            cloneUnitButton[i].transform.localScale = Vector3.one;

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
