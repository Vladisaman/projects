using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("GUID"));
        if(PlayerPrefs.HasKey("GUID"))
        {
            PlayerData.GetInstance().id = PlayerPrefs.GetString("GUID");
        } 
        else
        {
            PlayerData.GetInstance().id = Guid.NewGuid().ToString();
            PlayerPrefs.SetString("GUID", PlayerData.GetInstance().id);
            PlayerPrefs.Save();
        }

        SendRequest("get");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendRequest(string header)
    {
        StartCoroutine(MySQLManager.GetInstance().SendRequest(JsonUtility.ToJson(PlayerData.GetInstance()), header));
    }

    public void UpdateRequest()
    {
        PlayerData.GetInstance().coins += 15;
        SendRequest("update");
    }
}