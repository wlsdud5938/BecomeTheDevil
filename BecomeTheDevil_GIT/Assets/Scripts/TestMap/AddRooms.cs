using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRooms : MonoBehaviour {

	private MapGenerator generator;
    public int path;
    public int nodeX;
    public int nodeY;

    void Start(){

        generator = GameObject.FindGameObjectWithTag("Map").GetComponent<MapGenerator>();
        generator.nodeMap[nodeX, nodeY] = 1;
        generator.rooms.Add(this.gameObject);

    }
}
