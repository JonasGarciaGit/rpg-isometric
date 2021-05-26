using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public GameObject store;
    public Text coins;
    private bool canUseAgain = true;


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

            
            if (item != null)
            {
                //Selling items
                if (store.active)
                    {

                        long coin = Int64.Parse(coins.text);
                        coin += item.price;
                        coins.text = Convert.ToString(coin);

                        Inventory.instance.Remove(item);
                        return;
                    }

            //Using items
            if (item.category.Equals("potion")) {

                if (item.name.Equals("HpPotion"))
                {
                    int maxHealth = GetComponentInParent<PlayerHP>().maxHealth;
                    int currentHealth = GetComponentInParent<PlayerHP>().currentHealth;
                    double heal = maxHealth * 0.25;

                    GetComponentInParent<PlayerHP>().ModifyHealth(Mathf.RoundToInt((float) heal));
                    if ( currentHealth > maxHealth)
                    {
                        currentHealth = maxHealth;
                    }

                    Inventory.instance.Remove(item);

                }else if (item.name.Equals("SpeedPotion"))
                {
                    Inventory.instance.Remove(item);
                    StartCoroutine("speedPotion");
                    
                }
                else if (item.name.Equals("StrongPotion"))
                {
                    if (canUseAgain)
                    {
                        Inventory.instance.Remove(item);
                        StartCoroutine("strongPotion");
                    }
                   
                    
                }

                return;
            }

            if (item.category.Equals("helm"))
            {
                if (item.name.Equals("GodHelm"))
                {
                    GetComponentInParent<CharacterWindow>().addHelm(item);
                }
                else if (item.name.Equals("IronHelmet"))
                {
                    GetComponentInParent<CharacterWindow>().addHelm(item);
                }
                else if (item.name.Equals("LeatherHelmet"))
                {
                    GetComponentInParent<CharacterWindow>().addHelm(item);
                }

                return;
            }

            if (item.category.Equals("sword"))
            {
                if (item.name.Equals("GodSword"))
                {
                    GetComponentInParent<CharacterWindow>().addWeapon(item);
                }
                else if (item.name.Equals("IronSword"))
                {
                    GetComponentInParent<CharacterWindow>().addWeapon(item);
                }
                else if (item.name.Equals("WoodenSword"))
                {
                    GetComponentInParent<CharacterWindow>().addWeapon(item);
                }

                return;
            }

            if (item.category.Equals("armor"))
            {
                if (item.name.Equals("IronArmor"))
                {
                    GetComponentInParent<CharacterWindow>().addArmor(item);
                }
                else if (item.name.Equals("LeatherArmor"))
                {
                    GetComponentInParent<CharacterWindow>().addArmor(item);
                }
                else if (item.name.Equals("WoodenArmor"))
                {
                    GetComponentInParent<CharacterWindow>().addArmor(item);
                }
            }

        }

    }


    IEnumerator speedPotion()
    {
        float moveSpeed = GetComponentInParent<PlayerMoviment>().moveSpeed;
        GetComponentInParent<PlayerMoviment>().moveSpeed = 6;
        yield return new WaitForSeconds(5f);
        GetComponentInParent<PlayerMoviment>().moveSpeed = moveSpeed;
    }

    IEnumerator strongPotion()
    {

        GameObject player = GetComponentInParent<PlayerMoviment>().gameObject;

        DealSomeDamage dealSomeDamage = player.GetComponentInChildren<DealSomeDamage>(true);

        int damage = dealSomeDamage.weaponDamage;
        dealSomeDamage.weaponDamage = damage + 25;
        canUseAgain = false;
        yield return new WaitForSeconds(20f);
        dealSomeDamage.weaponDamage = damage;
        canUseAgain = true;
    }
     
}
