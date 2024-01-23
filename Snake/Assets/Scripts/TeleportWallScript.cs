using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWallScript : MonoBehaviour
{
    [SerializeField] WallDir Dir;
    public static bool isTeleportingAllowed;
    public static bool wasHead;
    private void Start()
    {
        wasHead = false;
        isTeleportingAllowed = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && SnakeScript.Instance.getMoving() == false && isTeleportingAllowed)
        {
            if (other.GetComponent<SnakeScript>())
            {
                wasHead = true;
            } else
            {
                wasHead = false;
            }

            isTeleportingAllowed = false;

        }
    }
}

public enum WallDir
{
    LEFT,
    RIGHT,
    UP,
    DOWN,
}
