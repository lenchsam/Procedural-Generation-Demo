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

    public void OnBuildUnitRequested(CraftablesScriptableObject craftable)
    {
        //not needed for this state
    }

    public void OnTileClicked(TileScript tile)
    {

        //code for selecting unit
        Unit unitComponent = tile.OccupiedUnit?.GetComponent<Unit>();

        if (tile.OccupiedUnit != null) {
            if (unitComponent.Team == _playerController.TurnManager.GetCurrentPlayer())
            {
                // If it's our unit, change to the UnitSelectedState.
                _playerController.ChangeState(new UnitSelectedState(_playerController, unitComponent));
                return;
            }
        }

        //code for selecting structure
        eStructures structure = tile.GetStructureType();
        
        if (structure != eStructures.None && tile.OccupiedBuilding != null)
        {
            City cityComponent = tile.OccupiedBuilding.GetComponent<City>();
            if (cityComponent != null && cityComponent.Team != _playerController.TurnManager.GetCurrentPlayer())
            {
                //city not owned by current player
                return;
            }
            _playerController.ChangeState(new StructureSelectedState(_playerController, structure, tile.OccupiedBuilding));
        }
    }

}
