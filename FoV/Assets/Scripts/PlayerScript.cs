using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    [SerializeField] float MoveSpeed;
    private bool isCrouching;

    private void Awake()
    {
        isCrouching = false;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isCrouching = true;
            MoveSpeed = 1.5F;
        } else
        {
            isCrouching = false;
            MoveSpeed = 3.0F;
        }

        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * Time.deltaTime * MoveSpeed);
    }
    public bool isMoving()
    {
        if(!isCrouching && moveDirection != Vector2.zero)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
