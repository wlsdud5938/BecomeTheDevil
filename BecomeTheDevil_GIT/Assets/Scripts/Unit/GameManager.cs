using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element { MeleeSlime, RangeSlime, NONE }

public class GameManager : Singleton<GameManager> {
    

    public ObjectPool Pool { get; private set; }
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
