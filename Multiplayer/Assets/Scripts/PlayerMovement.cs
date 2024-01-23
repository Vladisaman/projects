using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    private Direction dir;
    [SerializeField] private float Speed;
    private const float coefficient = 0.02F;


    private void Start()
    {
        if (IsHost)
        {
            transform.position = new Vector3(-6.5f, 0, 0);
        }
        else
        {
            transform.position = new Vector3(6.5f, 0, 0);
            if (IsOwner)
            {
                GetComponent<GameManager>().SpawnBallServerRpc();
            }
        }
    }

    private void Update()
    {
        if (IsOwner)
        {
            if (Input.GetKey(KeyCode.W))
            {

                dir = Direction.Up;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir = Direction.Down;
            }
            else
            {
                dir = Direction.None;
            }
        }
    }

    private void FixedUpdate()
    {
        switch (dir)
        {
            case Direction.Up:
                HandleMovement(Speed * coefficient);
                break;
            case Direction.Down:
                HandleMovement(-Speed * coefficient);
                break;
            default:
                break;
        }
    }

    private void HandleMovement(float delta)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + delta);
    }
}

public enum Direction
{
    None,
    Up,
    Down,
}