using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public bool isPlayerOwned;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayerOwned)
        {
            SeekerScript.gm.GoalEnemy();
        } else
        {
            SeekerScript.gm.GoalPlayer();
        }
    }
}
