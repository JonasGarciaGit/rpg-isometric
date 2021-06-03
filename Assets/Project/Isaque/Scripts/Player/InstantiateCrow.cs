using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCrow : MonoBehaviour
{
    public GameObject crowPrefab;
    public GameObject player;
    GameObject crow;
    public bool isCrowActive = false;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
       if(isCrowActive == false)
        {
            Vector3 newPosition = new Vector3(player.transform.position.x, 2f, player.transform.position.z);
            crow = Instantiate(crowPrefab,newPosition, Quaternion.identity);
            crow.GetComponent<CrowIA>().questSystem = this.gameObject.GetComponentInParent<QuestSystem>();
            isCrowActive = true;
        }

       if (!crow.GetComponent<CrowIA>().player)
        {
            crow.GetComponent<CrowIA>().player = player;
        }
    }
}
