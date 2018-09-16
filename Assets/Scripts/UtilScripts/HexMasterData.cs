using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexMasterData
{
    // Hex outer radius
    public static float outer_r = 10;
    // Hex inner radius
    public static float inner_r = outer_r * 0.866025404f;

    // Count of triangles at hex in c-clockwwise direction
    const int fanTriCount = 6;

    //
    static Mesh m_hexMesh = null;
    public static Mesh hexMesh
    {
        get
        {
            if (m_hexMesh == null) InitGeometry();

            return m_hexMesh;
        }
    }

    static List<Vector3> hexVertList = null;
    public static List<Vector3> HexVerticesList
    {
        get
        {
            if (hexVertList == null) hexProcMeshSetup();

            return hexVertList;
        }
    }

    static List<int> hexTrianglesList = null;
    public static List<int> HexTrianglesList
    {
        get
        {
            if (hexTrianglesList == null) hexProcMeshSetup();

            return hexTrianglesList;
        }
    }

    static List<Vector2> hexUvList = null;
    public static List<Vector2> HexUvList
    {
        get
        {
            if (hexUvList == null) hexProcMeshSetup();
            return hexUvList;
        }
    }

    static void hexProcMeshSetup()
    {
        hexVertList = new List<Vector3>();
        hexUvList = new List<Vector2>();
        hexTrianglesList = new List<int>();

        for (int i = 0; i <= fanTriCount; ++i)
        {

            hexVertList.Add(Vector3.zero);
            hexVertList.Add(HexMasterData.corners[i]);
            hexVertList.Add(HexMasterData.corners[i + 1]);

            hexUvList.Add(new Vector2(0.5f, 0.5f));
            hexUvList.Add(HexMasterData.uv[i]);
            hexUvList.Add(HexMasterData.uv[i + 1]);

            hexTrianglesList.Add(i * 3);
            hexTrianglesList.Add(i * 3 + 1);
            hexTrianglesList.Add(i * 3 + 2);
        }
    }

    public static int directionCount = 6;


    // Hex direction indexes
    public enum HexDirections { frontRight = 0, frontLeft = 1, left = 2, bottomLeft = 3, bottomRight = 4, right = 5}
    // Hex directions in vector
    public static Vector2Int[] directions = {
        // front right
        new Vector2Int(0, 1),
        // front left
        new Vector2Int(-1, 1),
        // left
        new Vector2Int(-1, 0),
        // bottom left
        new Vector2Int(0, -1),
        // bottom right
        new Vector2Int(1, -1),
        // right
        new Vector2Int(1, 0)
    };

    // Hex local coords
    public static Vector3[] corners = {
        new Vector3(0f, 0f, outer_r),
        new Vector3(inner_r, 0f, 0.5f * outer_r),
        new Vector3(inner_r, 0f, -0.5f * outer_r),
        new Vector3(0f, 0f, -outer_r),
        new Vector3(-inner_r, 0f, -0.5f * outer_r),
        new Vector3(-inner_r, 0f, 0.5f * outer_r),
        new Vector3(-inner_r, 0f, 0.5f * outer_r),
        new Vector3(0f, 0f, outer_r)
    };

    // Hex local half coords
    public static Vector3[] halfCorners = {
        new Vector3(inner_r, 0f, 0.0f),
        new Vector3(inner_r, 0f, 0.5f * outer_r),
        new Vector3(inner_r * 0.5f, 0f, 0.75f * outer_r),

        new Vector3(0f, 0f, outer_r),
        new Vector3(-inner_r * 0.5f, 0f, 0.75f * outer_r),
        new Vector3(-inner_r, 0f, 0.5f * outer_r),

        new Vector3(-inner_r, 0f, 0.0f),
        new Vector3(-inner_r, 0f, -0.5f * outer_r),
        new Vector3(-inner_r * 0.5f, 0f, -0.75f * outer_r),

        new Vector3(0f, 0f, -outer_r),
        new Vector3(inner_r * 0.5f, 0f, 0.75f * -outer_r),
        new Vector3(inner_r, 0f, -0.5f * outer_r)
    };

    // UV coords
    public static Vector2[] uv = {
        new Vector2(0.5f, 1.0f),
        new Vector2(1.0f, 0.75f),
        new Vector2(1.0f, 0.25f),
        new Vector2(0.5f, 0.0f),
        new Vector2(0f, 0.25f),
        new Vector2(0f, 0.75f),
        new Vector2(0f, 0.75f),
        new Vector2(0.5f, 1.0f),
    };

    // Init of hex mesh
    static void InitGeometry()
    {
        m_hexMesh = new Mesh();

        m_hexMesh.name = "Hex Mesh";

        m_hexMesh.SetVertices(HexVerticesList);
        m_hexMesh.SetUVs(0, HexUvList);
        m_hexMesh.subMeshCount = 2;
        m_hexMesh.SetTriangles(HexTrianglesList, 0);
        m_hexMesh.SetTriangles(HexTrianglesList, 1);
        
        m_hexMesh.RecalculateNormals();
    }

    // Hex positioning relative to origin point at (0,0,0) in cube coord's
    public static Vector3 GetHexPosition(int x, int y)
    {
        return new Vector3(
                    (y + x * 0.5f - x / 2) * inner_r * 2.0f,
                    0,
                    x * outer_r * 1.5f
                );
    }
    // 
    public static Vector2Int GetHexCoordsInCube(int x, int y)
    {
        return new Vector2Int(x - y / 2, y);
    }
}