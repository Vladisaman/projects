using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager
{
    private static CheckpointManager instance;
    public Vector3 checkpointPos;

    public static CheckpointManager GetInstance()
    {
        if(instance == null)
        {
            instance = new CheckpointManager();
        }

        return instance;
    }
}