using UnityEngine;
using UnityEngine.UI;
using System;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public GameObject store;
    public Text coins;

    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Debug.Log("Removing item...");
        Inventory.instance.Remove(item);
    }


    //Use or sell item
    public void UseItem()
    {
        if(item != null)
        {
            if (store.active)
            {

                long coin = Int64.Parse(coins.text);
                coin += item.price;
                coins.text = Convert.ToString(coin);

                Inventory.instance.Remove(item);
            }

            item.Use();
        }
    }
}
