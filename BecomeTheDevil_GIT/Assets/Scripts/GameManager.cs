using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    
    

    private int health = 15;
    public GameObject[] enemys;
    public GameObject enemy;
    private float timer = 0.0f;
    public GameObject entryRoom;
    // Use this for initialization
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 5.0f)
        {
            timer = 0.0f;
            Instantiate(enemy, entryRoom.transform.GetChild(0).transform.position, Quaternion.identity);

        }
    }
}