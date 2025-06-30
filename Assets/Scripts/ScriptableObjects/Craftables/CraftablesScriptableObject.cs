using UnityEngine;

[CreateAssetMenu(fileName = "CraftablesScriptableObject", menuName = "Scriptable Objects/CraftablesScriptableObject")]
public class CraftablesScriptableObject : ScriptableObject
{
    public int TurnsToCraft;
    public GameObject CraftablePrefab;
}
