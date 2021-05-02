using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealSomeDamage : MonoBehaviour
{
    [SerializeField]
    public int weaponDamage;
    public Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myTransform = this.gameObject.transform;
    }
}
