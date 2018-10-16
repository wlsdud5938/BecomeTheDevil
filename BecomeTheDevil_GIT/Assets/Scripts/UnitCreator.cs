using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreator : MonoBehaviour {
    private Player player;
    public float setTime = 2.0f;
    public float timer;
    public Vector3 playerPosition;
    public GameObject unit;
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update()
    {
        timer = setTime;
        if (null != GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            playerPosition = player.gameObject.transform.position;
            timer += Time.deltaTime;
        }
    
    }

    public void OnClick()
    {
        if(timer >= setTime)
        {
            Instantiate(unit, player.transform.position, Quaternion.identity);
            timer = 0.0f;
        }
    }
}
