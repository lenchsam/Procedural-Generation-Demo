using UnityEngine;

public class UnitSelectedState : IState
{
    PlayerController _playerController;
    Unit _selectedUnitComponent;
    public UnitSelectedState(PlayerController playerController, Unit selectedUnit)
    {
        _playerController = playerController;
        _selectedUnitComponent = selectedUnit;
    }

    public void Enter()
    {
        //TODO: display 
    }

    public void Exit()
    {
        //TODO: hide movement range here
    }

    public void OnTileClicked(TileScript tile)
    {
        //check if they select a different unit
        if (tile.OccupiedUnit != null) { 
            Unit unitComponent = tile.OccupiedUnit.GetComponent<Unit>();

            //if its same team as us
            if (unitComponent.Team == _playerController.TurnManager.GetCurrentPlayer())
            {
                // If it's our unit, change to the UnitSelectedState.
                _playerController.ChangeState(new UnitSelectedState(_playerController, unitComponent));
                return;
            }
        }

        //decides what the selected unit should do
        _playerController.UnitManager.HandleUnitCommand(_selectedUnitComponent, tile);

        _playerController.ChangeState(new DefaultState(_playerController));

    }
}
