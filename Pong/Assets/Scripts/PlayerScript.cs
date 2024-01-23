using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float MoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float input = Input.GetAxis("Vertical");
        rb.MovePosition(rb.position + Vector2.up * input * MoveSpeed * Time.deltaTime);
    }
}
