using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
        //Debug.Log(GridPosition.X + ", " + GridPosition.Y);
        if (Input.GetMouseButtonDown(0))
        {
            PlaceTower();
        }
    }

    private void PlaceTower()
    {
        //Debug.Log("Place Tower");
        //Instantiate(GameManager.Instance.TowerPrefab, transform.position, Quaternion.identity);
    }
}
