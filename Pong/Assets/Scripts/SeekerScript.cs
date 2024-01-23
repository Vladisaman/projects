using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerScript : MonoBehaviour
{
    public static GameManager gm;

    // Start is called before the first frame update
    void Awake()
    {
        gm = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
