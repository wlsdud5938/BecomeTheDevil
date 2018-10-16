using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Potal : MonoBehaviour
{
    private Player player;
    float timer = 0.0f;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        timer = 0.0f;
        if(other.gameObject.tag == "Player" && player.haveKey==true)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        timer+=Time.deltaTime;
        if (other.gameObject.tag == "Player" && player.haveKey == true && timer >= 5.0f)
            SceneManager.LoadScene("Win");
    }

}