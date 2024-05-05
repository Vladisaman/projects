using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Camera cam1;
    [SerializeField] Camera cam2;
    [SerializeField] Camera sharedCam;

    [SerializeField] Transform Player1;
    [SerializeField] Transform Player2;
    private bool isTogether;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0,0,10);
        isTogether = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Mathf.Abs(Player1.transform.position.x) - Mathf.Abs(Player2.transform.position.x)) <= 10 &&
            Mathf.Abs(Mathf.Abs(Player1.transform.position.y) - Mathf.Abs(Player2.transform.position.y)) <= 5f)
        {
            cam1.gameObject.SetActive(false);
            cam2.gameObject.SetActive(false);
            sharedCam.gameObject.SetActive(true);
            isTogether = true;
        }
        else
        {
            cam1.gameObject.SetActive(true);
            cam2.gameObject.SetActive(true);
            sharedCam.gameObject.SetActive(false);
            isTogether = false;
        }
    }

    private void FixedUpdate()
    {
        if (!isTogether)
        {
            cam1.transform.position = Player1.transform.position - offset;
            cam2.transform.position = Player2.transform.position - offset;
        } 
        else
        {
            sharedCam.transform.position = new Vector3((Player1.position.x + Player2.position.x) / 2, (Player1.position.y + Player2.position.y) / 2, -10);
        }
    }
}
