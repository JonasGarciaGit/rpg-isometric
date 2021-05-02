using UnityEngine.UI;
using UnityEngine;
using System;



public class StoreController : MonoBehaviour
{
    public Item[] items;
    public Text coins;

    public void onBuyItem(string name)
    {
        long coin = Int64.Parse(coins.text);

        if (Inventory.instance.items.Count < 20) {
            foreach (Item item in items)
            {
                if (item.name.Equals(name))
                {
                    if (coin >= item.price)
                    {
                        Inventory.instance.Add(item);
                        coin -= item.price;
                        coins.text = Convert.ToString(coin);
                    }
                }
            }
        }
    }

}
