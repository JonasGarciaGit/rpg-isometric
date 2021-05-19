using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class environmentMusicController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip music;

    private void Start()
    {
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.volume = 0.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (!audioSource.isPlaying && other.gameObject.GetComponent<PhotonView>().isMine)
            {
                audioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (audioSource.isPlaying && other.gameObject.GetComponent<PhotonView>().isMine)
            {
                audioSource.Stop();
            }
        }
    }

}
