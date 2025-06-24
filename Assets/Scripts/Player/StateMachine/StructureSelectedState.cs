using UnityEngine;

public class StructureSelectedState : IState
{

    PlayerController _playerController;
    eStructures _selectedStructure;

    public StructureSelectedState(PlayerController playerController, eStructures selectedStructure)
    {
        _playerController = playerController;
        _selectedStructure = selectedStructure;
    }

    public void Enter()
    {
        Debug.Log($"Structure Selected: {_selectedStructure}");
        _playerController.UIManager.UpdateStructureUI(_selectedStructure);
    }

    public void Exit()
    {
        _playerController.UIManager.DisableStructureUI();
    }

    public void OnTileClicked(TileScript tile)
    {
        _playerController.ChangeState(new DefaultState(_playerController));
    }
}
