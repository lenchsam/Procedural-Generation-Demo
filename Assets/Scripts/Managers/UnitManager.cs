using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UnitManager : MonoBehaviour
{

    [SerializeField] private TurnManager _turnManager;
    [SerializeField] private HexGrid _hexGrid;
    [SerializeField] private PathFinding _pathFinding;

    void Start()
    {
        _turnManager.OnTurnEnd.AddListener(HandleTurnEnd);
    }
    private void OnDestroy()
    {
        _turnManager.OnTurnEnd.RemoveListener(HandleTurnEnd);
    }

    public void HandleUnitCommand(Unit selectedUnitComponent, TileScript tile)
    {
        if (tile.OccupiedUnit != null)
        {
            //dont need to check team as its checked before this function is called (UnitSelectedState line 30)
            Debug.Log("unit attacking");
            selectedUnitComponent.Attack(tile.OccupiedUnit.GetComponent<Unit>());
        }
        else
        {
            //move unit

            _hexGrid.GetTileScriptFromPosition(selectedUnitComponent.gameObject.transform.position).OccupiedUnit = null;
            _hexGrid.GetTileScriptFromPosition(selectedUnitComponent.gameObject.transform.position).IsWalkable = true;

            Vector2Int startCoords = _hexGrid.GetIntCordsFromPosition(selectedUnitComponent.gameObject.transform.position);
            Vector2Int targetCoords = _hexGrid.GetIntCordsFromPosition(tile.transform.position);
            List<GameObject> path = _pathFinding.FindPath(startCoords, targetCoords);
            StartCoroutine(lerpToPosition(selectedUnitComponent.gameObject.transform.position, path, _pathFinding.UnitMovementSpeed, selectedUnitComponent.gameObject));

            tile.GetComponent<TileScript>().OccupiedUnit = selectedUnitComponent.gameObject;
            tile.GetComponent<TileScript>().IsWalkable = false;
        }
    }

    private void HandleTurnEnd()
    {

    }

    private IEnumerator lerpToPosition(Vector3 startPosition, List<GameObject> targetPositions, float unitMovementSpeed, GameObject gameObjectToMove)
    {
        Debug.Log("moving unit");
        foreach (GameObject targetPosition in targetPositions)
        {
            Vector3 adjustedTarget = new Vector3(targetPosition.transform.position.x, targetPosition.transform.position.y, targetPosition.transform.position.z);


            float distance = Vector3.Distance(gameObjectToMove.transform.position, adjustedTarget);
            float duration = distance / unitMovementSpeed;

            float timeElapsed = 0f;

            while (timeElapsed < duration)
            {
                
                gameObjectToMove.transform.position = Vector3.Lerp(gameObjectToMove.transform.position, adjustedTarget, timeElapsed / duration);

                timeElapsed += Time.deltaTime;

                yield return null;
            }

            gameObjectToMove.transform.position = adjustedTarget;
        }
    }
}
