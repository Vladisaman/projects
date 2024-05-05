using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkObject : MonoBehaviour
{
    Vector2 storedPosition;
    [SerializeField] CollectorScript collector;

    // Start is called before the first frame update
    void Start()
    {
        storedPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(!storedPosition.Equals(transform.position))
        {
            collector.CollectData();
        }
    }

    public NetworkTransform GivePosition()
    {
        return new NetworkTransform { posX = transform.position.x, posY = transform.position.y, name = gameObject.name };
    }
}
