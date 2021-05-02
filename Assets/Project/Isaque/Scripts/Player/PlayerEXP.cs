using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerEXP : MonoBehaviour
{
  
    public int maxExp = 1000;

    public int currentExp;

    public GameObject levelUpFX;

    public event Action<float> OnExpPctChanged = delegate { };

    private void OnEnable()
    {
        currentExp = 0;
    }

    public void ModifyExp(int amount)
    {
        currentExp += amount;

        float currentExpPct = (float)currentExp / (float)maxExp;
        OnExpPctChanged(currentExpPct);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentExp >= maxExp)
        {
            //Controla a quantidade de exp e o level upado.
            GameObject levelUpParticle = Instantiate(levelUpFX,new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.30f, this.gameObject.transform.position.z),Quaternion.identity);
            levelUpParticle.transform.parent = this.gameObject.transform;
            Destroy(levelUpParticle, 5f);
            currentExp = currentExp - maxExp;
            maxExp = maxExp + 1000;
            float currentExpPct = (float)currentExp / (float)maxExp;
            OnExpPctChanged(currentExpPct);

            //Modifica status ao upar
            PlayerHP playerHP = GetComponentInParent<PlayerHP>();
            if(playerHP.maxHealth < 2000)
            {
                playerHP.maxHealth += 200;
            }

            playerHP.ModifyHealth(playerHP.maxHealth);
        }

    }
}
