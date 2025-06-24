using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Unit Specific UI Elements")]
    public GameObject foundCityButton;

    private void Start()
    {
        DisableUnitUI();
    }
    public void UpdateUnitUI(e_UnitType unitType)
    {
        //disable all unit-specific buttons
        DisableUnitUI();

        //determine which buttons to enable based on the unit type
        switch (unitType)
        {
            case e_UnitType.Settler:
                foundCityButton.SetActive(true);
                break;
            case e_UnitType.Warrior:
                
                break;
            case e_UnitType.Archer:
                
                break;
        }
    }

    public void DisableUnitUI()
    {
        foundCityButton.SetActive(false);
    }
}
