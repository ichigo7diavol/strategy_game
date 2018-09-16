using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSegment {

    //
    public List<Vector2> points { get; set; }

    public RoadSegment()
    {
        points = new List<Vector2>();
    }

    public void AddSegmentPart(List<Vector2> segments)
    {
        points.AddRange(segments);
    }
}
