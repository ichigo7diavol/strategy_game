using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Linq;

public class AStar : MonoBehaviour {

    public struct starVals
    {
        public HexCell cell { get; set; }
        public int gScore { get; set; }
        public float fScore { get; set; }

        public starVals(HexCell cell, int gScore, float fScore) { this.cell = cell; this.gScore = gScore; this.fScore = fScore; }
    }

    void reconstruct(Dictionary<HexCell, HexCell> map, HexCell goal)
    {
        HexCell it = goal;
        while (map.ContainsKey(it))
        {
            it = map[it];
        }
    }

    // TODO: 
    void PathFinding(HexGrid hexGrid, HexCell start, HexCell goal)
    {
        Assert.IsNotNull(start, "Start hex is null!");
        Assert.IsNotNull(goal, "Goal hex is null!");

        Dictionary<HexCell, HexCell> cameFrom = new Dictionary<HexCell, HexCell>();
        Dictionary<int, starVals> openMap = new Dictionary<int, starVals>();

        openMap[start.GetHashCode()] = new starVals(start, 0, (goal.pos3D - start.pos3D).magnitude);

        HashSet<int> closedSet = new HashSet<int>();

        while (openMap.Count > 0)
        {
            int currentKey = openMap.Aggregate((l, r) => l.Value.fScore < r.Value.fScore ? l : r).Key;
            starVals current = openMap[currentKey];
            if (current.cell.GetHashCode() == goal.GetHashCode())
            {
                reconstruct(cameFrom, goal);
                return;
            }

            openMap.Remove(currentKey);
            closedSet.Add(currentKey);
            //current.cell.Material.color = Color.green;

            foreach (Vector2Int direction in HexMasterData.directions)
            {
                HexCell neighbor = hexGrid.GetCellByVector2D(current.cell.pos2D + direction);
                if (neighbor == null || closedSet.Contains(neighbor.GetHashCode()))
                    continue;
                // TODO: against 1 should using find distance function to avoid interface binding to current realization
                int tentative_gScore = current.gScore + 1;

                if (!openMap.ContainsKey(neighbor.GetHashCode()))
                    openMap.Add(neighbor.GetHashCode(), new starVals(neighbor, 0, 0));
                else if (tentative_gScore >= openMap[neighbor.GetHashCode()].gScore)
                    continue;

                cameFrom[neighbor] = current.cell;
                openMap.Remove(neighbor.GetHashCode());
                openMap.Add(neighbor.GetHashCode(), new starVals(neighbor, tentative_gScore, tentative_gScore + (goal.pos3D - neighbor.pos3D).magnitude));
            }
        }
    }
}
