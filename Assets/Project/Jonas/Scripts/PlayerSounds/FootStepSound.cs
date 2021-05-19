using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{

    public AudioSource audioSourceFootStep;
    public AudioClip runningSound;

    public void footStepSound()
    {
        if (gameObject.GetComponent<PhotonView>().isMine)
        {
            audioSourceFootStep.loop = true;
            audioSourceFootStep.volume = 0.3f;
            audioSourceFootStep.clip = runningSound;
            audioSourceFootStep.Play();
        }
    }
}
