using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour
{
    public bool canJump;

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("STAY");
        if (collision.gameObject.layer == 3)
        {
            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("EXIT");

        if (collision.gameObject.layer == 3)
        {
            canJump = false;
        }
    }
}
