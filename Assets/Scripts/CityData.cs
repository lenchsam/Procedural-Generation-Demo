using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CityData
{
    public e_Team Team;
    public Vector2Int intCords;
    public List<Vector2Int> Tiles;
    private HexGrid _hexGrid;
    CityData()
    {
        _hexGrid = GameObject.FindAnyObjectByType<HexGrid>();
        List<GameObject> surroundingTiles = _hexGrid.GetSurroundingTiles(_hexGrid.GetTileFromIntCords(intCords));

        foreach (GameObject tile in surroundingTiles)
        {
            TileScript tileScript = tile.GetComponent<TileScript>();
            Tiles.Add(tileScript.IntCoords);
        }
    }
}
