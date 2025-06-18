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

    //for hilighting reachable tiles
    public List<TileScript> GetReachableTiles(Vector2Int startCoords, int maxMovementPoints)
    {
        //dictionary stores all tiles that we have found a path to
        //int value is the lowest movement cost found so far to reach that tile
        Dictionary<TileScript, int> costSoFar = new Dictionary<TileScript, int>();

        List<TileScript> frontier = new List<TileScript>();

        TileScript startTile = _hexGrid.GetTileScriptFromIntCords(startCoords);
        if (startTile == null) return new List<TileScript>();

        frontier.Add(startTile);
        costSoFar[startTile] = 0; //0 points to be at the start.

        while (frontier.Count > 0)
        {
            //get tile from the frontier with the lowest cost so far
            frontier.Sort((a, b) => costSoFar[a].CompareTo(costSoFar[b]));
            TileScript current = frontier[0];
            frontier.RemoveAt(0);

            //explore neighbors of current tile
            foreach (GameObject neighborGO in _hexGrid.GetSurroundingTiles(current.gameObject))
            {
                TileScript neighbor = neighborGO.GetComponent<TileScript>();

                if (neighbor == null || !neighbor.IsWalkable) continue;

                //calculate cost to move to this neighbor
                int newCost = costSoFar[current] + neighbor.MovementCost;

                //if path is within movement budget
                if (newCost <= maxMovementPoints)
                {
                    //if haven't seen this tile before or we've found a cheaper path to it
                    if (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor])
                    {
                        //update cost and add it to the frontier to be explored
                        costSoFar[neighbor] = newCost;
                        if (!frontier.Contains(neighbor))
                        {
                            frontier.Add(neighbor);
                        }
                    }
                }
            }
        }

        //return all tiles we found a path to
        return new List<TileScript>(costSoFar.Keys);
    }


    //finds cheapest path from a start point to a target point using A*
    public List<GameObject> FindPath(Vector2Int startCoords, Vector2Int targetCoords)
    {
        TileScript startTile = _hexGrid.GetTileScriptFromIntCords(startCoords);
        TileScript targetTile = _hexGrid.GetTileScriptFromIntCords(targetCoords);

        if (startTile == null || targetTile == null || !targetTile.IsWalkable) return null;

        List<TileScript> openList = new List<TileScript>();
        HashSet<TileScript> closedList = new HashSet<TileScript>();

        //gCost is the accumilated movement cost from the start
        Dictionary<TileScript, int> gCost = new Dictionary<TileScript, int>();
        Dictionary<TileScript, int> fCost = new Dictionary<TileScript, int>();
        Dictionary<TileScript, TileScript> cameFrom = new Dictionary<TileScript, TileScript>();

        openList.Add(startTile);
        gCost[startTile] = 0;
        fCost[startTile] = _hexGrid.DistanceBetweenTiles(startCoords, targetCoords);

        while (openList.Count > 0)
        {
            //find tile in the open list with the lowest fCost
            TileScript currentTile = null;
            int lowestFCost = int.MaxValue;
            foreach (TileScript tileInOpenList in openList)
            {
                if (fCost[tileInOpenList] < lowestFCost)
                {
                    lowestFCost = fCost[tileInOpenList];
                    currentTile = tileInOpenList;
                }
            }

            if (currentTile == targetTile)
            {
                return ReconstructPath(cameFrom, currentTile);
            }

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            foreach (GameObject neighborGO in _hexGrid.GetSurroundingTiles(currentTile.gameObject))
            {
                TileScript neighbor = neighborGO.GetComponent<TileScript>();

                if (neighbor == null || !neighbor.IsWalkable || closedList.Contains(neighbor))
                {
                    continue;
                }

                //the cost to move to neighbor is its own MovementCost
                int tentativeGCost = gCost[currentTile] + neighbor.MovementCost;

                if (!gCost.ContainsKey(neighbor) || tentativeGCost < gCost[neighbor])
                {
                    cameFrom[neighbor] = currentTile;
                    gCost[neighbor] = tentativeGCost;
                    fCost[neighbor] = tentativeGCost + _hexGrid.DistanceBetweenTiles(neighbor.IntCoords, targetCoords);

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        return null; //no path found
    }

    private List<GameObject> ReconstructPath(Dictionary<TileScript, TileScript> cameFrom, TileScript currentTile)
    {
        List<GameObject> totalPath = new List<GameObject> { currentTile.gameObject };
        while (cameFrom.ContainsKey(currentTile))
        {
            currentTile = cameFrom[currentTile];
            totalPath.Add(currentTile.gameObject);
        }
        totalPath.Reverse();
        return totalPath;
    }
}
