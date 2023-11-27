using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapTest : MonoBehaviour {

    public Tile tilelight;

    [SerializeField]
    Tilemap map;

    //[SerializeField]
    //Vector3 disappearPos;

    [SerializeField]
    Transform RolePos;

    //Tilemap tilemap;
	void Start () {
    
        var tilePos = map.WorldToCell(RolePos.position);//格子內的世界座標都換算成格子座標

        map = GetComponent<Tilemap>();
        map.SetTile(tilePos, null);
        
    }

    void Update () {

    }
}
