using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BallScript : NetworkBehaviour
{
    [SerializeField] GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Player").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsHost && IsOwner)
        {
            if (other.CompareTag("GoalLeft"))
            {
                gameManager.IncreaseGoalCountServerRpc(false);
            }
            if (other.CompareTag("GoalRight"))
            {
                gameManager.IncreaseGoalCountServerRpc(true);
            }
        }
    }
}
