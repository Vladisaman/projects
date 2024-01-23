using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aagent : MonoBehaviour
{
    public Vector3 EndPoint;
    private Vector3 TargetPos;
    public float MoveSpeed;

    public bool isSomethingForward;
    public bool isSomethingRight;
    public bool isSomethingLeft;

    // Start is called before the first frame update
    void Start()
    {
        EndPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, 7.5F))
        {
            isSomethingForward = true;
        }
        else
        {
            isSomethingForward = false;
        }
        if (Physics.Raycast(transform.position, transform.right, 7.5F))
        {
            isSomethingRight = true;
        }
        else
        {
            isSomethingRight = false;
        }
        if (Physics.Raycast(transform.position, transform.right * -1, 7.5F))
        {
            isSomethingLeft = true;
        }
        else
        {
            isSomethingLeft = false;
        }

        if (isSomethingForward == false)
        {
            transform.position = Vector3.Lerp(transform.position, EndPoint, Time.deltaTime * MoveSpeed);
        }
    }

    private void ChangeTarget(Vector3 NewTarget)
    {
        TargetPos = NewTarget + new Vector3(0, 0.6F, 0);
    }
    public void SetEndPoint(Vector3 point)
    {
        EndPoint = point;
    }
}
