using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer soundAudioMixer;
    public AudioMixer musicAudioMixer;

    public void SetVolume (float volume)
    {        
        soundAudioMixer.SetFloat("volume", volume);        
    }

    public void SetMusicVolume(float volume)
    {       
        musicAudioMixer.SetFloat("musicVolume", volume);
    }
}
