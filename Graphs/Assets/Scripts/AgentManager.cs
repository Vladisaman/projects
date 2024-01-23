using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    //[SerializeField] Aagent aagent;

    [SerializeField] Transform Agent;
    [SerializeField] GameObject EndPoint;

    public static AgentManager instance;

    GraphMeshSurface GMS;


    // Start is called before the first frame update
    void Start()
    {
        GMS = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GraphMeshSurface>();

        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject Point = Instantiate(EndPoint, new Vector3(hit.point.x, 0.75F, hit.point.z), Quaternion.identity);
                Agent.GetComponent<Agent>().ChangePath(FindPath(Agent.transform.position, Point.transform.position));
            }
        }
    }

    public Path FindPath(Vector3 agentPos, Vector3 endPos)
    {
        bool isFirstLower = agentPos.z < endPos.z;
        bool isFirstLeft = agentPos.x < endPos.x;

        int iFirst = (int)Mathf.Ceil(Mathf.Abs((Mathf.Abs(agentPos.z) - Mathf.Abs(GraphMeshSurface.FloorMatrix[0, 0].position.z)) / GMS.MatrixSize));
        int iSecond = (int)Mathf.Ceil(Mathf.Abs((Mathf.Abs(endPos.z) - Mathf.Abs(GraphMeshSurface.FloorMatrix[0, 0].position.z)) / GMS.MatrixSize));
        int jFirst = (int)Mathf.Ceil(Mathf.Abs((Mathf.Abs(agentPos.x) - Mathf.Abs(GraphMeshSurface.FloorMatrix[0, 0].position.x)) / GMS.MatrixSize));
        int jSecond = (int)Mathf.Ceil(Mathf.Abs((Mathf.Abs(endPos.x) - Mathf.Abs(GraphMeshSurface.FloorMatrix[0, 0].position.z)) / GMS.MatrixSize));


        for (int i = 0; i <= GraphMeshSurface.FloorMatrix.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= GraphMeshSurface.FloorMatrix.GetUpperBound(1); j++)
            {
                if (Vector3.Distance(GraphMeshSurface.FloorMatrix[i, j].position, agentPos) <= Vector3.Distance(GraphMeshSurface.FloorMatrix[iFirst, jFirst].position, agentPos))
                {
                    iFirst = i;
                    jFirst = j;
                }

                if (Vector3.Distance(GraphMeshSurface.FloorMatrix[i, j].position, endPos) <= Vector3.Distance(GraphMeshSurface.FloorMatrix[iSecond, jSecond].position, endPos))
                {
                    iSecond = i;
                    jSecond = j;
                }
            }
        }


        Debug.Log("iFirst = " + iFirst + " jFirst = " + jFirst + " iSecond = " + iSecond + " jSecond = " + jSecond);

        for (int i = iFirst < iSecond ? iFirst : iSecond; i <= (iFirst < iSecond ? iSecond : iFirst); i++)
        {
            for (int j = jFirst < jSecond ? jFirst : jSecond; j <= (jFirst < jSecond ? jSecond : jFirst); j++)
            {
                int arrayIndex = (i + 1) * (j + 1) - 1;

                if (!GraphMeshSurface.FloorMatrix[i, j].isIgnored)
                {
                    if (isFirstLower)
                    {
                        if (i + 1 <= GraphMeshSurface.FloorMatrix.GetUpperBound(0))
                        {
                            GraphMeshSurface.graph.AddConnection(arrayIndex, (i + 2) * (j + 1) - 1, 2);
                        }
                    }
                    else
                    {
                        if (i - 1 >= 0)
                        {
                            GraphMeshSurface.graph.AddConnection(arrayIndex, i * (j + 1) - 1, 2);
                        }
                    }
                    if (isFirstLeft)
                    {
                        if (j + 1 <= GraphMeshSurface.FloorMatrix.GetUpperBound(1))
                        {
                            GraphMeshSurface.graph.AddConnection(arrayIndex, (i + 1) * (j + 2) - 1, 2);

                            if (isFirstLower && i + 1 <= GraphMeshSurface.FloorMatrix.GetUpperBound(0))
                            {
                                GraphMeshSurface.graph.AddConnection(arrayIndex, (i + 2) * (j + 2) - 1, 1);
                            }
                            else
                            if (!isFirstLower && i - 1 >= 0)
                            {
                                GraphMeshSurface.graph.AddConnection(arrayIndex, i * (j + 2) - 1, 1);
                            }
                        }
                    }
                    else
                    {
                        if (j - 1 >= 0)
                        {
                            GraphMeshSurface.graph.AddConnection(arrayIndex, (i + 1) * j - 1, 2);

                            if (isFirstLower && i + 1 <= GraphMeshSurface.FloorMatrix.GetUpperBound(0))
                            {
                                GraphMeshSurface.graph.AddConnection(arrayIndex, (i + 2) * j - 1, 1);
                            }
                            else
                            if (!isFirstLower && i - 1 >= 0)
                            {
                                GraphMeshSurface.graph.AddConnection(arrayIndex, i * j - 1, 1);
                            }
                        }
                    }
                }
            }
        }



        int startIndex = iFirst * 100 + jFirst;
        int endIndex = iSecond * 100 + jSecond;
        Path path = GraphMeshSurface.graph.FindShortestPath(GraphMeshSurface.FloorMatrix[iFirst, jFirst], GraphMeshSurface.FloorMatrix[iSecond, jSecond], startIndex < endIndex ? startIndex : endIndex, startIndex < endIndex ? endIndex : startIndex);
        GraphMeshSurface.graph.ClearConnections();
        Debug.Log(path.PathLength);

        return path;
    }
}
