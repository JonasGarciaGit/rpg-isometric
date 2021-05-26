using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickUpCoin : MonoBehaviour
{

    public TextMeshProUGUI[] playerTextsPro;
    public Text[] playerText;
    public string monsterType;
    private int coinValue = 0;
    TextMeshProUGUI coinTxt = null;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(monsterType != null)
        {
            if(coinValue == 0)
            {
                if (monsterType == "Orc")
                {
                    coinValue = 40;
                }
                else if (monsterType == "Witch")
                {
                    coinValue = 30;
                }
                else if (monsterType == "Ogre")
                {
                    coinValue = 50;
                }
                else if (monsterType == "StoneGolem")
                {
                    coinValue = 500;
                }
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerTextsPro = other.GetComponentsInChildren<TextMeshProUGUI>(true);
            playerText = other.GetComponentsInChildren<Text>(true);

            foreach (TextMeshProUGUI texto in playerTextsPro)
            {
                if(texto.name == "CoinTxT")
                {
                    coinTxt = texto;
                }
            }

            foreach (Text texto in playerText)
            {
                if(texto.name == "Coins")
                {
                    texto.text = (int.Parse(texto.text) + coinValue).ToString(); ;
                    coinTxt.text = coinValue.ToString() + " Coins!";
                    coinTxt.gameObject.SetActive(true);
                    coinTxt.enabled = true;
                    StartCoroutine("waitForSecondsFunc");

                }
               
            }

            this.gameObject.GetComponent<Renderer>().enabled = false;
            this.gameObject.GetComponent<SphereCollider>().enabled = false;

        }
    }

    IEnumerator waitForSecondsFunc()
    {
        yield return new WaitForSeconds(0.5f);
        coinTxt.enabled = false;
        coinTxt.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
