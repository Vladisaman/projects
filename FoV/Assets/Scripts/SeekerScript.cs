using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerScript : MonoBehaviour
{
    public static PlayerScript player;

    private void Awake()
    {
        player = GetComponent<PlayerScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
