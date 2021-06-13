using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemies : MonoBehaviour
{
    [SerializeField]
    private GameObject monster;
    [SerializeField]
    private GameObject respawn;
    [SerializeField]
    private GameObject prefabMonster;
    [SerializeField]
    private int cooldown;

    private int tempViewId;

    private float timer;



    // Start is called before the first frame update
    void Start()
    {
        try
        {
            respawn = this.gameObject;
            monster = respawn.transform.GetChild(0).gameObject;
            tempViewId = monster.GetComponent<PhotonView>().photonView.viewID;
        }
        catch (Exception e)
        {

        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (monster == null)
        {
            timer += Time.deltaTime;
        }

        if(timer >= cooldown)
        {
            timer = 0;
            GameObject child = Instantiate(prefabMonster, this.gameObject.transform.position, Quaternion.identity);
            child.transform.parent = respawn.transform;
            child.GetComponent<PhotonView>().viewID = tempViewId;
            monster = respawn.transform.GetChild(0).gameObject;
        }
    }

    
       
       
  
}

