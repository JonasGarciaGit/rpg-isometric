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
        audioSource.volume = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && other.gameObject.GetComponent<PhotonView>().isMine)
        {
            foreach (var audioSrc in audioSourceList)
            {
                if (!audioSrc.name.Equals(this.gameObject.name))
                {
                    StartCoroutine("stopGradualPlayerFalse", audioSrc);
                }
            }

            if (!audioSource.isPlaying && other.gameObject.GetComponent<PhotonView>().isMine)
            {
                StartCoroutine("playGradual");
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player") && other.gameObject.GetComponent<PhotonView>().isMine)
        {
            foreach (var audioSrc in audioSourceList)
            {
                StartCoroutine("stopGradualPlayerTrue", audioSrc);
            }

            if (audioSource.isPlaying && other.gameObject.GetComponent<PhotonView>().isMine)
            {
                StartCoroutine("stopGradual");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (canPlay && other.tag.Equals("Player") && other.gameObject.GetComponent<PhotonView>().isMine)
        {
            if (!audioSource.isPlaying)
            {
                StartCoroutine("playGradual");
            }

        }
    }

    IEnumerator playGradual()
    {
        audioSource.Play();
        audioSource.volume = 0;
        yield return new WaitForSeconds(1f);
        audioSource.volume = 0.1f;
        yield return new WaitForSeconds(1f);
        audioSource.volume = 0.2f;
        yield return new WaitForSeconds(1f);
        audioSource.volume = 0.3f;
        yield return new WaitForSeconds(1f);
        audioSource.volume = 0.4f;
        yield return new WaitForSeconds(1f);
        audioSource.volume = 0.5f;

    }

    IEnumerator stopGradual()
    {
        audioSource.volume = 0.5f;
        yield return new WaitForSeconds(0.5f);
        audioSource.volume = 0.4f;
        yield return new WaitForSeconds(0.5f);
        audioSource.volume = 0.3f;
        yield return new WaitForSeconds(0.5f);
        audioSource.volume = 0.2f;
        yield return new WaitForSeconds(0.5f);
        audioSource.volume = 0.1f;
        yield return new WaitForSeconds(0.5f);
        audioSource.volume = 0f;
        yield return new WaitForSeconds(0.5f);
        audioSource.Stop();
    }

    IEnumerator stopGradualPlayerFalse(AudioSource src)
    {
        audioSource.volume = 0.5f;
        yield return new WaitForSeconds(0.5f);
        audioSource.volume = 0.4f;
        yield return new WaitForSeconds(0.5f);
        audioSource.volume = 0.3f;
        yield return new WaitForSeconds(0.5f);
        audioSource.volume = 0.2f;
        yield return new WaitForSeconds(0.5f);
        audioSource.volume = 0.1f;
        yield return new WaitForSeconds(0.5f);
        audioSource.volume = 0f;
        yield return new WaitForSeconds(0.5f);
        src.Stop();
        src.GetComponentInParent<environmentMusicController>().canPlay = false;
    }

    IEnumerator stopGradualPlayerTrue(AudioSource src)
    {
        audioSource.volume = 0.5f;
        yield return new WaitForSeconds(0.5f);
        audioSource.volume = 0.4f;
        yield return new WaitForSeconds(0.5f);
        audioSource.volume = 0.3f;
        yield return new WaitForSeconds(0.5f);
        audioSource.volume = 0.2f;
        yield return new WaitForSeconds(0.5f);
        audioSource.volume = 0.1f;
        yield return new WaitForSeconds(0.5f);
        audioSource.volume = 0f;
        yield return new WaitForSeconds(0.5f);
        src.Stop();
        src.GetComponentInParent<environmentMusicController>().canPlay = true;
    }


}
