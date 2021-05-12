using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickUpCoin : MonoBehaviour
{

    public TextMeshProUGUI[] playerTextsPro;
    public Text[] playerText;
    TextMeshProUGUI coinTxt = null;
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
                    int value = Random.Range(10, 50);
                    texto.text = (int.Parse(texto.text) + value).ToString(); ;
                    coinTxt.text = value.ToString() + " Coins!";
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
