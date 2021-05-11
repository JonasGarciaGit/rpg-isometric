using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDest : MonoBehaviour
{
    public int pivotPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("npc"))
        {

            if (pivotPoint == 3)
            {
                pivotPoint = 0;
            }

            if (pivotPoint == 2)
            {
                this.gameObject.transform.position = new Vector3(8.78f, 0.18f, 3.06f);
                pivotPoint = 3;
            }

            if (pivotPoint == 1)
            {
                this.gameObject.transform.position = new Vector3(1.9f, 0.18f, -1.36f);
                pivotPoint = 2;
            }


            if (pivotPoint == 0)
            {
                this.gameObject.transform.position = new Vector3(8.78f, 0.18f, -6.53f);
                pivotPoint = 1;
            }           
        }
    }

}
