using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour {

    public class CellInfo
    {
        public float perlinValue { get; set; }

        private RoadRouter.Type type = RoadRouter.Type.none;
        public RoadRouter.Type Type { get { return type; } set { type = value; } }

        //
        public CellInfo() { }

        //
        public int[] roads = null;

        //
        public void InitNode(Material mat, Token newToken)
        {
            token = newToken;
            token.material = mat;

            type = RoadRouter.Type.node;
            roads = new int[HexMasterData.directionCount];
        }

        //
        public void InitRoadDirection(int direction, int length)
        {
            roads[direction] = length;
        }

        //
        public int RoadLength(int direction)
        {
            return roads[direction];
        }

        // 
        public Token token { get; set; }
    }

    //
    public GameObject mark;

    //
    public CellInfo cellInfo { get; set; }

    //
    public void InitToken(Material mat, GameObject tokenPref, RoadRouter.Type type)
    {
        GameObject token = Instantiate(tokenPref, transform);
        cellInfo.InitNode(mat, token.GetComponent<Token>());
        cellInfo.Type = type;
    }

    //
    bool showPerlin;
    public bool ShowPerlin { get { return showPerlin; } set { showPerlin = value; } }
    
    // Cell position
    Vector2Int m_pos2D;
    public Vector2Int pos2D { get { return m_pos2D; } set { m_pos2D = value; } }
    public Vector3Int pos3D { get { return new Vector3Int(pos2D.x, pos2D.y, 0 - pos2D.x - pos2D.y); }}

    // Mesh filter component
    MeshFilter meshFilter;

    // Mesh renderer component
    MeshRenderer meshRenderer;
    //
    public Material MainMaterial { get { return meshRenderer.materials[0]; } set { meshRenderer.materials = new Material[2] { value, meshRenderer.materials[1] }; } }
    public Material HatchMaterial { get { return meshRenderer.materials[1]; } set { meshRenderer.materials = new Material[2] { meshRenderer.materials[0], value }; } }

    // Mesh collider
    MeshCollider meshCollider;

    // My mesh object
    Mesh m_Mesh;

    // Init of cell
    public void Init(Vector2Int pos)
    {
        name = pos.x + " " + pos.y;
        m_pos2D = pos;
    }

    // Awake
    void Awake ()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();

        m_Mesh = HexMasterData.hexMesh;
        meshFilter.mesh = m_Mesh;

        //foreach (Vector3 x in HexMasterData.halfCorners)
        //    Instantiate(mark, transform).transform.localPosition = x;


        meshCollider.sharedMesh = m_Mesh;
        cellInfo = new CellInfo();

        meshRenderer.materials = new Material[2];

    }
}
