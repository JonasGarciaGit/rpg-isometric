using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public GameObject finalPoint;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, finalPoint.transform.position, speed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
