using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Selector : MonoBehaviour {

    Mesh mesh;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;

    // Default selector material
    public Material defaultMaterial;

    void Awake ()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        meshInit();
    }

    void meshInit ()
    {
        mesh = new Mesh();

        List<Vector3> vert = new List<Vector3>();

        vert.Add(new Vector3(-HexMasterData.inner_r, 1, -HexMasterData.inner_r));
        vert.Add(new Vector3(-HexMasterData.inner_r, 1, HexMasterData.inner_r));
        vert.Add(new Vector3(HexMasterData.inner_r, 1, HexMasterData.inner_r));
        vert.Add(new Vector3(HexMasterData.inner_r, 1, -HexMasterData.inner_r));

        mesh.SetVertices(vert);

        List<int> tri = new List<int>();

        tri.Add(0);
        tri.Add(1);
        tri.Add(2);

        tri.Add(0);
        tri.Add(2);
        tri.Add(3);

        mesh.SetTriangles(tri, 0);
        mesh.RecalculateNormals();

        List<Vector2> uv = new List<Vector2>();

        uv.Add(new Vector2(0.0f, 0.0f));
        uv.Add(new Vector2(0.0f, 1.0f));
        uv.Add(new Vector2(1.0f, 1.0f));
        uv.Add(new Vector2(1.0f, 0.0f));

        mesh.SetUVs(0 , uv);

        meshFilter.mesh = mesh;
        meshFilter.name = "Selector mesh";

        meshRenderer.material = defaultMaterial;
    }
}
