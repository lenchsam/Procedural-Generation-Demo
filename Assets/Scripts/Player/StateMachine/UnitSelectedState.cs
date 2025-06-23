using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedState : IState
{
    PlayerController _playerController;
    Unit _selectedUnitComponent;

    private List<TileScript> _validMoves;
    public UnitSelectedState(PlayerController playerController, Unit selectedUnit)
    {
        _playerController = playerController;
        _selectedUnitComponent = selectedUnit;
    }

    public void Enter()
    {
        TileScript tileScript = _playerController.HexGrid.GetTileScriptFromPosition(_selectedUnitComponent.transform.position);
        _validMoves = _playerController.PathFinding.GetReachableTiles(tileScript.IntCoords, _selectedUnitComponent.GetMovementPoints());

        ToggleMoveHighlights(true);
    }

    public void Exit()
    {
        ToggleMoveHighlights(false);
    }

    public void OnTileClicked(TileScript tile)
    {
        // if tile is not in range return to DefaultState as cannot move or attack there
        if (!_validMoves.Contains(tile))
        {
            Debug.Log("tile too far away");
            _playerController.ChangeState(new DefaultState(_playerController));
            return;
        }

        //check if they select a different unit
        if (tile.OccupiedUnit != null)
        {
            Unit unitComponent = tile.OccupiedUnit.GetComponent<Unit>();

            //if its same team as us
            if (unitComponent.Team == _playerController.TurnManager.GetCurrentPlayer())
            {
                //if it's our unit, change to the UnitSelectedState
                _playerController.ChangeState(new UnitSelectedState(_playerController, unitComponent));
                return;
            }
        }

        //decides what the selected unit should do
        _playerController.UnitManager.HandleUnitCommand(_selectedUnitComponent, tile);

        _playerController.ChangeState(new DefaultState(_playerController));
    }

    void ToggleMoveHighlights(bool enable)
    {
        foreach (TileScript tile in _validMoves)
        {
            // Assuming TileScript has a method to highlight the tile
            if (enable)
            {
                tile.Highlight();
            }
            else
            {
                tile.Unhighlight();
            }
        }
    }
}
