using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Potal : MonoBehaviour
{
    private Player player;
    RoomTemplates temp;
    float timer = 0.0f;
    bool find = false;
    // Use this for initialization
    void Start()
    {
        temp = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }

    // Update is called once per frame
    void Update()
    {
        if (temp.spawnedBoss && !find)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            find = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        timer = 0.0f;
        if(other.gameObject.tag == "Player" && player.haveKey==true)
        {
            gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<Animator>().SetTrigger("GateOpen");
            Debug.Log("!");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        timer+=Time.deltaTime;
        if (other.gameObject.tag == "Player" && player.haveKey == true && timer >= 5.0f)
            SceneManager.LoadScene("Win");
    }

}