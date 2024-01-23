using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{
    [SerializeField] GameObject SnakePartPrefab;
    List<SnakePartScript> SnakeParts;

    public float MoveSpeed;
    public float DelayTimer;
    public float DelayMin;

    private bool isAllowedMoving;

    public static SnakeScript Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        isAllowedMoving = false;
        SnakeParts = new List<SnakePartScript>();
        SnakeParts.Add(GetComponent<SnakePartScript>());
        SnakeParts[0].MoveDir = Vector3.forward;
        Enlarge();
        StartCoroutine(Movement());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SnakeParts[0].MoveDir = Vector3.forward;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SnakeParts[0].MoveDir = Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            SnakeParts[0].MoveDir = Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SnakeParts[0].MoveDir = Vector3.back;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Enlarge();
        }

        if (isAllowedMoving)
        {
            for (int i = 0; i <= SnakeParts.Count - 1; i++)
            {
                SnakeParts[i].transform.position = Vector3.Lerp(SnakeParts[i].transform.position, SnakeParts[i].TargetPos, Time.deltaTime * MoveSpeed);
            }
        }
    }

    void Enlarge()
    {
        GameObject Part = Instantiate(SnakePartPrefab, SnakeParts[SnakeParts.Count - 1].TargetPos + SnakeParts[SnakeParts.Count - 1].MoveDir * -1, Quaternion.identity);
        Part.GetComponent<SnakePartScript>().MoveDir = SnakeParts[SnakeParts.Count - 1].MoveDir;
        Part.GetComponent<SnakePartScript>().TargetPos = Part.transform.position;
        SnakeParts.Add(Part.GetComponent<SnakePartScript>());

        if(DelayTimer > DelayMin)
        {
            MoveSpeed += 1;
            DelayTimer -= 0.1F;
        }
    }

    IEnumerator Movement()
    {
        while (true)
        {
            yield return new WaitForSeconds(DelayTimer);
            for (int i = SnakeParts.Count - 1; i >= 1; i--)
            {
                SnakeParts[i].TargetPos = new Vector3(SnakeParts[i - 1].TargetPos.x, SnakeParts[i - 1].TargetPos.y, SnakeParts[i - 1].TargetPos.z);

                SnakeParts[i].MoveDir = SnakeParts[i - 1].MoveDir;
            }
            SnakeParts[0].TargetPos = SnakeParts[0].MoveDir + transform.position;
            isAllowedMoving = true;
            yield return new WaitForSeconds(DelayTimer);
            isAllowedMoving = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("APPL"))
        {
            Enlarge();
            Destroy(other.gameObject);
        }
    }
    public bool getMoving()
    {
        return isAllowedMoving;
    }
}
