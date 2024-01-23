using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private Path currPath;
    private Vector3 TargetPos;
    public float MoveSpeed;
    private int currIndex;

    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        currIndex = 0;
        TargetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Physics.Raycast(transform.position, transform.forward, out hit, 1.5F))
        //{
        //    if(hit.transform.gameObject.layer == 3)
        //    {
        //        Vector3 leftBottom = new Vector3(hit.transform.position.x - hit.transform.localScale.x / 2, hit.transform.position.y, hit.transform.position.z - hit.transform.localScale.z / 2);
        //        Vector3 leftTop = new Vector3(hit.transform.position.x - hit.transform.localScale.x / 2, hit.transform.position.y, hit.transform.position.z + hit.transform.localScale.z / 2);
        //        Vector3 rightBottom = new Vector3(hit.transform.position.x + hit.transform.localScale.x / 2, hit.transform.position.y, hit.transform.position.z - hit.transform.localScale.z / 2);
        //        Vector3 rightTop = new Vector3(hit.transform.position.x + hit.transform.localScale.x / 2, hit.transform.position.y, hit.transform.position.z + hit.transform.localScale.z / 2);

        //        ChangePath(AgentManager.instance.BuildPath(transform.position, currPath.PathPoints[currPath.PathPoints.Count-1].position, leftBottom, leftTop, rightBottom, rightTop));

        //        hit.transform.gameObject.layer = 0;

        //        //TODO: 
        //        //Вычислять все кубики графа в пределах стены, разрушая всякую с ними связь
        //        //Давать графу новую информацию учитывая ограничения стены
        //        //Перестраивать путь
        //        //Идти по новопроложенному пути
        //    }
        //}

        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * MoveSpeed);

        if (currPath != null && currIndex < currPath.PathPoints.Count - 1)
        {
            if (Vector3.Distance(transform.position, TargetPos) <= 0.35F)
            {
                ChangeTarget(currPath.PathPoints[++currIndex].position);
            }
        }
        
    }
    private void ChangeTarget(Vector3 NewTarget)
    {
        TargetPos = NewTarget;
        transform.LookAt(TargetPos);
    }
    public void ChangePath(Path path)
    {
        currPath = path;
    }
}
