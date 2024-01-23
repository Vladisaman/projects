using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    [SerializeField] Camera cam;
    [SerializeField] ElevatorScript eleScript;
    GameObject LastTarget;

    [SerializeField] Material ButtonMat1;
    [SerializeField] Material ButtonMat2;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();



    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5.0F))
        {
            if (LastTarget != null)
            {
                LastTarget.GetComponentInParent<MeshRenderer>().material = ButtonMat1;
                LastTarget.SetActive(false);
            }
            if (hit.transform.CompareTag("Button"))
            {
                hit.transform.GetComponent<MeshRenderer>().material = ButtonMat2;
                hit.transform.GetChild(0).gameObject.SetActive(true);
                LastTarget = hit.transform.GetChild(0).gameObject;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    eleScript.OpenDoors();
                }
            }
            else if (hit.transform.CompareTag("NextFloorButton") && SceneManager.GetActiveScene().buildIndex != 1)
            {
                hit.transform.GetComponent<MeshRenderer>().material = ButtonMat2;
                hit.transform.GetChild(0).gameObject.SetActive(true);
                LastTarget = hit.transform.GetChild(0).gameObject;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    eleScript.SecondFloor();
                }
            }
            else if (hit.transform.CompareTag("NextFloorButton") && SceneManager.GetActiveScene().buildIndex != 0)
            {
                hit.transform.GetComponent<MeshRenderer>().material = ButtonMat2;
                hit.transform.GetChild(0).gameObject.SetActive(true);
                LastTarget = hit.transform.GetChild(0).gameObject;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    eleScript.FirstFloor();
                }
            }
        }
    }

    void FixedUpdate()
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
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }
}