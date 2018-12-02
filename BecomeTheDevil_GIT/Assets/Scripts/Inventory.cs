using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<GameObject> AllSlot; //슬롯을 관리해줄 리스트
    public RectTransform InvenRect;
    public GameObject OriginSlot;

    public GameObject SlotParent;

    public float slotSize = 130f;              // 슬롯의 사이즈.
    public float slotGapX = 24f;               // 슬롯간 가로 간격.
    public float slotGapY = 42f;              //슬롯간 세로 간격.
    public float slotCountX = 6;            // 슬롯의 가로 개수.
    public float slotCountY = 3;            // 슬롯의 세로 개수.

    private float InvenWidth;           // 인벤토리 가로길이.
    private float InvenHeight;          // 인밴토리 세로길이.
    private float EmptySlot;            // 빈 슬롯의 개수.

    public List<GameObject> GetAllSlot() { return AllSlot; }


    // Use this for initialization
    void Awake () {

        // 인벤토리 이미지의 가로, 세로 사이즈 셋팅.
        InvenWidth = (slotCountX * slotSize) + (slotCountX * slotGapX) + slotGapX;
        InvenHeight = (slotCountY * slotSize) + (slotCountY * slotGapY) + slotGapY;

        // 셋팅된 사이즈로 크기를 설정.
        InvenRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, InvenWidth); // 가로.
        InvenRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, InvenHeight);  // 세로.

        // 슬롯 생성하기.
        for (int y = 0; y < slotCountY; y++)
        {
            for (int x = 0; x < slotCountX; x++)
            {
                // 슬롯을 복사한다.
                GameObject slot = Instantiate(OriginSlot) as GameObject;
                // 슬롯의 RectTransform을 가져온다.
                RectTransform slotRect = slot.GetComponent<RectTransform>();
                // 슬롯의 자식인 투명이미지의 RectTransform을 가져온다.
                RectTransform item = slot.transform.GetChild(0).GetComponent<RectTransform>();

                slot.name = "slot_" + y + "_" + x; // 슬롯 이름 설정.
                slot.transform.parent = SlotParent.transform; // 슬롯의 부모를 설정

                // 슬롯이 생성될 위치 설정하기.
                slotRect.localPosition = new Vector3((slotSize * x) + (slotGapX * (x + 1)) + 482,
                                                   -((slotSize * y) + (slotGapY * (y + 1)) + 223),
                                                      0);

                // 슬롯의 자식인 투명이미지의 사이즈 설정하기.
                slotRect.localScale = Vector3.one;
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize); // 가로
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);   // 세로.

                // 슬롯의 사이즈 설정하기.
                item.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize - slotSize * 0.3f); // 가로.
                item.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize - slotSize * 0.3f);   // 세로.

                // 리스트에 슬롯을 추가.
                AllSlot.Add(slot);
            }
        }

        // 빈 슬롯 = 슬롯의 숫자.
        EmptySlot = AllSlot.Count;

        Invoke("Init", 0.1f);

    }
	
	// Update is called once per frame
	void Update () {

    }

    public bool AddItemInSlot(ItemObject item)
    {
        if (item.Name.Equals("Coin"))
            return true;
        // 슬롯에 총 개수.
        int slotCount = AllSlot.Count;

        // 넣기위한 아이템이 슬롯에 존재하는지 검사.
        for (int i = 0; i < slotCount; i++)
        {
            // 그 슬롯의 스크립트를 가져온다.
            InvenSlot slot = AllSlot[i].GetComponent<InvenSlot>();

            // 슬롯이 비어있으면 통과.
            if (slot.IsSlotEmpty())
                continue;

            //슬롯에 원하는 아이템이 있을 경우 아이템을 넣는다.
            if (slot.ItemReturn().type == item.type)
            {
                // 슬롯에 아이템을 넣는다.
                slot.AddItem(item);
                return true;
            }
        }

        // 빈 슬롯에 아이템을 넣기위한 검사.
        for (int i = 0; i < slotCount; i++)
        {
            InvenSlot slot = AllSlot[i].GetComponent<InvenSlot>();

            // 슬롯이 비어있지 않으면 통과
            if (!slot.IsSlotEmpty())
                continue;

            slot.AddItem(item);
            return true;
        }

        return false;

    }

}
