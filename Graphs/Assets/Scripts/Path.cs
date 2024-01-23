using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Path : ICloneable
{
    public List<Vertex> PathPoints;
    public float PathLength;

    public Path()
    {
        PathPoints = new List<Vertex>();
    }

    public void AddPoint(Vertex point, float Distance)
    {
        PathPoints.Add(point);
        PathLength += Distance;
    }

    public void RemoveLastPoint(int length)
    {
        PathPoints.RemoveAt(PathPoints.Count - 1);
        PathLength -= length;
    }

    public object Clone()
    {
        Path path = new Path();
        foreach (Vertex vertex in PathPoints)
        {
            path.PathPoints.Add(vertex);
        }
        path.PathLength = PathLength;
        return path;
    }
}
