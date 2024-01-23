using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] public Transform[] PatrolPoints;
    public static GameManager instance;
    public Action CheckPing;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}