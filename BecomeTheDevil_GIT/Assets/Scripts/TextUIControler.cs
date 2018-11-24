using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextUIControler : MonoBehaviour {
    public Text waveInfo;

    GameManager gameManager;
	// Use this for initialization
	void Start () {
        gameManager = GameManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameManager.currNumOfEnemyes == 0)
        {
            float nextTime = gameManager.nextWaveTime - gameManager.enemySpawnTimer;

            string s = nextTime.ToString("0.0");

            waveInfo.text =  "NEXT WAVE :\n" + s;
        }
        else
        {
            waveInfo.text = "NUM OF ENEMY :\n" + gameManager.currNumOfEnemyes.ToString();
        }
	}
}
