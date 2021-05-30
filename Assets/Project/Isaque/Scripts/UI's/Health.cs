using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField]
    public int maxHealth = 100;

    public int currentHealth;

    private PlayerEXP playerExp;

    private EnemyIA enemyIa;

    private BossIA bossIa;

    private WitchIA witchIa;

    private bool receiveExp = false;

    private bool weaponIsStay;

    public DealSomeDamage dealSomeDamage;

    public QuestSystem questSystem;

    private Animator playerAnimator;

    [SerializeField]
    private GameObject bloodPrefab;

    private float damageCooldown = 1f;

    public event Action<float> OnHealthPctChanged = delegate { };

    private void OnEnable()
    {
        currentHealth = maxHealth;
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

        if(playerAnimator != null)
        {
            if (playerAnimator.GetBool("isAttacking") == false && playerAnimator.GetBool("isAttackingHeavying") == false)
            {
                damageCooldown = 1f;
            }

        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Weapon")
        {
            dealSomeDamage = other.GetComponent<DealSomeDamage>();
            playerAnimator = other.GetComponentInParent<Animator>();
            

            if(playerAnimator.GetBool("isAttacking") == true || playerAnimator.GetBool("isAttackingHeavying") == true)
            {
                if(damageCooldown == 1f)
                {
                    ModifyHealth(-dealSomeDamage.weaponDamage);
                    GameObject blood = Instantiate(bloodPrefab, this.gameObject.transform.position, Quaternion.identity);
                    Destroy(blood, 1f);

                    if(!playerAnimator.GetBool("isAttackingHeavying"))
                    {
                        damageCooldown = 0f;
                    }
                    
                }

            }


            if(currentHealth <= 0)
            {
                if(receiveExp == false)
                {
                    playerExp = dealSomeDamage.GetComponentInParent<PlayerEXP>();
                    questSystem = dealSomeDamage.GetComponentInParent<QuestSystem>();

                    if (questSystem.isQuestForKill == true && questSystem.haveQuest == true)
                    {

                        if (questSystem.enemiesTag == this.gameObject.tag)
                        {

                            questSystem.countEnemiesDead = questSystem.countEnemiesDead - 1;


                            if (questSystem.countEnemiesDead <= 0)
                            {
                                questSystem.newQuestInstructionsUI.text = "Quest completed! Deliver the quest to receive the rewards.";
                            }
                            else
                            {
                                questSystem.newQuestInstructionsUI.text = questSystem.basicIntruction + " left - " + questSystem.countEnemiesDead + " Enemies";
                            }
                        }

                    }

                    try
                    {
                        enemyIa = GetComponentInParent<EnemyIA>();
                        bossIa = GetComponentInParent<BossIA>();
                        witchIa = GetComponentInParent<WitchIA>();
                    }
                    catch
                    {

                    }
                  
                    if(this.gameObject.tag == "StoneGolem")
                    {
                        playerExp.ModifyExp(bossIa.monsterExp);

                    }else if(this.gameObject.tag == "Witch")
                    {
                        playerExp.ModifyExp(witchIa.monsterExp);
                    }
                    else
                    {
                        playerExp.ModifyExp(enemyIa.monsterExp);
                    }

                    
                    receiveExp = true;
                    
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon")
        {
            weaponIsStay = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
        {
            weaponIsStay = false;
        }
    }

}
