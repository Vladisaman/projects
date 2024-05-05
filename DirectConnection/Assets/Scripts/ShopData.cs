using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopData
{
    public List<ShopItem> ShopItems;

    [System.Serializable]
    public class ShopItem
    {
        public string label;
        public int price;
        public int amount;

        public ShopItem(string label, int price, int amount)
        {
            this.label = label;
            this.price = price;
            this.amount = amount;
        }
    }
}
