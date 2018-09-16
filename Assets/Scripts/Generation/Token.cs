using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour {

    MeshFilter m_meshFilter;
    MeshRenderer m_meshRenderer;
    
    //
    public Material material { get { return m_meshRenderer.material; } set { m_meshRenderer.material = value; } }

    void Awake()
    {
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshFilter.mesh = QuadMasterData.quadMesh;
        m_meshRenderer = GetComponent<MeshRenderer>();
    }
}
