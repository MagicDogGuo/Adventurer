using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGridContent : MonoBehaviour {

    [SerializeField]
    [Header("可挖掘的tilemap")]
    public Tilemap CanDigTileMap;

    [SerializeField]
    public Tilemap ObstacleTileMap;

}
