using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour
{

   
    public Stack<ItemObject> slot;
    public Text cntItemText;       // 아이템에 개수
    public int cntfontSize = 30;
    public Sprite DefaultImg; // 기본 이미지

    private Image ItemImg;
    private bool isSlotEmpty = true;     // 현재 슬롯이 비어있으면 true

    public ItemObject ItemReturn() { return slot.Peek(); }
    public bool IsSlotEmpty() { return isSlotEmpty; }
    public void SetSlots(bool isSlot) { this.isSlotEmpty = isSlot; }


    float clickTimer = 0.5f; //더블 클릭 한번 클릭됐을때 시간
    int counter = 0; //두번
    GameManager gameManager;

    // Use this for initialization
    void Awake()
    {
        // 스택 메모리 할당.
        slot = new Stack<ItemObject>();
        cntItemText.fontSize = cntfontSize;    //폰트 사이즈 지정
        ItemImg = transform.GetChild(0).GetComponent<Image>();
        var button = transform.GetComponent<Button>();
        button.onClick.AddListener(buttonListner);
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void buttonListner()
    {
        counter++;
        if (counter == 1)
        {
            Debug.Log("d");
            if(slot.Count!=0)
            {
                ItemObject tmp = slot.Peek();
                if (tmp.Name.Equals("Iron"))
                {
                    gameManager.EquipStoneItem(100);
                    UseItem(tmp);
                }
                else if (tmp.Name.Equals("Ice"))
                {
                    gameManager.EquipIceItem(100);
                    UseItem(tmp);
                }
                else Debug.Log("hi");


            }

        }
    }
   
    public void AddItem(ItemObject item)
    {
        // 스택에 아이템 추가.
        slot.Push(item);

        isSlotEmpty = false;

        UpdateInfo(item.ItemSpr());
    }

    public void UseItem(ItemObject item)
    {
        slot.Pop();
        UpdateInfo(item.ItemSpr());
        counter = 0;
    }

    // 슬롯에 대한 각종 정보 업데이트.
    public void UpdateInfo(Sprite sprite)
    {

        if (slot.Count == 0)
        {
            ItemImg.sprite = DefaultImg;
            slot.Clear();
            isSlotEmpty = true;
            cntItemText.GetComponent<Text>().text = "";
            return;
        }

        ItemImg.sprite = sprite;       // 슬롯안에 들어있는 아이템의 이미지를 셋팅.
        ItemImg.color = new Color(ItemImg.color.r, ItemImg.color.g, ItemImg.color.b, 255f);

        if (slot.Count > 1)
            cntItemText.GetComponent<Text>().text = slot.Count.ToString();
        else cntItemText.GetComponent<Text>().text = "";
    }


}
