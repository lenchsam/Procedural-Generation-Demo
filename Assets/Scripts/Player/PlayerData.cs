using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public e_Team Player;
    public List<Vector2Int> RevealedTiles;
    public Vector3 SavedCameraPosition;
    public List<Unit> Units;

    public PlayerData(e_Team player, Vector3 initialCameraPos)
    {
        Player = player;
        RevealedTiles = new List<Vector2Int>();
        Units = new List<Unit>();
        SavedCameraPosition = initialCameraPos;
    }
}
