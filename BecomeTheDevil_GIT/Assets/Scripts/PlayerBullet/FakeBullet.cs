using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBullet : MonoBehaviour {

    private BulletController parent;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () { 
        /*
        if (parent.mouseButtonUp)   // 차지 후 발사할 때 fakeBullet 없앰.
        {
            BattleManager.Instance.Pool.ReleaseObject(gameObject);
        }*/
	}

    public void Initialize(BulletController parent)
    {
        this.parent = parent;
    }
}
