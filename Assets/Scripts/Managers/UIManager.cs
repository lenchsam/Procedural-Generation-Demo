using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Unit Specific UI Elements")]
    [SerializeField] private GameObject _foundCityButton;

    [Header("Structure Specific UI Elements")]
    [SerializeField] private GameObject _cityButton;
    [SerializeField] private GameObject _UICraftingQueue;

    private void Start()
    {
        DisableUnitUI();
        DisableStructureUI();
    }
    public void UpdateUnitUI(e_UnitType unitType)
    {
        //disable all unit-specific buttons
        DisableUnitUI();

        //determine which buttons to enable based on the unit type
        switch (unitType)
        {
            case e_UnitType.Settler:
                _foundCityButton.SetActive(true);
                break;
            case e_UnitType.Warrior:
                
                break;
            case e_UnitType.Archer:
                
                break;
        }
    }
    public void UpdateStructureUI(eStructures structure)
    {
        switch (structure)
        {
            case eStructures.None:
                
                break;
            case eStructures.City:
                _cityButton.SetActive(true);
                _UICraftingQueue.SetActive(true);
                break;
        }
    }

    public void DisableUnitUI()
    {
        _foundCityButton.SetActive(false);
    }
    public void DisableStructureUI()
    {
        _cityButton.SetActive(false);
        _UICraftingQueue.SetActive(false);
    }

    public void AddToCraftingQueue(e_UnitType unitType)
    {
        GameObject toAdd = null;
        switch (unitType)
        {
            case e_UnitType.Settler:
                Debug.Log("Settler");
                toAdd = Resources.Load<GameObject>("Presets/UI/UI_Settler");
                break;
            case e_UnitType.Warrior:
                Debug.Log("warrior");
                toAdd = Resources.Load<GameObject>("Presets/UI/UI_Warrior");
                break;
            case e_UnitType.Archer:
                Debug.Log("archer");
                toAdd = Resources.Load<GameObject>("Presets/UI/UI_Archer");
                break;
            default:
                break;
        }

        GameObject instantiatedObject = Instantiate(toAdd);
        instantiatedObject.transform.SetParent(_UICraftingQueue.transform.GetChild(0), false);
    }
}
