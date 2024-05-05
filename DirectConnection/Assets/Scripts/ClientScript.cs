using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TMPro;

public class ClientScript : MonoBehaviour
{
    TcpClient client;
    NetworkStream stream;
    [SerializeField] GameObject ShopItemPrefab;
    [SerializeField] GameObject ShopPanel;
    public delegate void GenerateShopDelegate(string msg);
    public event GenerateShopDelegate OnMessageRecieve;
    public ShopData currShopData;
    private bool requireUpdating;
    [SerializeField] TMP_InputField inputField;


    // Start is called before the first frame update
    void Start()
    {
        ConnectToServer();
        //OnMessageRecieve += ParseShopData;
    }

    // Update is called once per frame
    void Update()
    {
        //if (requireUpdating)
        //{
        //    GenerateShop();
        //}
    }

    public void ConnectToServer()
    {
        try
        {
            client = new TcpClient();
            client.Connect(IPAddress.Parse("127.0.0.1"), 1234);
            stream = client.GetStream();
            Thread thread = new Thread(new ThreadStart(RecieveMessage));
            thread.Start();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void SendMessage(string messageString)
    {
        byte[] message = Encoding.UTF8.GetBytes(messageString);
        stream.Write(message, 0, message.Length);
        stream.Flush();
        stream = client.GetStream();
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

    //private void ParseShopData(string msg)
    //{
    //    currShopData = JsonUtility.FromJson<ShopData>(msg);
    //    requireUpdating = true;
    //}

    //private void GenerateShop()
    //{
    //    foreach (Transform transform in ShopPanel.transform)
    //    {
    //        Destroy(transform.gameObject);
    //    }

    //    for (int i = 0; i < currShopData.ShopItems.Count; i++)
    //    {
    //        GameObject item = Instantiate(ShopItemPrefab, ShopPanel.transform);
    //        item.GetComponent<ItemBuyScript>().thisItem = currShopData.ShopItems[i];
    //        item.GetComponent<ItemBuyScript>().client = this;
    //    }

    //    requireUpdating = false;
    //}

    //public void SendBoughtItem(ShopData.ShopItem item)
    //{
    //    SendMessage("shop|" + JsonUtility.ToJson(item));
    //}

    private void OnDestroy()
    {
        if (client != null)
        {
            client.Close();
        }
    }

    public void CreateRoom()
    {
        try
        {
            int.Parse(inputField.text);
            if (inputField.text != "")
            { 
                SendMessage("create|" + inputField.text);
            }
        } 
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    public void ConnectToRoom()
    {
        try
        {
            int.Parse(inputField.text);
            if (inputField.text != "")
            {
                SendMessage("join|" + inputField.text);


                RoomShareInfo("BABUBE");
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void RoomShareInfo(string info)
    {
        try
        {
            SendMessage("share|" + info);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}