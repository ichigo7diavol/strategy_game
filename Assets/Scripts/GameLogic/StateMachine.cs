using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;

[RequireComponent(typeof(RoadRouter))]
public class StateMachine : MonoBehaviour {

    public GameObject hexGridPrefab = null;
    public GameObject controllerPrefab = null;

    GameObject hexGridGO = null;
    HexGrid hexGrid = null;

    GameObject controllerGO = null;
    Control controller = null;

    GridPopulation populator = null;

    RoadRouter router = null;

    float time = 0;
    bool f = false;

    void Awake()
    {
        hexGridGO = Instantiate(hexGridPrefab);
        hexGrid = hexGridGO.GetComponent<HexGrid>();

        controllerGO = Instantiate(controllerPrefab);
        controller = controllerGO.GetComponent<Control>();

        populator = GetComponent<GridPopulation>();

        populator.PopulateGrid(hexGrid);
        router = GetComponent<RoadRouter>();

        router.StartRouting(hexGrid,hexGrid.center);
    }
}
