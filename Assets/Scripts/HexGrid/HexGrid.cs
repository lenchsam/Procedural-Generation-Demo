using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
//https://www.redblobgames.com/grids/hexagons USEFUL RESOURCE FOR ALL THINGS HEX GRID
public class HexGrid : MonoBehaviour
{
    
    [BoxGroup("Assignables")]
    [SerializeField] GameObject _tilesParent;
    [BoxGroup("Assignables")]
    [SerializeField] GameObject _fogOfWarPrefab;
    [BoxGroup("Map Settings")]
    public int MapWidth;
    [BoxGroup("Map Settings")]
    public int MapHeight;
    [BoxGroup("Map Settings")]
    public bool ShowFOW;
    [BoxGroup("Map Settings")]
    int _tileSize = 1; 
    [SerializeField] Dictionary<GameObject, TileScript> _tiles = new Dictionary<GameObject, TileScript>();

    ProceduralGeneration _proceduralGeneration;
    
    Vector2 _seedOffset;  // Random offset for noise generation

    private async void Start()
    {
        _proceduralGeneration = FindAnyObjectByType<ProceduralGeneration>();
        await _proceduralGeneration.MakeMapGrid(MapWidth, MapHeight, _tiles, _tileSize);
    }
    public TileScript GetTileScriptFromPosition(Vector2 cords){
        foreach(KeyValuePair<GameObject, TileScript> TS in _tiles){
            if(new Vector2(TS.Key.transform.position.x, TS.Key.transform.position.z) == cords){
                return TS.Value;
            }
        }
        return null;
    }
    public TileScript GetTileScriptFromIntCords(Vector2Int cords){
        foreach(KeyValuePair<GameObject, TileScript> TS in _tiles){
            if(TS.Value.IntCoords == cords){
                return TS.Value;
            }
        }
        return null;
    }
    public void BlockTile(Vector2 coords){
        var tileScript = GetTileScriptFromPosition(coords);
        tileScript.IsWalkable = false;
    }
    public void UnblockTile(Vector2 coords){
        var tileScript = GetTileScriptFromPosition(coords);
        tileScript.IsWalkable = true;
    }
    public GameObject GetTileFromPosition(Vector2 cords){
        foreach(KeyValuePair<GameObject, TileScript> TS in _tiles){
            if(new Vector2(TS.Key.transform.position.x, TS.Key.transform.position.z) == cords){
                return TS.Key;
            }
        }
        return null;;
    }
    public GameObject GetTileFromIntCords(Vector2Int cords) {
        foreach(KeyValuePair<GameObject, TileScript> TS in _tiles){
            if(TS.Value.IntCoords == cords){
                return TS.Key;
            }
        }
        return null;
    }
    public Vector2 GetCoordinatesFromPosition(Vector3 position){
        //Debug.Log(new Vector2(position.x, position.z));
        return new Vector2(position.x, position.z);
    }
    public List<GameObject> GetSurroundingTiles(GameObject tileGO){

        List<GameObject> connecting = new List<GameObject>();

        //arrays of directions needed to be checked for tiles
        //need two for checking both odd and even rows
        Vector2Int[] directionsOdd = {
            new Vector2Int(0, -1),
            new Vector2Int(0, +1),
            new Vector2Int(-1, +1),
            new Vector2Int(-1, 0),
            new Vector2Int(+1, +1),
            new Vector2Int(+1, 0)
        };
        Vector2Int[] directionsEven = {
            new Vector2Int(0, -1),
            new Vector2Int(0, +1),
            new Vector2Int(-1, -1),
            new Vector2Int(-1, 0),
            new Vector2Int(+1, -1),
            new Vector2Int(+1, 0)
        };
        
        Vector2Int tileCords = tileGO.GetComponent<TileScript>().IntCoords;

        connecting.Add(GetTileFromIntCords(tileCords));

        if(tileCords.x % 2 != 0){//if the tile is on an odd row
            foreach(Vector2 dir in directionsOdd){
                GameObject tile = GetTileFromIntCords(new Vector2Int((int)(tileCords.x + dir.x), (int)(tileCords.y + dir.y)));
                if(tile){connecting.Add(tile);} //Top
            } 
        }else{//if the tile is on an even row
            //check each possible tile surrounding it to see if its there, if it is there add it to the list
            foreach(Vector2 dir in directionsEven){
                GameObject tile = GetTileFromIntCords(new Vector2Int((int)(tileCords.x + dir.x), (int)(tileCords.y + dir.y)));
                if(tile){connecting.Add(tile);} //Top
            } 
        }

        return connecting;
    }
    public int DistanceBetweenTiles(Vector2Int startCoords, Vector2Int targetCoords) {
        // Convert offset coordinates to cube coordinates
        Vector3Int startCube = OffsetToCube(startCoords);
        Vector3Int targetCube = OffsetToCube(targetCoords);

        // Calculate distance using cube coordinates
        int distance = (Mathf.Abs(startCube.x - targetCube.x) + Mathf.Abs(startCube.y - targetCube.y) + Mathf.Abs(startCube.z - targetCube.z)) / 2;

        //Debug.Log(distance + " THIS IS THE DISTANCE");

        return distance;
    }
    //only needed for DistanceBetweenTiles function
    private Vector3Int OffsetToCube(Vector2Int offsetCoords) {
        int col = offsetCoords.x;
        int row = offsetCoords.y;

        int x = col;
        int z = row - (col - (col & 1)) / 2; // handles the shift in ofset tiles

        //y is the negative of x and z. cube coordinated needs this
        int y = -x - z;

        return new Vector3Int(x, y, z);
    }
    public Vector2Int GetIntCordsFromPosition(Vector2 pos){
        TileScript TS = GetTileScriptFromPosition(pos);
        return TS.IntCoords;
    }
    public void AddFogOfWar(TileScript tile){
        GameObject fow = Instantiate(_fogOfWarPrefab, transform);
        fow.name = "FOW " + tile.gameObject.name;
        fow.transform.position = new Vector3(tile.transform.position.x, 0, tile.transform.position.z);
        fow.GetComponent<TileScript>().IntCoords = tile.IntCoords;
        tile.Fow = fow;
        tile.gameObject.layer = LayerMask.NameToLayer("Hidden");
    }
    public void RevealTile(TileScript tile){
        tile.Reveal();
        foreach(GameObject neighbour in GetSurroundingTiles(tile.gameObject)){
            neighbour.GetComponent<TileScript>().Reveal();
        }
    }
}
public enum eOccupiedBy{
    None,
    wall,
    farm,
    barracks
}
public enum eTileType{
    Ocean,
    Grass,
    Coast,
    Mountain
}
