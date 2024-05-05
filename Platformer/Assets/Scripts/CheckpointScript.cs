using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private bool wasUsed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!wasUsed & collision.CompareTag("Player"))
        {
            CheckpointManager.GetInstance().checkpointPos = transform.position;
            wasUsed = true;
            GetComponent<SpriteRenderer>().color = Color.black;
        }
    }
}
