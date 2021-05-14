using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{

    public AudioSource audioSource;

    public AudioClip runningSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void footStepSound()
    {
        audioSource.loop = true;
        audioSource.volume = 0.3f;
        audioSource.clip = runningSound;

        audioSource.Play();
    }
}
