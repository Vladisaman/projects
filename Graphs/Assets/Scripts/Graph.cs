using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    private Vertex[] vertices;
    private int[,] matrix;
    private int maxSize;
    private int currSize;

    public Graph(int maxSize)
    {
        currSize = 0;
        this.maxSize = maxSize;
        vertices = new Vertex[maxSize];
        matrix = new int[maxSize,maxSize];
    }

    public void AddVertex(Vertex vertex)
    {
        if (currSize < maxSize)
        {
            vertices[currSize] = vertex;
            currSize++;
        }
    }

    public void AddConnection(int firstIndex, int secondIndex, int Distance)
    {
        matrix[firstIndex, secondIndex] = Distance;
    }

    public int FindIndex(Vertex vertex, int StartIndex, int EndIndex)
    {
        for (int i = StartIndex; i <= EndIndex; i++)
        {
            if (vertices[i] == vertex)
            {
                return i;
            }
        }
        return -1;
    }
    public Path FindShortestPath(Vertex StartPoint, Vertex EndPoint, int StartIndex, int EndIndex)
    {
        Debug.Log("pos = " + EndPoint.position + "  Sindex = " + vertices[StartIndex].position);
        Path path = new Path();
        path.AddPoint(StartPoint, 0);
        Stack<Vertex> stack = new Stack<Vertex>();
        stack.Push(StartPoint);
        StartPoint.hasBeenVisited = true;

        Path[] paths = new Path[currSize];

        while (stack.Count > 0)
        {
            int TempInd = FindIndex(stack.Peek(), StartIndex, EndIndex);

            Debug.Log("Start = " + StartIndex + "| End = " + EndIndex + "| Temp = " + TempInd);

            for (int i = StartIndex; i <= EndIndex; i++)
            {
                if (matrix[TempInd, i] > 0)
                {
                    Path newPath = (Path)path.Clone();
                    newPath.AddPoint(vertices[i], matrix[TempInd, i]);
                    if (paths[i] == null || paths[i].PathLength > newPath.PathLength)
                    {
                        paths[i] = newPath;
                    }
                }
            }

            Vertex TempVertex = FindClosestVertex(stack.Peek(), StartIndex, EndIndex);

            if (TempVertex == null)
            {
                if (stack.Count > 1)
                {
                    int tempint = FindIndex(stack.Pop(), StartIndex, EndIndex);
                    path.RemoveLastPoint(matrix[FindIndex(stack.Peek(), StartIndex, EndIndex), tempint]);
                } else
                {
                    stack.Pop();
                    path.RemoveLastPoint(0);
                }
            } else
            {
                path.AddPoint(TempVertex, matrix[FindIndex(stack.Peek(), StartIndex, EndIndex), FindIndex(TempVertex, StartIndex, EndIndex)]);
                stack.Push(TempVertex);
                stack.Peek().hasBeenVisited = true;
            }
        }
        return paths[FindIndex(EndPoint, StartIndex, EndIndex)];
    }

    public Vertex FindClosestVertex(Vertex StartPoint, int StartIndex, int EndIndex)
    {
        int StartPointIndex = FindIndex(StartPoint, StartIndex, EndIndex);
        int TempDistIndex = -1;

        for (int i = StartIndex; i <= EndIndex; i++)
        {
            if (matrix[StartPointIndex, i] > 0 && !vertices[i].hasBeenVisited)
            {
                if (TempDistIndex == -1)
                {
                    TempDistIndex = i;
                }
                else if (matrix[StartPointIndex, TempDistIndex] > matrix[StartPointIndex, i])
                {
                    TempDistIndex = i;
                }
            }
        }
        if (TempDistIndex > -1)
        {
            return vertices[TempDistIndex];
        }
        return null;
    }

    public void ClearConnections()
    {
        for (int i = 0; i <= matrix.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= matrix.GetUpperBound(1); j++)
            {
                matrix[i, j] = 0;
            }
        }
    }
}
