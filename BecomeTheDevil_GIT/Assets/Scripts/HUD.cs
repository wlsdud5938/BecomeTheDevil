using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    public Text timerText;
    public float maxHealth;
    public float playerHealth;
    public Slider healthSlider;
    private RoomTemplates templates;

    float hp;
    float timer = 0;

	// Use this for initialization
	void Start () {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

    }

    // Update is called once per frame
    void Update () {
        UpdateTimer();
        UpdatePlayerHealth();
	}

    void UpdatePlayerHealth()
    {
        hp = (playerHealth / maxHealth) * 100;
        healthSlider.value = hp;
    }


    void UpdateTimer()
    {
        if (templates.spawnedBoss == true)
        {
            timer += Time.deltaTime;
            timerText.text = "TIMER : " + timer.ToString("0.0");
        }
    }
}
