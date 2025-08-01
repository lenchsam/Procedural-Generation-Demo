using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    [SerializeField] GameObject _fogOfWarPrefab;
    public bool ShowFOW;
    [SerializeField] HexGrid _hexGrid;

    public void AddFogOfWarTile(TileScript tile)
    {
        GameObject fow = Instantiate(_fogOfWarPrefab, transform);
        fow.name = "FOW " + tile.gameObject.name;
        fow.transform.position = new Vector3(tile.transform.position.x, 0, tile.transform.position.z);
        fow.GetComponent<TileScript>().IntCoords = tile.IntCoords;
        tile.Fow = fow;
        tile.gameObject.layer = LayerMask.NameToLayer("Hidden");
    }
    public void RevealTile(TileScript tile)
    {
        if(!ShowFOW) return;
        tile.Reveal();
        foreach (GameObject neighbour in _hexGrid.GetSurroundingTiles(tile.gameObject))
        {
            neighbour.GetComponent<TileScript>().Reveal();
        }
    }
}
