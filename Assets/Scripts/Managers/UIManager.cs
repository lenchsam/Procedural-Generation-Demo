using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Unit Specific UI Elements")]
    [SerializeField] private GameObject _foundCityButton;

    [Header("Structure Specific UI Elements")]
    [SerializeField] private GameObject _cityButton;

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
    }
}
