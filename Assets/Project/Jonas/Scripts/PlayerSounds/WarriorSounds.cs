using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSounds : MonoBehaviour
{

    public AudioSource audioSourceAttacks;

    public AudioClip lightAttackSound;
    public AudioClip heavyAttackSound;

    public void lightAttack()
    {
        audioSourceAttacks.volume = 0.3f;
        audioSourceAttacks.clip = lightAttackSound;
        audioSourceAttacks.Play();
    }

    public void heavyAttack()
    {
        audioSourceAttacks.volume = 0.3f;
        audioSourceAttacks.clip = heavyAttackSound;
        audioSourceAttacks.Play();
    }
}
