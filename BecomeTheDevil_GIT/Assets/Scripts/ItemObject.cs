using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour {

    public enum TYPE { Coin, Iron, Ice }

    public TYPE type;           // 아이템의 타입
    public string Name;         // 아이템 이름

    private Inventory iv;

    private Sprite itemSprite;

    public Sprite ItemSpr() { return itemSprite; }

    // Use this for initialization
    void Awake () {
        iv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

        itemSprite = transform.GetComponent<SpriteRenderer>().sprite;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void AddItem()
    {
        if (iv.AddItemInSlot(this))
            gameObject.SetActive(false); // 필드의 아이템을 비활성화
        else
            Debug.Log("아이템을 습득하지 못했습니다.");
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            AddItem();
    }

    public void Init(string Name)
    {
        // 이름에 따라 타입을 결정.
        switch (Name)
        {
            case "Coin": type = TYPE.Coin; break;
            case "Iron": type = TYPE.Iron; break;
            case "Ice": type = TYPE.Ice; break;
        }

        this.Name = Name;           // 이름을 초기화.

    }

}
