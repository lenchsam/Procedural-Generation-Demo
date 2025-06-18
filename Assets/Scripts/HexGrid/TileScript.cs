using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public bool IsWalkable;
    public Vector2Int IntCoords;
    public GameObject OccupiedUnit;
    public eTileType TileType;
    public eBiomes Biome;
    public int MovementCost = 1;

    public void Constructor(bool isWalkable, Vector2Int intCords, eTileType tileType, eBiomes biome){
        IsWalkable = isWalkable;
        IntCoords = intCords;
        TileType = tileType;
        Biome = biome;
        if(TileType == eTileType.Ocean){
            IsWalkable = false;
        }
    }
}
