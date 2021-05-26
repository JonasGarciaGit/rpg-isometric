using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class environmentSoundV2 : MonoBehaviour
{
    public AudioSource audioSrc;
    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && other.gameObject.GetComponent<PhotonView>().isMine)
        {
            StartCoroutine("fadeIn");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player") && other.gameObject.GetComponent<PhotonView>().isMine)
        {
            StartCoroutine("fadeOut");
        }
    }

    IEnumerator fadeIn()
    {
        audioSrc.Play();
        animator.SetBool("fadeIn", true);
        animator.SetBool("fadeOut", false);
        yield return new WaitForSeconds(4f);
        animator.SetBool("fadeIn", false);
    }

    IEnumerator fadeOut()
    {
        animator.SetBool("fadeOut", true);
        animator.SetBool("fadeIn", false);
        yield return new WaitForSeconds(2f);
        audioSrc.Stop();
        animator.SetBool("fadeOut", false);
    }

}
