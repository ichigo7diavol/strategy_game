using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {

    //
    public float m_roadWidth = 5;

    //
    Dictionary<Vector2Int, RoadSegment> m_segments;

    //
    RoadSegment m_head;
    public RoadSegment headSegment { get { return m_head; } }

    //
    RoadSegment m_tail;
    public RoadSegment tailSegment { get { return m_tail; } }

    //
    Vector2 m_headPos;
    public Vector2 headPos { get { return m_headPos; } }

    //
    Vector2 m_tailPos;
    public Vector2 tailPos { get { return m_tailPos; } }

    //
    Mesh m_mesh;
    MeshFilter m_meshFiler;

    //
    List<Vector3> m_vert;
    List<int> m_tri;
    
    //
    void Awake()
    {
        m_segments = new Dictionary<Vector2Int, RoadSegment>();
        m_vert = new List<Vector3>();
        m_tri = new List<int>();

        m_meshFiler = GetComponent<MeshFilter>();
    }

    //
    public void Init(float road_width, Vector2Int pos)
    {
        m_roadWidth = road_width;

        m_segments.Add(pos, m_tail = m_head = new RoadSegment());
        m_headPos = m_tailPos = pos;
    }

    //
    void RegenerateRoad()
    {
        m_vert.Clear();
        int tri_count = -1;

        foreach (KeyValuePair<Vector2Int, RoadSegment> segment in m_segments)
        {
            List<Vector2>.Enumerator segm_enum = segment.Value.points.GetEnumerator();
            

            while (segm_enum.MoveNext()) {

                Vector2 first = segm_enum.Current;
                print(segm_enum.MoveNext());
                Vector2 second = segm_enum.Current;
                Vector2 perpen_dir = Vector2.Perpendicular(first - second).normalized * m_roadWidth;

                m_vert.Add(first + perpen_dir);
                m_tri.Add(++tri_count);

                m_vert.Add(first + -1 * perpen_dir);
                m_tri.Add(++tri_count);

                m_vert.Add(second + perpen_dir);
                m_tri.Add(++tri_count);

                m_vert.Add(first + -1 * perpen_dir);
                m_tri.Add(++tri_count);

                m_vert.Add(second + -1 * perpen_dir);
                m_tri.Add(++tri_count);

                m_vert.Add(second + perpen_dir);
                m_tri.Add(++tri_count);

                foreach (Vector2 x in m_vert)
                    print(x);
            }
        }
        m_mesh = new Mesh();

        m_mesh.SetVertices(m_vert);
        m_mesh.SetTriangles(m_tri,0);
        m_mesh.RecalculateNormals();

        m_meshFiler.mesh = m_mesh;
    }

    //
    public void AddSegment(Vector2Int cell, List<Vector2> segments)
    {   
        if (!m_segments.ContainsKey(cell))
            m_segments.Add(cell, new RoadSegment());

        m_segments[cell].AddSegmentPart(segments);
        
        RegenerateRoad();
    }
}
