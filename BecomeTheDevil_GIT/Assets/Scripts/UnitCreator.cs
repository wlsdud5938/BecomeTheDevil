using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreator : MonoBehaviour {
    private Player player;
    private float timer = 10.0f;
    public Vector3 playerPosition;
    public GameObject unit;
    public bool a = true;
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update()
    {
        if (null != GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            playerPosition = player.gameObject.transform.position;
            timer += Time.deltaTime;
        }
    }

    public void OnClick()
    {
        if(timer >= 10.0f)
        {
            Instantiate(unit, player.transform.position, Quaternion.identity);
        }
    }

}
