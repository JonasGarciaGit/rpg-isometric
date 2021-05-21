using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DealSomeDamage : MonoBehaviour
{
    [SerializeField]
    public int weaponDamage;
    public Transform myTransform;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if(player != null)
        {
            if (player.GetComponentInParent<PlayerMoviment>(true).characterClass.Equals("Thief"))
            {
                weaponDamage = Int32.Parse(player.GetComponentInParent<CharacterWindow>(true).attack.text) / 2;
            }
            else
            {
                weaponDamage = Int32.Parse(player.GetComponentInParent<CharacterWindow>(true).attack.text);
            }      
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if (player.GetComponentInParent<PlayerMoviment>(true).characterClass.Equals("Thief"))
            {
                weaponDamage = Int32.Parse(player.GetComponentInParent<CharacterWindow>(true).attack.text) / 2;
            }
            else
            {
                weaponDamage = Int32.Parse(player.GetComponentInParent<CharacterWindow>(true).attack.text);
            }          
        }
       
        myTransform = this.gameObject.transform;
    }
}
