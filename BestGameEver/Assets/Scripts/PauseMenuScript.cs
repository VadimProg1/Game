using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public AudioMixer audioMixerSound;
    public AudioMixer audioMixerMusic;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }       
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Settings()
    {

    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void SetVolume(float volume)
    {
        audioMixerSound.SetFloat("volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixerMusic.SetFloat("musicVolume", volume);
    }
}
