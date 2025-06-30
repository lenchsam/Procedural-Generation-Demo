using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class City : MonoBehaviour
{

    public Queue<CraftablesScriptableObject> buildQueue = new Queue<CraftablesScriptableObject>();

    private int turnsUntilConstructionComplete;

    [SerializeField] private TurnManager _turnManager;
    [SerializeField] private HexGrid _hexGrid;

    public e_Team Team;

    public void SetTeam(e_Team team)
    {
        Team = team;
    }   


    private void Start()
    {
        _turnManager = FindFirstObjectByType<TurnManager>();
        _hexGrid = FindFirstObjectByType<HexGrid>();

        _turnManager.OnTurnEnd.AddListener(TurnEnded);
    }
    private void OnDestroy()
    {
        _turnManager.OnTurnEnd.RemoveListener(TurnEnded);
    }

    private void TurnEnded()
    {
        if (buildQueue.Count == 0) { return; }

        //if its not our turn then do nothing
        if (_turnManager.GetCurrentPlayer() != Team) { return; }

        Debug.Log("taking 1 from turn");
        // - 1 from turns remaining for current item to be built, it clamps to 0 so it doesnt go negative
        turnsUntilConstructionComplete -= 1;
        
        //if building is complete
        if (turnsUntilConstructionComplete <= 0)
        {
            Debug.Log($"City {Team} has completed building {buildQueue.Peek().CraftablePrefab.name}");

            CraftablesScriptableObject thingToBuild = buildQueue.Dequeue(); 

            turnsUntilConstructionComplete = thingToBuild.TurnsToCraft;

            //instantiate unit prefab at the city position
            GameObject tile = _hexGrid.GetTileFromPosition(gameObject.transform.position);
            GameObject unit = Instantiate(thingToBuild.CraftablePrefab, tile.transform.position, Quaternion.identity);
            tile.GetComponent<TileScript>().OccupiedUnit = unit;
            unit.GetComponent<Unit>().Team = _turnManager.GetCurrentPlayer();


        }
    }

    public void AddToBuildQueue(CraftablesScriptableObject craftablesScriptableObject)
    {
        buildQueue.Enqueue(craftablesScriptableObject);
    }
}
