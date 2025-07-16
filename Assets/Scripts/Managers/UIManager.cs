using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Unit Specific UI Elements")]
    [SerializeField] private GameObject _foundCityButton;

    [Header("Structure Specific UI Elements")]
    [SerializeField] private GameObject _cityButton;
    [SerializeField] private GameObject _UICraftingGameObject;
    [SerializeField] private TMP_Text _UITurnsUntilCrafted;

    private Queue<GameObject> _UICraftingQueue = new Queue<GameObject>();

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
                _UICraftingGameObject.SetActive(true);
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
        _UICraftingGameObject.SetActive(false);
    }

    public void AddToCraftingQueue(e_UnitType unitType)
    {
        GameObject toAdd = null;
        switch (unitType)
        {
            case e_UnitType.Settler:
                toAdd = Resources.Load<GameObject>("Presets/UI/UI_Settler");
                break;
            case e_UnitType.Warrior:
                toAdd = Resources.Load<GameObject>("Presets/UI/UI_Warrior");
                break;
            case e_UnitType.Archer:
                toAdd = Resources.Load<GameObject>("Presets/UI/UI_Archer");
                break;
            default:
                break;
        }

        GameObject instantiatedObject = Instantiate(toAdd);
        instantiatedObject.transform.SetParent(_UICraftingGameObject.transform.GetChild(0), false);

        _UICraftingQueue.Enqueue(instantiatedObject);
    }

    public void UpdateCraftingTurnsText(int turnsUntilCrafted)
    {
        _UITurnsUntilCrafted.text = "Turns Until Unit Crafted: " + turnsUntilCrafted.ToString();
    }
    internal void RemoveFromCraftingQueue()
    {
        GameObject craftedUnitUI = _UICraftingQueue.Dequeue();
        Destroy(craftedUnitUI);
    }
}
