using UnityEngine;

public class DefaultState : IState
{
    private PlayerController _playerController;

    public DefaultState(PlayerController playerController)
    {
        _playerController = playerController;
    }
    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }

    public void OnTileClicked(TileScript tile)
    {
        
        if(tile.OccupiedUnit == null) { return; }

        Unit unitComponent = tile.OccupiedUnit.GetComponent<Unit>();

        if (unitComponent.Team == _playerController.TurnManager.GetCurrentPlayer())
        {
            // If it's our unit, change to the UnitSelectedState.
            _playerController.ChangeState(new UnitSelectedState(_playerController, unitComponent));
            return;
        }

    }

}
