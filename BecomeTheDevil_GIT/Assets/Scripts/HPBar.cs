using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Slider HPSlider;
    private Statu stat;

    // Use this for initialization
    void Start () {
        stat = gameObject.GetComponent<Statu>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateHPBar();
	}

    void UpdateHPBar()
    {
      // HPSlider.maxValue = stat.HP;
      //  HPSlider.value = stat.currentHP;
        
        // statu의 값을 받아와 HP Silder의 값을 변경한다.
    }
}
