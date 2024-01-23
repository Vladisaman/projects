using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;
    [SerializeField] GameObject WorldMap;
    [SerializeField] GameObject Minimap;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    bool isAllowedMoving;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            WorldMap.SetActive(!WorldMap.activeSelf);
            WorldMap.GetComponent<WorldMapScript>().TranslateMap(transform.position);
            Minimap.SetActive(!Minimap.activeSelf);
            isAllowedMoving = !isAllowedMoving;
        }
    }

    void Awake()
    {
        isAllowedMoving = true;
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isAllowedMoving)
        {
            // Update IsRunning from input.
            IsRunning = canRun && Input.GetKey(runningKey);

            // Get targetMovingSpeed.
            float targetMovingSpeed = IsRunning ? runSpeed : speed;
            if (speedOverrides.Count > 0)
            {
                targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
            }

            // Get targetVelocity from input.
            Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

            // Apply movement.
            rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
        }
    }
}