using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuadMasterData
{
    // Quad outer radius
    public static float width = 20;
    // Quad inner radius
    public static float length = 20;

    // 
    const int triangilationPower = 100;

    //
    public static bool isTile = true;

    //
    static Mesh m_quadMesh = null;
    public static Mesh quadMesh
    {
        get
        {
            if (m_quadMesh == null) InitGeometry();

            return m_quadMesh;
        }
    }

    static List<Vector3> quadVertList = null;
    public static List<Vector3> QuadVerticesList
    {
        get
        {
            if (quadVertList == null) quadProcMeshSetup();

            return quadVertList;
        }
    }

    static List<int> quadTrianglesList = null;
    public static List<int> QuadTrianglesList
    {
        get
        {
            if (quadTrianglesList == null) quadProcMeshSetup();

            return quadTrianglesList;
        }
    }

    static List<Vector2> quadUvList = null;
    public static List<Vector2> QuadUvList
    {
        get
        {
            if (quadUvList == null) quadProcMeshSetup();
            return quadUvList;
        }
    }

    // Quad direction indexes
    enum QuadDirections { }
    // Quad directions in vector
    public static Vector2Int[] directions = {
    };

    // Quad local coords
    public static Vector3[] corners = {
        // fist tri //
        // bottom-left
        new Vector3(0,0,0),
        // upper-left
        new Vector3(0,0,1),
        // upper-right
        new Vector3(1,0,1),

        // second tri //
        // upper-left
        new Vector3(0,0,0),
        // upper-right
        new Vector3(1,0,1),
        // bottom-right
        new Vector3(1,0,0)
    };

    // UV coords
    public static Vector2[] uv = {
        new Vector2(0,0),
        new Vector2(0,1),
        new Vector2(1,1),
        new Vector2(0,0),
        new Vector2(1,1),
        new Vector2(1,0)
    };

    //
    static void quadProcMeshSetup()
    {
        quadVertList = new List<Vector3>();
        quadUvList = new List<Vector2>();
        quadTrianglesList = new List<int>();

        float yStep = length / triangilationPower;
        float xStep = width / triangilationPower;

        for (int y = 0; y < triangilationPower; ++y)
        {
            float currYStep = yStep * y;
            float uvYStep = y * 1.0f / triangilationPower;

            for (int x = 0; x < triangilationPower; ++x)
            {
                float currXStep = xStep * x;
                float uvXStep = x * 1.0f / triangilationPower;

                for (int i = 0; i < 6; ++i)
                {
                    Vector3 currCorner = new Vector3(corners[i].x * xStep, 0, corners[i].z * yStep);
                    quadVertList.Add(new Vector3(-width / 2 + currXStep, 0, -length / 2 + currYStep) + currCorner);
                    quadTrianglesList.Add(i + 6 * x + 6 * triangilationPower * y);
                    if (isTile)
                        quadUvList.Add(new Vector2(uvXStep + 1.0f / triangilationPower * uv[i].x, uvYStep + 1.0f / triangilationPower * uv[i].y));
                    else
                        quadUvList.Add(corners[i]);
                }
            }
        }
    }

    // Init of quad mesh
    static void InitGeometry()
    {
        m_quadMesh = new Mesh();

        m_quadMesh.name = "Quad Mesh";

        m_quadMesh.SetVertices(QuadVerticesList);
        m_quadMesh.SetUVs(0, QuadUvList);
        m_quadMesh.subMeshCount = 2;
        m_quadMesh.SetTriangles(QuadTrianglesList, 0);
        m_quadMesh.SetTriangles(QuadTrianglesList, 1);

        m_quadMesh.RecalculateNormals();
    }

    // Quad positioning relative to origin point at (0,0,0) in cube coord's
    public static Vector3 GetQuadPosition(int x, int y)
    {
        return new Vector3(0,0,0);
    }

    // 
    public static Vector2Int GetQuadCoordsInCube(int x, int y)
    {
        return new Vector2Int(x - y / 2, y);
    }
}
