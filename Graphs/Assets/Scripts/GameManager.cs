//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class GameManager : MonoBehaviour
//{
//    [SerializeField] Vertex[] vertices;
//    [SerializeField] GameObject AgentPrefab;
//    [SerializeField] GameObject DistancePanel;
//    [SerializeField] TMP_InputField inputField;
//    [SerializeField] Material ArrowMat;
//    [SerializeField] GameObject DistText;
//    public Graph graph;
//    public Vertex CurrVertex;
//    public Vertex CurrVertex2;

//    private void Awake()
//    {
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        graph = new Graph(vertices.Length);
//        for (int i = 0; i < vertices.Length; i++)
//        {
//            graph.AddVertex(vertices[i]);
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetMouseButtonDown(1))
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            if (Physics.Raycast(ray, out RaycastHit hit))
//            {
//                if (hit.transform.CompareTag("Vertex"))
//                {
//                    Instantiate(AgentPrefab, hit.transform.position + new Vector3(0, 0.6F, 0), Quaternion.identity).GetComponent<Agent>().ChangePath(graph.FindShortestPath(CurrVertex, vertices[0]));
//                }
//            }
//        }

//        if (Input.GetMouseButtonDown(0))
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            if (Physics.Raycast(ray, out RaycastHit hit))
//            {
//                if (hit.transform.CompareTag("Vertex"))
//                {
//                    if (!CurrVertex)
//                    {
//                        CurrVertex = hit.transform.GetComponent<Vertex>();
//                    }
//                    else
//                    {
//                        CurrVertex2 = hit.transform.GetComponent<Vertex>();
//                        DistancePanel.SetActive(true);
//                    }
//                }
//            }
//        }
//    }

//    public void DistanceConfirm()
//    {
//        if (inputField.text.Length > 0)
//        {
//            graph.AddConnection(CurrVertex, CurrVertex2, int.Parse(inputField.text));
//            LineRenderer Render = null;
//            if (!CurrVertex.GetComponent<LineRenderer>())
//            {
//                Render = CurrVertex.gameObject.AddComponent<LineRenderer>();
//            }
//            else
//            {
//                Render = CurrVertex.gameObject.GetComponent<LineRenderer>();
//            }
//            Render.startWidth = 0.5F;
//            Render.endWidth = 0.5F;
//            Render.material = ArrowMat;

//            if (CurrVertex.LastIndex > 1)
//            {
//                Render.positionCount += 2;
//            }
//            Render.SetPosition(CurrVertex.LastIndex++, CurrVertex.transform.position);
//            Render.SetPosition(CurrVertex.LastIndex++, CurrVertex2.transform.position);

//            Instantiate(DistText, new Vector3((CurrVertex.transform.position.x + CurrVertex2.transform.position.x) / 2, 2, (CurrVertex.transform.position.z + CurrVertex2.transform.position.z) / 2), Quaternion.identity).GetComponentInChildren<TMP_Text>().text = graph.GetVertexDistance(CurrVertex, CurrVertex2).ToString();

//            CurrVertex2 = null;
//            CurrVertex = null;
//        }
//    }
//}