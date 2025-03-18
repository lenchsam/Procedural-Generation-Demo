using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapMenuSelect : MonoBehaviour
{
    [SerializeField] MapScriptableObject _mapScriptableObject;
    [SerializeField] TMP_Dropdown _mapSizeDropdown;
    public void AssignParameters(){
        switch(_mapSizeDropdown.value){
        case 0:
            _mapScriptableObject.MapSize = new Vector2Int(20, 20);
            _mapScriptableObject.PoissonRadius = 4;
            break;
        case 1:
            _mapScriptableObject.MapSize = new Vector2Int(30, 30);
            _mapScriptableObject.PoissonRadius = 5;
            break;
        case 2:
            _mapScriptableObject.MapSize = new Vector2Int(40, 40);
            _mapScriptableObject.PoissonRadius = 6;
            break;
        case 3:
            _mapScriptableObject.MapSize = new Vector2Int(50, 50);
            _mapScriptableObject.PoissonRadius = 8;
            break;
        }
    }
}
