using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ClientScript : MonoBehaviour
{
    TcpClient client;
    NetworkStream stream;
    int number;


    // Start is called before the first frame update
    void Start()
    {
        number = 0;
        ConnectToServer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectToServer()
    {
        try
        {
            client = new TcpClient();
            client.Connect(IPAddress.Parse("127.0.0.1"), 8080);
            stream = client.GetStream();
            Thread thread = new Thread(new ThreadStart(RecieveMessage));
            thread.Start();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void RecieveMessage()
    {
        byte[] message = new byte[4096];
        int bytesAmount;

        while (true)
        {
            bytesAmount = 0;
            try
            {
                bytesAmount = stream.Read(message, 0, message.Length);
                if (bytesAmount == 0)
                {
                    Debug.Log("No data");
                    break;
                }
            }
            catch
            {
                Debug.Log("Message error");
                break;
            }
            string stringMsg = Encoding.UTF8.GetString(message, 0, bytesAmount);

            //OnMessageRecieve.Invoke(stringMsg);
            Debug.Log("MESSAGE RECIEVED = " + stringMsg);
        }

        client.Close();
    }

    public void SendMessage(string messageString)
    {
        byte[] message = Encoding.UTF8.GetBytes(messageString);
        stream.Write(message, 0, message.Length);
        stream.Flush();
        stream = client.GetStream();
    }

    private void OnDestroy()
    {
        if (client != null)
        {
            client.Close();
        }
    }

    public void TestMessage()
    {
        SendMessage(number.ToString());
        number++;
    }
}
