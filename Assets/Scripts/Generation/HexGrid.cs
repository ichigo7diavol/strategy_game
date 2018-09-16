using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class HexGrid : MonoBehaviour
{
    // Grid size
    [Header("Grid size params")]
    [RangeAttribute(0, 64)]
    [DelayedAttribute]
    public int height = 10;
    public int Height { get { return height; } }
    [RangeAttribute(0, 64)]
    [DelayedAttribute]
    public int width = 10;
    public int Width { get { return width; } }
    int size = 100;
    public int Size { get { return Size; } }

    [Header("Cell prefab")]
    // Cell prefab
    public GameObject cellPrefab;

    // Cells array
    GameObject[] cells = null;
    
    [Header("Flags")]
    [SerializeField]
    bool isDebug = true;
    public bool IsDebug { get { return isDebug; } set { isDebug = value; } }

    // hex accessor to return it as HexCell by RA
    public HexCell GetCell(int x, int y)
    {
        return cells[x + y * width].GetComponent<HexCell>();
    }

    // hex accessor to return it as GO by RA
    public GameObject GetCellAsGO(int x, int y)
    {
        return cells[x + y * width];
    }

    // safe accessor to get cell by cube coordinates without Z
    public HexCell GetCellByVector2D(Vector2Int vector)
    {
        if (vector.x < -vector.y / 2 || vector.x >= width - vector.y / 2)
            return null;

        int index = vector.x + vector.y * width + vector.y / 2;
        DebugRoutine("[" + GetType() + ":" + name + "]" + "\nRequired hex index was: " + index + "\nAnd size is: " + size, null);

        if (index < size && index >= 0)
            return cells[index].GetComponent<HexCell>();
        else
            return null;
    }

    // same but without checking 
    public HexCell GetCellByVector2DRaw(Vector2Int vector)
    {
        return cells[vector.x + vector.y * width + vector.y / 2].GetComponent<HexCell>();
    }

    // get center cell
    public HexCell center { get { return GetCellByVector2D(new Vector2Int(Width / 2 - Height / 4, Height / 2)); } }

    // set cell by x,y coord's (not cube)
    public GameObject SetCell(int x, int y, GameObject obj)
    {
        cells[x * width + y] = obj;
        return obj;
    }

    // Use this for initialization
    void Awake()
    {
        DebugAssetion();
        GridInit();
    }

    // editor value validating in-built callback
    void OnValidate() {
        DebugRoutine("Something changes at: ", gameObject);

        size = height * width;
        if (Application.isPlaying && cells != null && false)
        {
            DebugRoutine("On validate!", null);
            DestroyGrid();
            GridInit();
        }
    }

    // Creating cell grid with default prefab params
    void GridInit() {

        name = "Hex grid";
        size = height * width;

        cells = new GameObject[height * width];

        if (cells == null)
            return;

        for (int i = 0; i < height; ++i)
            for (int j = 0; j < width; ++j)
            {
                cells[i * width + j] = Instantiate(cellPrefab, HexMasterData.GetHexPosition(i, j), Quaternion.identity, transform);
                HexCell cell = cells[i * width + j].GetComponent<HexCell>();
                cell.Init(HexMasterData.GetHexCoordsInCube(j, i));
            }
    }
    
    // Optimize it and correct
    void DestroyGrid()
    {
        for (int i = 0; i < cells.Length; ++i)
        {
            DebugRoutine("Destroy:", cells[i]);

            var cell = cells[i];
            cells[i] = null;
            Destroy(cell);
        }
        cells = null;
    }

    // Debug messaging
    GameObject DebugRoutine(string msg, GameObject obj)
    {
        return obj;
    }

    // Init assertions routine
    void DebugAssetion()
    {
        Assert.IsNotNull(cellPrefab, "Default cell prefab required!");
    }
}
