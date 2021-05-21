using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class environmentMusicController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip music;
    public AudioSource[] audioSourceList;
    public bool canPlay = false;

    private void Start()
    {
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.volume = 0.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && other.gameObject.GetComponent<PhotonView>().isMine)
        {
            foreach (var audioSrc in audioSourceList)
            {
                if (!audioSrc.name.Equals(this.gameObject.name))
                {
                    audioSrc.Stop();
                    audioSrc.GetComponentInParent<environmentMusicController>().canPlay = false;
                }
            }

            if (!audioSource.isPlaying && other.gameObject.GetComponent<PhotonView>().isMine)
            {
                audioSource.Play();
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player") && other.gameObject.GetComponent<PhotonView>().isMine)
        {
            foreach (var audioSrc in audioSourceList)
            {
                audioSrc.GetComponentInParent<environmentMusicController>().canPlay = true;
            }

            if (audioSource.isPlaying && other.gameObject.GetComponent<PhotonView>().isMine)
            {
                audioSource.Stop();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (canPlay && other.tag.Equals("Player") && other.gameObject.GetComponent<PhotonView>().isMine)
        {
            Debug.Log("Estou dentro");
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

        }
    }


}
