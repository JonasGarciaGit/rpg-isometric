using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHP : MonoBehaviour
{
    public int maxHealth = 100;

    public int currentHealth;

    public bool weaponIsStay;

    public DealSomeDamage dealSomeDamage;

    private Animator monsterAnimator;

    [SerializeField]
    private GameObject bloodPrefab;

    public event Action<float> OnHealthPctChanged = delegate { };

    private void OnEnable()
    {
        currentHealth = 100;
    }

    public void ModifyHealth(int amount)
    {
        currentHealth += amount;

        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyWeapon")
        {
            
            dealSomeDamage = other.GetComponent<DealSomeDamage>();
            monsterAnimator = other.GetComponentInParent<Animator>();

            if (dealSomeDamage.weaponDamage != 0)
            {
            if (monsterAnimator.GetBool("isAttkOne") == true || monsterAnimator.GetBool("isAttkTwo") == true)
            {
                ModifyHealth(-dealSomeDamage.weaponDamage);
                GameObject blood = Instantiate(bloodPrefab, this.gameObject.transform.position, Quaternion.identity);
                 Destroy(blood, 1f);
                }

            }
           
        }
        if(other.tag == "Fireball")
        {
            dealSomeDamage = other.GetComponent<DealSomeDamage>();

            ModifyHealth(-dealSomeDamage.weaponDamage);
            GameObject blood = Instantiate(bloodPrefab, this.gameObject.transform.position, Quaternion.identity);
            Destroy(blood, 1f);
            
        }

    }

    /*
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "EnemyWeapon")
        {
            weaponIsStay = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EnemyWeapon")
        {
            weaponIsStay = false;
        }
    }
    */
}
