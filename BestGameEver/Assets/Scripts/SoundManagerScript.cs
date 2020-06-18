using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip playerHitSound, jumpSound, enemyDeathSound, playerWalkSound, fireSound,
        playerDeathSound, playerTakenDamageSound, hardBass;
    static AudioSource audioSrc;

    void Start()
    {
        playerHitSound = Resources.Load<AudioClip>("playerHit");
        jumpSound = Resources.Load<AudioClip>("jump");
        enemyDeathSound = Resources.Load<AudioClip>("enemyDeath");
        playerWalkSound = Resources.Load<AudioClip>("playerWalk");
        fireSound = Resources.Load<AudioClip>("playerFire");
        playerDeathSound = Resources.Load<AudioClip>("playerDeath");
        playerTakenDamageSound = Resources.Load<AudioClip>("playerTakenDamage");
        hardBass = Resources.Load<AudioClip>("musicBack");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "playerHit":
                audioSrc.PlayOneShot(playerHitSound);
                break;
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "playerWalk":
                audioSrc.PlayOneShot(playerWalkSound);
                break;
            case "playerFire":
                audioSrc.PlayOneShot(fireSound);
                break;
            case "playerDeath":
                audioSrc.PlayOneShot(playerDeathSound);
                break;
            case "enemyDeath":
                audioSrc.PlayOneShot(enemyDeathSound);
                break;
            case "takenDamage":
                audioSrc.PlayOneShot(playerTakenDamageSound);
                break;
            case "hardBass":
                audioSrc.PlayOneShot(hardBass);
                break;
        }
    }   
}
