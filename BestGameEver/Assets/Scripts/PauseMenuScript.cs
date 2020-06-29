﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject buyMenuUI;
    public GameObject optionsMenuUI;
    public AudioMixer audioMixerSound;
    public AudioMixer audioMixerMusic;
    public PlayerController player;
    public MoneyScript money;
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
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
        buyMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
    }

    public void Settings()
    {

    }

    public void BuyExpBullets()
    {
        if(money.GetMoney() >= 2)
        {
            player.expBulletsIsBuyed = true;
            money.EraseMoney(2);
        }
    }

    public void BuyFreezeBullets()
    {
        player.freezeBulletsIsBuyed = true;
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
