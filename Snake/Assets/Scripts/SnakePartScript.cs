using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePartScript : MonoBehaviour
{
    public Vector3 MoveDir;
    public Vector3 TargetPos;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("WOLL"))
        {
            if (MoveDir == Vector3.left || MoveDir == Vector3.right)
            {
                transform.position = new Vector3(transform.position.x * -1, 0, transform.position.z);
            }
            else if (MoveDir == Vector3.forward || MoveDir == Vector3.back)
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z * -1);
            }
            TargetPos = transform.position;
        }
    }
}
