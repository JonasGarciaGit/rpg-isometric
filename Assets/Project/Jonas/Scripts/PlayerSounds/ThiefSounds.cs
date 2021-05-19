using UnityEngine;

public class ThiefSounds : MonoBehaviour
{
    public AudioSource audioSourceAttacks;


    public AudioClip lightAttackSound;
    public AudioClip heavyAttackSound;

    public void lightAttack()
    {
        if (gameObject.GetComponent<PhotonView>().isMine)
        {
            audioSourceAttacks.volume = 0.3f;
            audioSourceAttacks.clip = lightAttackSound;
            audioSourceAttacks.Play();
        }
       
    }

    public void heavyAttack()
    {

        if (gameObject.GetComponent<PhotonView>().isMine)
        {
            audioSourceAttacks.volume = 0.5f;
            audioSourceAttacks.clip = heavyAttackSound;
            audioSourceAttacks.Play();
        }
        
    }
}
