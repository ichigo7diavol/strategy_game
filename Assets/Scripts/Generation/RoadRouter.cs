using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadRouter : MonoBehaviour {

    public enum Type { none = 0 /**/, road = 1 /**/, node = 2 /**/ }

    public Material mat;
    public int processTimes = 30;
    public GameObject roadNetworkPrefab;
    RoadNetwork m_roadNetwork;
    HexGrid m_grid;
    List<Road> roads;

    void Awake()
    {
        m_roadNetwork = Instantiate(roadNetworkPrefab, m_grid.transform).GetComponent<RoadNetwork>();
    }

    public void StartRouting(HexGrid grid, HexCell startCell)
    {
        m_grid = grid;

        roads.Add(m_roadNetwork.InitRoad(m_grid.center.pos2D));

        for (int i = 0; i < processTimes && roads.Count > 0; ++i)
            ProcessNode();
    }

    public void ProcessNode()
    {
        foreach (Road road in roads)
        {
            //m_roadNetwork.ContinueRoad(,);
        }
    }
}
