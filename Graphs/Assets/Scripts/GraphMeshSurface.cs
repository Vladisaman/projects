using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphMeshSurface : MonoBehaviour
{
    public static List<Transform> Obstacles;

    [SerializeField] public float MatrixSize;
    public static Vertex[,] FloorMatrix;
    public static Graph graph;

    private void Awake()
    {
        Obstacles = new List<Transform>();
        foreach(GameObject obstacle in GameObject.FindGameObjectsWithTag("WALL"))
        {
            Obstacles.Add(obstacle.transform);
        }
        BuildSurface(GameObject.FindGameObjectWithTag("Floor").transform);
    }

    public void BuildSurface(Transform floor)
    {
        FloorMatrix = new Vertex[(int)(floor.localScale.z / MatrixSize) + 1, (int)(floor.localScale.x / MatrixSize) + 1];
        graph = new Graph((FloorMatrix.GetUpperBound(1) + 1) * (FloorMatrix.GetUpperBound(0) + 1));

        for (int i = 0; i <= FloorMatrix.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= FloorMatrix.GetUpperBound(1); j++)
            {
                FloorMatrix[i, j] = new Vertex(new Vector3(j * MatrixSize - floor.localScale.x / 2, 0.75F, i * MatrixSize - floor.localScale.z / 2));
                graph.AddVertex(FloorMatrix[i, j]);
            }
        }

        for (int i = 0; i <= FloorMatrix.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= FloorMatrix.GetUpperBound(1); j++)
            {
                foreach (Transform obstacle in Obstacles)
                {
                    Vector3 leftBottom = new Vector3(obstacle.position.x - obstacle.localScale.x / 2, obstacle.position.y, obstacle.position.z - obstacle.localScale.z / 2);
                    Vector3 leftTop = new Vector3(obstacle.position.x - obstacle.localScale.x / 2, obstacle.position.y, obstacle.position.z + obstacle.localScale.z / 2);
                    Vector3 rightBottom = new Vector3(obstacle.position.x + obstacle.localScale.x / 2, obstacle.position.y, obstacle.position.z - obstacle.localScale.z / 2);
                    Vector3 rightTop = new Vector3(obstacle.position.x + obstacle.localScale.x / 2, obstacle.position.y, obstacle.position.z + obstacle.localScale.z / 2);

                    if (FloorMatrix[i, j].position.x >= leftBottom.x && FloorMatrix[i, j].position.x <= rightBottom.x && FloorMatrix[i, j].position.z <= leftTop.z && FloorMatrix[i, j].position.z >= leftBottom.z)
                    {
                        FloorMatrix[i, j].isIgnored = true;
                    }
                }
            }
        }
    }
}
