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
        int round = gameManager.idxOfWave;
        if (gameManager.currNumOfEnemyes<=0)
        {
            float nextTime = gameManager.nextWaveTime - gameManager.enemySpawnTimer;

            string s = nextTime.ToString("0.0");

            waveInfo.text =  " 다음 웨이브 : "+(round+1).ToString()+"\n"+ " NEXT WAVE TIMER:\n " + s;
        }//적을 만드는 도중이 아니고 적을 모두 다 잡은 시간일
        else
        {
            waveInfo.text = " 현재 웨이브 : " + round.ToString()+"\n"+" NUM OF ENEMY :\n " + gameManager.currNumOfEnemyes.ToString();
        }
	}
}
