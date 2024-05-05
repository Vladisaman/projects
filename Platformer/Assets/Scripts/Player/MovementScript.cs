using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript
{
    private Rigidbody2D rb;
    private float speed;
    private float jumpStrength;

    public MovementScript(Rigidbody2D rb2d, float speed, float jump)
    {
        rb = rb2d;
        this.speed = speed;
        jumpStrength = jump;
    }

    public void Move(float axis)
    {
        rb.velocity = new Vector2(speed * axis, rb.velocity.y);

        if((rb.transform.localScale.x > 0 && axis < 0) || (rb.transform.localScale.x < 0 && axis > 0))
        {
            rb.transform.localScale = new Vector2(-1 * rb.transform.localScale.x, rb.transform.localScale.y);
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpStrength);
    }
}
