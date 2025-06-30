using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Objects/Player")]
public class PlayerScriptableObject : ScriptableObject
{
    public e_Team Team;
    public List<Vector2Int> RevealedTiles = new List<Vector2Int>();
    public List<GameObject> Units = new List<GameObject>();
}
