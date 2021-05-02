using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEXPUI : MonoBehaviour
{
    [SerializeField]
    private Image foregroundImage;
    [SerializeField]
    private float updateSpeedSeconds = 0.5f;
    [SerializeField]
    private Text level;
    [SerializeField]
    private Font font;
    [SerializeField]
    private PlayerEXP playerExp;

    private void Awake()
    {
        GetComponentInParent<PlayerEXP>().OnExpPctChanged += HandleExpChanged;
    }


    private void Start()
    {
        level.font = font;
        level.fontStyle = FontStyle.Bold;
        level.fontSize = 14;
        level.text = 1.ToString();
    }

    private void HandleExpChanged(float pct)
    {
        StartCoroutine(ChangeToPct(pct));
    }

    private IEnumerator ChangeToPct(float pct)
    {
        float preChangePct = foregroundImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
            yield return null;

        }
        foregroundImage.fillAmount = pct;
    }

    private void Update()
    {
       if(playerExp.currentExp >= playerExp.maxExp)
        {
            level.text = (int.Parse(level.text) + 1).ToString();
        }
    }
}
