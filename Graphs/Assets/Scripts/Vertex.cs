using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex
{
    public Vector3 position;
    public int LastIndex;
    public bool hasBeenVisited;
    public bool isIgnored;

    public Vertex(Vector3 pos)
    {
        isIgnored = false;
        position = pos;
        LastIndex = 0;
        hasBeenVisited = false;
    }
}