using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemBuyScript : MonoBehaviour
{
    public ShopData.ShopItem thisItem;
    public ClientScript client;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = thisItem.label;
        transform.GetChild(1).GetComponent<TMP_Text>().text = thisItem.price.ToString();
        transform.GetChild(2).GetComponent<TMP_Text>().text = thisItem.amount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyThisItem()
    {
        thisItem.amount -= 1;

        //client.SendBoughtItem(thisItem);
    }
}
