using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSounds : MonoBehaviour
{
    public AudioClip attackingAudio;
    public AudioSource audioSource;
    public AudioClip deathAudio;


    public void Attack()
    {
        audioSource.volume = 0.5f;
        audioSource.clip = attackingAudio;
        audioSource.Play();
    }

    public void DieWitcher()
    {
        audioSource.volume = 0.5f;
        audioSource.clip = deathAudio;
        audioSource.Play();
    }
}
