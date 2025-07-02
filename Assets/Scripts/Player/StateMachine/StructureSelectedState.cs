using UnityEngine;

public class StructureSelectedState : IState
{

    PlayerController _playerController;
    eStructures _selectedStructure;
    GameObject _selectedStructureInstance;

    public StructureSelectedState(PlayerController playerController, eStructures selectedStructure, GameObject selectedStructurePrefab)
    {
        _playerController = playerController;
        _selectedStructure = selectedStructure;
        _selectedStructureInstance = selectedStructurePrefab;
    }

    public void Enter()
    {
        _playerController.UIManager.UpdateStructureUI(_selectedStructure);
    }

    public void Exit()
    {
        _playerController.UIManager.DisableStructureUI();
    }

    public void OnBuildUnitRequested(CraftablesScriptableObject craftable)
    {
        City cityComponent = _selectedStructureInstance.GetComponent<City>();

        //if selected structure is not a city cannot build units
        if (cityComponent == null){ return; }

        cityComponent.AddToBuildQueue(craftable);
    }

    public void OnTileClicked(TileScript tile)
    {
        //dont do anything if hovering over a UI element
        if (_playerController.IsPointerOverUI)
            return;
        _playerController.ChangeState(new DefaultState(_playerController));
    }
}
