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

    private void OnEnable()
    {
        currentHealth = 100;
    }

    private void Start()
    {
        respawnPoint = GameObject.Find("RespawnInicial");
        blackFade.canvasRenderer.SetAlpha(0.0f);
        defense = float.Parse(gameObject.GetComponentInParent<CharacterWindow>(true).defense.text);
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

                    double realDamage = dealSomeDamage.weaponDamage - (defense * 0.1);
                    Debug.Log(realDamage);
                    Debug.Log(Mathf.RoundToInt((float)realDamage));
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
            Debug.Log(realDamage);
            Debug.Log(Mathf.RoundToInt((float)realDamage));
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
