using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] public GameObject object1;
    [SerializeField] public GameObject object2;

    public NetworkTransform SentObject;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ClientScript>().onObjectSpawn += CreateObjectAtLocation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateObject1();
        }

        if (Input.GetMouseButtonDown(1))
        {
            CreateObject2();
        }

        if(SentObject != null)
        {
            Vector3 location = new Vector3(SentObject.posX, SentObject.posY, 0);
            if(SentObject.name == "Object1")
            {
                Instantiate(object1, location, Quaternion.identity);
            } 
            else if (SentObject.name == "Object2")
            {
                Instantiate(object2, location, Quaternion.identity);
            }
            SentObject = null;
        }
    }

    public void CreateObject1()
    {
        Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        location.z = 0;
        GameObject temp = Instantiate(object1, location, Quaternion.identity);
        GetComponent<ClientScript>().SendMessage(JsonUtility.ToJson(new NetworkTransform { posX = temp.transform.position.x, posY = temp.transform.position.y, name = object1.name }));
    }

    public void CreateObject2()
    {
        Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        location.z = 0;
        GameObject temp = Instantiate(object2, location, Quaternion.identity);
        GetComponent<ClientScript>().SendMessage(JsonUtility.ToJson(new NetworkTransform { posX = temp.transform.position.x, posY = temp.transform.position.y, name = object2.name }));
    }

    public void CreateObjectAtLocation(NetworkTransform netTransform)
    {
        SentObject = netTransform;
    }
}

[Serializable]
public class NetworkTransform
{
    public string name;
    public float posX;
    public float posY;
}