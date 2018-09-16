using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadNetwork : MonoBehaviour {

    //
    public float m_roadWidth = 5;
    //
    public GameObject roadPrefab;
    // Cell < Road < segments
    Dictionary<Vector2Int, List<Road>> m_roadsInCells;
    List<Road> m_roads;

    public HexGrid m_grid { get; set; }

    void Awake()
    {
        m_roads = new List<Road>();
        m_roadsInCells = new Dictionary<Vector2Int, List<Road>>();
    }

    public Road InitRoad(Vector2Int cellPosition)
    {
        GameObject roadGO = Instantiate(roadPrefab, transform);
        Road road = roadGO.GetComponent<Road>();

        //road.Init(m_roadWidth);
        
        if (!m_roadsInCells.ContainsKey(cellPosition))
            m_roadsInCells.Add(cellPosition, new List<Road>());
        
        m_roads.Add(road);
        m_roadsInCells[cellPosition].Add(road);

        return road;
     }

    public void ContinueRoad(Road road, Vector2Int cellPosition, List<Vector2> segments)
    {
        // road points convertation
        GameObject cell = m_grid.GetCellByVector2D(cellPosition).gameObject;

        for (int i = 0; i < segments.Count; ++i)
            segments[i] = cell.transform.TransformPoint(segments[i]);

        road.AddSegment(cellPosition, segments);
    }
}
