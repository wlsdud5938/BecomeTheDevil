using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element { MeleeSlime, RangeSlime, NONE }

public class GameManager : Singleton<GameManager>
{


    public ObjectPool Pool { get; private set; }

    private int health = 15;


    // Use this for initialization
    void Awake()
    {
        Pool = GetComponent<ObjectPool>();      // 이것 땜에 고생했다;;;
    }

    // Update is called once per frame
    void Update()
    {

    }
}