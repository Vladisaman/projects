using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float speed;
    private float angle;
    private Vector3 Direction;
    private void Start()
    {
        angle = Mathf.Deg2Rad * transform.eulerAngles.z;
        Direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
        Destroy(gameObject, 5.0f);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + Direction, Time.deltaTime * speed);
        //transform.Translate(Direction * Time.deltaTime * speed);
    }
}
