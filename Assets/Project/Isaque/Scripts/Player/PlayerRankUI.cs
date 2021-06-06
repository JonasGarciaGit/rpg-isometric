using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerRankUI : MonoBehaviour
{
    [SerializeField]
    private Image foregroundImage;
    [SerializeField]
    private float updateSpeedSeconds = 0.5f;
    [SerializeField]
    private PlayerRank playerRank;

    private void Awake()
    {
        GetComponentInParent<PlayerRank>().OnExpRankPctChanged += HandleExpChanged;
    }


    private void Start()
    {

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
}
