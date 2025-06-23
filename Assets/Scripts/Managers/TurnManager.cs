using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    public UnityEvent OnTurnEnd = new UnityEvent();

    [SerializeField] private HexGrid _hexGrid;
    [SerializeField] Transform _cameraTransform;

    [SerializeField] private List<PlayerData> _players = new List<PlayerData>();
    private int _currentPlayerIndex = 0;

    public PlayerData GetCurrentPlayerData()
    {
        return _players[_currentPlayerIndex];
    }
    public e_Team GetCurrentPlayer()
    {
        return GetCurrentPlayerData().Player;
    }

    public void InitializePlayers(Vector2Int[] startPositions, GameObject startUnitPrefab)
    {
        _players.Clear();
        for (int i = 0; i < startPositions.Length; i++)
        {
            e_Team team = (e_Team)i;

            Vector3 initialCamPos = new Vector3(
                _hexGrid.GetTileFromIntCords(startPositions[i]).transform.position.x,
                _cameraTransform.position.y,
                _hexGrid.GetTileFromIntCords(startPositions[i]).transform.position.z
            );

            _players.Add(new PlayerData(team, initialCamPos));

            GameObject unit = GameObject.Instantiate(startUnitPrefab, _hexGrid.GetTileFromIntCords(startPositions[i]).transform.position, Quaternion.identity);
            _hexGrid.GetTileScriptFromIntCords(startPositions[i]).OccupiedUnit = unit;
            unit.GetComponent<Unit>().Team = team;

        }

        _cameraTransform.position = _players[_currentPlayerIndex].SavedCameraPosition;
    }

    public void EndTurn()
    {
        PlayerData currentPlayer = GetCurrentPlayerData();
        if (currentPlayer == null) return;

        currentPlayer.SavedCameraPosition = _cameraTransform.position;

        _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count; //modulo loops back to 0
        PlayerData nextPlayer = GetCurrentPlayerData();

        ChangeCamera(nextPlayer.SavedCameraPosition);

        PlayerController playerController = FindFirstObjectByType<PlayerController>();
        playerController.ChangeState(new DefaultState(playerController));

        OnTurnEnd.Invoke();
    }

    private void ChangeCamera(Vector3 newPosition)
    {
        _cameraTransform.position = newPosition;
    }

    //private void ChangeFOW(List<Vector2Int> RevealedTiles, List<Vector2Int> TilesToBlock){
    //    foreach(Vector2Int tileCords in RevealedTiles){
    //        _hexGrid.GetTileScriptFromIntCords(tileCords).Reveal();
    //    }
    //    foreach(Vector2Int tileCords in TilesToBlock){
    //        _hexGrid.GetTileScriptFromIntCords(tileCords).ReBlock();
    //    }
    //}
}
