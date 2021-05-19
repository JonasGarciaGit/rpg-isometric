using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCloud : MonoBehaviour
{
    public bool canSpawn = true;
    public GameObject finalPoint;
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;

    private float timeToSpawn = 0f;
    public float cooldown = 300f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (canSpawn)
        {
            int chooseCloud = Random.Range(0, 4);

            if (chooseCloud == 1)
            {
                GameObject clound = Instantiate(prefab1, this.transform.position, Quaternion.identity);
                clound.GetComponent<CloudMove>().finalPoint = finalPoint;
                canSpawn = false;
            }

            if (chooseCloud == 2)
            {
                GameObject clound = Instantiate(prefab2, this.transform.position, Quaternion.identity);
                clound.GetComponent<CloudMove>().finalPoint = finalPoint;
                canSpawn = false;
            }

            if (chooseCloud == 3)
            {
                GameObject clound = Instantiate(prefab3, this.transform.position, Quaternion.identity);
                clound.GetComponent<CloudMove>().finalPoint = finalPoint;
                canSpawn = false;
            }

        }


        if (canSpawn == false)
        {
            timeToSpawn += 1;
            if (timeToSpawn >= cooldown)
            {
                canSpawn = true;
                timeToSpawn = 0f;
            }
        }

    }
}
