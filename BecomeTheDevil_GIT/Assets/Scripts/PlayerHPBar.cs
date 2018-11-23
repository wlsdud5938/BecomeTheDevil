using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour {
    public Slider hpSlider;

    Statu playerStatu;
	// Use this for initialization
	void Start () {
        playerStatu = GameObject.FindWithTag("Player").GetComponent<Statu>();
        hpSlider.maxValue = playerStatu.maxHP;

	}
	
	// Update is called once per frame
	void Update () {
        hpSlider.value = playerStatu.currentHP;
	}
}
