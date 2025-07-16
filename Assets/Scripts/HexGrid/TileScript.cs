using System;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public bool IsWalkable;
    public Vector2Int IntCoords;
    public GameObject OccupiedUnit;
    public GameObject OccupiedBuilding;
    public eTileType TileType;
    public eBiomes Biome;
    public int MovementCost = 1;

    [SerializeField] private eStructures _occupiedStructure = eStructures.None;

    public GameObject Fow;

    private TurnManager _turnManager;

    private void Awake()
    {
        _turnManager = FindAnyObjectByType<TurnManager>();
    }

    public void Constructor(bool isWalkable, Vector2Int intCords, eTileType tileType, eBiomes biome)
    {
        IsWalkable = isWalkable;
        IntCoords = intCords;
        TileType = tileType;
        Biome = biome;
        if (TileType == eTileType.Ocean)
        {
            IsWalkable = false;
        }
    }

    public void Highlight()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void Unhighlight()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    public void SetStructure(eStructures eStructure, GameObject structure)
    {
        _occupiedStructure = eStructure;
        OccupiedBuilding = structure;
    }
    public eStructures GetStructureType()
    {
        return _occupiedStructure;
    }

    public void Reveal()
    {
        gameObject.layer = LayerMask.NameToLayer("Tile");
        if(OccupiedBuilding) 
            OccupiedBuilding.layer = LayerMask.NameToLayer("Tile");

        _turnManager.GetCurrentPlayerData().RevealedTiles.Add(IntCoords);

        Fow.gameObject.SetActive(false);
        if (OccupiedUnit) { OccupiedUnit.SetActive(true); }
    }

    public void ReBlock()
    {
        gameObject.layer = LayerMask.NameToLayer("Hidden");
        if (OccupiedBuilding)
            OccupiedBuilding.layer = LayerMask.NameToLayer("Hidden");

        Fow.gameObject.SetActive(true);
        if (OccupiedUnit) { OccupiedUnit.SetActive(false); }
    }
}
