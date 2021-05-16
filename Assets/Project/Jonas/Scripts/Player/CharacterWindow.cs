using UnityEngine;
using UnityEngine.UI;
using System;

public class CharacterWindow : MonoBehaviour
{

    [SerializeField]
    private GameObject characterWindow;

    public Image helm;
    private Item helmItem;

    public Image armor;
    private Item armorItem;

    public Image weapon;
    private Item weaponItem;

    public Text defense;
    public Text attack;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            openOrCloseCharacterWindow();
        }

    }

    private void openOrCloseCharacterWindow()
    {
        characterWindow.SetActive(!characterWindow.activeSelf);
    }

    public void addHelm(Item item)
    {
        if(helmItem == null)
        {
            helm.gameObject.SetActive(true);
            helm.sprite = item.icon;
            helmItem = item;
            defense.text = Convert.ToString(item.defense + Int16.Parse(defense.text));
            Inventory.instance.Remove(item);
        }
       
    }

    public void removeHelm()
    {
        if (helmItem != null)
        {
            helm.gameObject.SetActive(false);
            helm.sprite = null;
            defense.text = Convert.ToString(Int16.Parse(defense.text) - helmItem.defense);
            Inventory.instance.Add(helmItem);
            helmItem = null;
        }
    }

    public void addArmor(Item item)
    {
        if (armorItem == null)
        {
            armor.gameObject.SetActive(true);
            armor.sprite = item.icon;
            armorItem = item;
            defense.text = Convert.ToString(item.defense + Int16.Parse(defense.text));
            Inventory.instance.Remove(item);
        }

    }

    public void removeArmor()
    {
        if (armorItem != null)
        {
            armor.gameObject.SetActive(false);
            armor.sprite = null;
            defense.text = Convert.ToString(Int16.Parse(defense.text) - armorItem.defense);
            Inventory.instance.Add(armorItem);
            armorItem = null;
        }
    }


    public void addWeapon(Item item)
    {
        if (weaponItem == null)
        {
            weapon.gameObject.SetActive(true);
            weapon.sprite = item.icon;
            weaponItem = item;
            attack.text = Convert.ToString(item.attack + Int16.Parse(attack.text));
            Inventory.instance.Remove(item);
        }

    }

    public void removeWeapon()
    {
        if (weaponItem != null)
        {
            weapon.gameObject.SetActive(false);
            weapon.sprite = null;
            attack.text = Convert.ToString(Int16.Parse(attack.text) - weaponItem.attack);
            Inventory.instance.Add(weaponItem);
            weaponItem = null;
        }
    }

}
