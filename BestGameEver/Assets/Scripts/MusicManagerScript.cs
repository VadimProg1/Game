using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    public static AudioClip musicClip;
    static AudioSource audioSrc;

    void Start()
    {
        musicClip = Resources.Load<AudioClip>("Music");       
        audioSrc = GetComponent<AudioSource>();
        PlaySound("Music");
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Music":
                audioSrc.PlayOneShot(musicClip);
                break;                       
        }
    }
}
