using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.XR;

public class DefaultState : IState
{
    private PlayerController _playerController;

    public DefaultState(PlayerController playerController)
    {
        _playerController = playerController;
    }
    public void Enter()
    {
        _playerController.UIManager.DisableUnitUI();
    }

    public void Exit()
    {
        
    }

    public void OnTileClicked(TileScript tile)
    {
        //code for selecting structure
        eStructures structure = tile.GetStructureType();
        if (structure != eStructures.None)
        {
            Debug.Log($"Structure Selected: {structure}");
            _playerController.ChangeState(new StructureSelectedState(_playerController, structure));
        }

        //code for selecting unit
        if (tile.OccupiedUnit == null) { return; }

        Unit unitComponent = tile.OccupiedUnit.GetComponent<Unit>();

        if (unitComponent.Team == _playerController.TurnManager.GetCurrentPlayer())
        {
            // If it's our unit, change to the UnitSelectedState.
            _playerController.ChangeState(new UnitSelectedState(_playerController, unitComponent));
            return;
        }
    }

}
