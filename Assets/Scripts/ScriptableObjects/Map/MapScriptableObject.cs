using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "MapParametersScriptableObject", menuName = "ScriptableObjects/MapParametersScriptableObject")]
public class MapScriptableObject : ScriptableObject
{
    public Vector2Int MapSize = new Vector2Int(20, 20);
    public int PoissonRadius = 4;
    public float NoiseScale = 0.1f;
    public float HeightThreshold = 0.5f;
    public float OceanThreshold = 0.2f;
}
