using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class MySQLManager
{
    public const string serverAddress = "http://localhost:8080/post/";

    private static MySQLManager instance;

    public static MySQLManager GetInstance()
    {
        if(instance == null)
        {
            instance = new MySQLManager();
        }
        return instance;
    }



    public IEnumerator SendRequest(string body, string header)
    {
        UnityWebRequest request = UnityWebRequest.Post(serverAddress, body, "application/json");
        request.SetRequestHeader("type", header);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Success");
            Debug.Log(request.downloadHandler.text);

            if(request.GetResponseHeader("type") == "parse")
            {
                PlayerData data = JsonUtility.FromJson<PlayerData>(request.downloadHandler.text);
                PlayerData.GetInstance().coins = data.coins;
                PlayerData.GetInstance().health = data.health;
            }
        }
        else
        {
            Debug.Log("Unsuccess");
            Debug.Log(request.result + " " + request.responseCode);
        }
    }
}
