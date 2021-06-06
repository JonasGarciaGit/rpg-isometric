using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRank : MonoBehaviour
{
    public int expRank = 1000;

    public int currentRankExp;

    public Text rankInformation;

    public event Action<float> OnExpRankPctChanged = delegate { };


    private void OnEnable()
    {
        currentRankExp = 0;
        rankInformation.text = "Rank F";
    }

    public void ModifyRankExp(int amount)
    {
        currentRankExp += amount;

        float currentExpPct = (float)currentRankExp / (float)expRank;
        OnExpRankPctChanged(currentExpPct);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentRankExp >= expRank)
        {
            //Controla a quantidade de exp e o level upado.
            currentRankExp = currentRankExp - expRank;
            expRank = expRank + 1000;
            float currentExpPct = (float)currentRankExp / (float)expRank;
            OnExpRankPctChanged(currentExpPct);

            if (rankInformation.text.Equals("Rank F"))
            {
                rankInformation.text = "Rank E";

            }
            else if (rankInformation.text.Equals("Rank E"))
            {
                rankInformation.text = "Rank D";

            }
            else if (rankInformation.text.Equals("Rank D"))
            {

                rankInformation.text = "Rank C";

            }
            else if (rankInformation.text.Equals("Rank C"))
            {

                rankInformation.text = "Rank B";

            }
            else if (rankInformation.text.Equals("Rank B"))
            {

                rankInformation.text = "Rank A";

            }
            else if (rankInformation.text.Equals("Rank A"))
            {
                rankInformation.text = "Rank S";

            }
            else if (rankInformation.text.Equals("Rank S"))
            {

                rankInformation.text = "Rank SS";

            }
            else if (rankInformation.text.Equals("Rank SS"))
            {
                rankInformation.text = "Rank SS";
            }
        }
    }
}
