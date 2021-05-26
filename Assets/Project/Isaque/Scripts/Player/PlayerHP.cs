using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public int maxHealth = 100;

    public int currentHealth;

    public bool weaponIsStay;

    public DealSomeDamage dealSomeDamage;

    private Animator monsterAnimator;

    [SerializeField]
    private GameObject bloodPrefab;

    private GameObject respawnPoint;

    public event Action<float> OnHealthPctChanged = delegate { };

    public Image blackFade;

    private float defense;

    public Text characterHpInfo;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        characterHpInfo.text = maxHealth.ToString(); 
    }

    private void Start()
    {
        respawnPoint = GameObject.Find("RespawnInicial");
        blackFade.canvasRenderer.SetAlpha(0.0f);
    }

    public void ModifyHealth(int amount)
    {
        currentHealth += amount;

        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);
    }


    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0 && !gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            StartCoroutine("playerDying");
        }
        if (gameObject.GetComponentInParent<CharacterWindow>(true).defense != null)
        {
            defense = float.Parse(gameObject.GetComponentInParent<CharacterWindow>(true).defense.text);
        }

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }


        characterHpInfo.text = maxHealth.ToString();

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
                    if (currentHealth <= 0)
                    {
                        currentHealth = 0;
                        return;
                    }
                
                    double realDamage = dealSomeDamage.weaponDamage - (defense * 0.3);
                   
                    if(realDamage <= 0)
                    {
                        realDamage = 0;
                    }

                    ModifyHealth(-Mathf.RoundToInt((float)realDamage));
                    GameObject blood = Instantiate(bloodPrefab, this.gameObject.transform.position, Quaternion.identity);
                    Destroy(blood, 1f);
                }

            }

        }
        if (other.tag == "Fireball")
        {
            dealSomeDamage = other.GetComponent<DealSomeDamage>();

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                return;
            }

            double realDamage = dealSomeDamage.weaponDamage - (defense * 0.1);
            ModifyHealth(-Mathf.RoundToInt((float)realDamage));
            GameObject blood = Instantiate(bloodPrefab, this.gameObject.transform.position, Quaternion.identity);
            Destroy(blood, 1f);

        }

    }

    IEnumerator playerDying()
    {
        gameObject.GetComponent<Animator>().Play("Death");
        gameObject.GetComponent<PlayerMoviment>().moveSpeed = 0f;
        FadeInImage();
        yield return new WaitForSeconds(4f);
        FadeOutImage();
        gameObject.GetComponent<PlayerMoviment>().moveSpeed = 4f;
        gameObject.GetComponent<Animator>().Play("Idle");
        gameObject.transform.position = respawnPoint.transform.position;
        ModifyHealth(maxHealth);
    }

    public void FadeInImage()
    {
        blackFade.CrossFadeAlpha(1f, 2f, false);
    }

    public void FadeOutImage()
    {
        blackFade.CrossFadeAlpha(0.0f, 10f, false);
    }

}
