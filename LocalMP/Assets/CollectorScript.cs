using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CollectorScript : MonoBehaviour
{
    List<NetworkObject> networkObjects;
    public string JsonTransforms;

    // Start is called before the first frame update
    void Start()
    {
        networkObjects = GameObject.FindObjectsByType(typeof(NetworkObject), FindObjectsSortMode.None).OfType<NetworkObject>().ToList<NetworkObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectData()
    {
        List<NetworkTransform> netTransforms = new List<NetworkTransform>();

        foreach(NetworkObject netObj in networkObjects)
        {
            netTransforms.Add(netObj.GivePosition());
        }

        JsonTransforms = JsonUtility.ToJson(netTransforms);
    }
}
