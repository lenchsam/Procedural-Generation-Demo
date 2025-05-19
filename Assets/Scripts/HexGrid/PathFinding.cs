using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    private HexGrid _hexGrid;
    public int UnitMovementSpeed;
    void Start()
    {
        _hexGrid = FindAnyObjectByType<HexGrid>();

        
    }
    public List<GameObject> FindPath(Vector2Int startCoords, Vector2Int targetCoords)
    {
        if (_hexGrid == null)
        {
            Debug.LogError("HexGrid reference is null in PathFinding.FindPath. Ensure HexGrid is present and PathFinding.Start() has run.");
            return null;
        }

        TileScript startTile = _hexGrid.GetTileScriptFromIntCords(startCoords);
        TileScript targetTile = _hexGrid.GetTileScriptFromIntCords(targetCoords);

        if (startTile == null)
        {
            Debug.LogError($"Start tile not found at coordinates: {startCoords}");
            return null;
        }
        if (targetTile == null)
        {
            Debug.LogError($"Target tile not found at coordinates: {targetCoords}");
            return null;
        }
        
        // Handle the case where the start and target are the same tile
        if (startTile == targetTile)
        {
            Debug.Log("Start and target tiles are the same.");
            return new List<GameObject> { startTile.gameObject };
        }

        // open list to track nodes to be evaluated
        List<TileScript> openList = new List<TileScript>();
        // closed list to track nodes already evaluated
        HashSet<TileScript> closedList = new HashSet<TileScript>();
        // dictionary to store the cost of moving from the start node (gCost)
        Dictionary<TileScript, int> gCost = new Dictionary<TileScript, int>();
        // dictionary to store the total estimated cost (fCost = gCost + hCost)
        Dictionary<TileScript, int> fCost = new Dictionary<TileScript, int>();
        // dictionary to store the parent of each tile, used to reconstruct the path
        Dictionary<TileScript, TileScript> cameFrom = new Dictionary<TileScript, TileScript>();

        openList.Add(startTile);
        gCost[startTile] = 0;
        fCost[startTile] = _hexGrid.DistanceBetweenTiles(startCoords, targetCoords); // Heuristic for the start node

        while (openList.Count > 0)
        {
            // Find the tile in the open list with the lowest fCost
            // More robust selection of current tile from openList
            TileScript currentTile = null;
            int bestFCost = int.MaxValue;
            int bestHCost = int.MaxValue; // For tie-breaking (heuristic cost to target)
            int currentIndexInOpenList = -1;

            for (int i = 0; i < openList.Count; i++)
            {
                TileScript candidateNode = openList[i];
                if (!fCost.TryGetValue(candidateNode, out int candidateFCostValue))
                {
                    Debug.LogWarning($"Node {candidateNode.name} ({candidateNode.IntCoords}) in openList is missing fCost. Skipping.");
                    continue; //skips current iteration
                }

                int candidateHCostValue = _hexGrid.DistanceBetweenTiles(candidateNode.IntCoords, targetCoords);

                if (candidateFCostValue < bestFCost)
                {
                    bestFCost = candidateFCostValue;
                    bestHCost = candidateHCostValue;
                    currentTile = candidateNode;
                    currentIndexInOpenList = i;
                }
                else if (candidateFCostValue == bestFCost) // Tie-breaking: prefer smaller hCost
                {
                    if (candidateHCostValue < bestHCost)
                    {
                        bestHCost = candidateHCostValue;
                        currentTile = candidateNode;
                        currentIndexInOpenList = i;
                    }
                }
            }
            
            if (currentTile == null)
            {
                // This can happen if openList contained nodes that all lacked fCost entries.
                Debug.LogWarning("No suitable tile with fCost found in openList to process. Pathfinding cannot continue.");
                return null; // No path found or error state
            }

            // if we've reached the target, reconstruct the path and return it
            if (currentTile == targetTile)
            {
                return ReconstructPath(cameFrom, currentTile);
            }

            // move current tile from open to closed list
            openList.RemoveAt(currentIndexInOpenList);
            closedList.Add(currentTile);
            
            // loop through each neighbor of the current tile
            List<GameObject> neighborGameObjects = _hexGrid.GetSurroundingTiles(currentTile.gameObject);
            if (neighborGameObjects == null) {
                 Debug.LogWarning($"GetSurroundingTiles returned null for {currentTile.name}. Skipping neighbors.");
                 continue; //skips current iteration
            }

            foreach (GameObject neighborGO in neighborGameObjects)
            {
                if (neighborGO == null)
                {
                    Debug.LogWarning("Encountered a null neighbor GameObject. Skipping.");
                    continue; //skips current iteration
                }
                TileScript neighbor = neighborGO.GetComponent<TileScript>();

                // if neighborGO has tilescript
                if (neighbor == null)
                {
                    Debug.LogWarning($"Neighbor GameObject {neighborGO.name} is missing TileScript component. Skipping.");
                    continue; //skips current iteration
                }
                
                // skip this neighbor if it's not walkable or it's already in the closed list
                if (!neighbor.IsWalkable || closedList.Contains(neighbor))
                {
                    continue; //skips current iteration
                }

                // if gcost isnt found for currenttile
                if (!gCost.ContainsKey(currentTile))
                {
                    Debug.LogError($"CRITICAL: gCost not found for currentTile: {currentTile.name} ({currentTile.IntCoords}). This indicates a flaw in the A* logic.");
                    continue;
                }

                // calculate the tentative gCost for this neighbor
                // the cost from currentTile to neighbor is the distance between them
                int costFromCurrentToNeighbor = _hexGrid.DistanceBetweenTiles(currentTile.IntCoords, neighbor.IntCoords);
                int tentativeGCost = gCost[currentTile] + costFromCurrentToNeighbor;

                // if this is a new node or we found a better path, update gCost, fCost, and cameFrom
                bool needsUpdate = !gCost.ContainsKey(neighbor) || tentativeGCost < gCost[neighbor];
                if (needsUpdate)
                {
                    cameFrom[neighbor] = currentTile;
                    gCost[neighbor] = tentativeGCost;
                    fCost[neighbor] = tentativeGCost + _hexGrid.DistanceBetweenTiles(neighbor.IntCoords, targetCoords);

                    // adds neighbor to the open list if it's not already there
                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }
        Debug.Log($"No path found from {startCoords} to {targetCoords} after checking all reachable tiles.");
        return null;
    }
    private List<GameObject> ReconstructPath(Dictionary<TileScript, TileScript> cameFrom, TileScript currentTile)
    {
        List<GameObject> totalPath = new List<GameObject>();
        totalPath.Add(currentTile.gameObject);

        while (cameFrom.ContainsKey(currentTile))
        {
            currentTile = cameFrom[currentTile];
            totalPath.Add(currentTile.gameObject);
        }

        totalPath.Reverse(); // reverse to get the path from start to goal
        return totalPath;
    }
}
