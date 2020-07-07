using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class PauseMenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject buyMenuUI;
    public GameObject optionsMenuUI;
    public AudioMixer audioMixerSound;
    public AudioMixer audioMixerMusic;
    public Slider musicSlider;
    public Slider soundSlider;
    public static float musicVolume = 0;
    public static float soundVolume = 0;
    public PlayerController player;
    public MoneyScript money;
    public TextMeshProUGUI moneyValue;
    public TextMeshProUGUI defSkinValue;
    public TextMeshProUGUI redSkinValue;
    public TextMeshProUGUI greenSkinValue;
    public TextMeshProUGUI freezeBulletValue;
    public TextMeshProUGUI expBulletValue;
    public TextMeshProUGUI gunValue;

    public int costForGun = 2;
    public int costForExpBullet = 2;
    public int costForFreezeBullet = 2;
    public int costForGreenSkin = 2;
    public int costForRedSkin = 2;

    public GameObject buyFreezeButton;
    public GameObject buyExpButton;
    public GameObject buyGunButton;

    void Start()
    {
        Cursor.visible = false;
        musicSlider.value = musicVolume;
        soundSlider.value = soundVolume;

        UpdateValues();
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

    public void MusicVolumeIsChanged()
    {
        musicVolume = musicSlider.value;
    }

    public void SoundVolumeIsChanged()
    {
        soundVolume = soundSlider.value;
    }

    public void UpdateValues()
    {
        moneyValue.text = money.GetMoney().ToString();

        if (player.GetCurrentSkin() == "Default")
        {
            defSkinValue.text = "(current)";
            if (player.GetGreenSkinIsBuyed())
            {
                greenSkinValue.text = "";
            }
            else
            {
                greenSkinValue.text = "Cost: " + costForGreenSkin.ToString();
            }
            if (player.GetRedSkinIsBuyed())
            {
                redSkinValue.text = "";
            }
            else
            {
                redSkinValue.text = "Cost: " + costForRedSkin.ToString();
            }
        }
        else if(player.GetCurrentSkin() == "Green")
        {
            defSkinValue.text = "";
            greenSkinValue.text = "(current)";
            if (player.GetRedSkinIsBuyed())
            {
                redSkinValue.text = "";
            }
            else
            {
                redSkinValue.text = "Cost: " + costForRedSkin.ToString();
            }
        }
        else if(player.GetCurrentSkin() == "Red")
        {
            defSkinValue.text = "";
            redSkinValue.text = "(current)";
            if (player.GetGreenSkinIsBuyed())
            {
                greenSkinValue.text = "";
            }
            else
            {
                greenSkinValue.text = "Cost: " + costForGreenSkin.ToString();
            }
        }
        
        if (player.GetGunIsBuyed() == false)
        {
            gunValue.text = "Cost: " + costForGun.ToString();
            buyFreezeButton.SetActive(false);
            buyExpButton.SetActive(false);
        }
        else
        {
            buyGunButton.SetActive(false);
            if (player.GetFreezeIsBuyed())
            {
                freezeBulletValue.text = "Buyed";
            }
            else
            {
                freezeBulletValue.text = "Cost: " + costForFreezeBullet.ToString();
            }
            if (player.GetExplosionIsBuyed())
            {
                expBulletValue.text = "Buyed";
            }
            else
            {
                expBulletValue.text = "Cost: " + costForExpBullet.ToString();
            }
            buyFreezeButton.SetActive(true);
            buyExpButton.SetActive(true);
        }

    }

    public void BuyExpBullets()
    {
        if(money.GetMoney() >= costForExpBullet && player.GetExplosionIsBuyed() == false)
        {
            player.ExplosionIsBuyed();
            money.EraseMoney(costForExpBullet);
            UpdateValues();
        }
    }

    public void BuyGun()
    {
        if (money.GetMoney() >= costForGun && player.GetGunIsBuyed() == false)
        {
            player.GunIsBuyed();
            money.EraseMoney(costForGun);
            UpdateValues();
            buyGunButton.SetActive(false);
            buyFreezeButton.SetActive(true);
            buyExpButton.SetActive(true);
        }
    }

    public void DefaultSkin()
    {       
        player.CurrentSkin("Default");
        UpdateValues();
    }

    public void GreenSkin()
    {
        if (money.GetMoney() >= costForGreenSkin && player.GetGreenSkinIsBuyed() == false)
        {
            player.GreenSkinIsBuyed();
            money.EraseMoney(costForGreenSkin);           
            player.CurrentSkin("Green");
        }
        else if (player.GetGreenSkinIsBuyed())
        {
            player.CurrentSkin("Green");
        }
        UpdateValues();
    }

    public void RedSkin()
    {
        if (money.GetMoney() >= costForRedSkin && player.GetRedSkinIsBuyed() == false)
        {
            player.RedSkinIsBuyed();
            money.EraseMoney(costForRedSkin);
            player.CurrentSkin("Red");
        }        
        else if (player.GetRedSkinIsBuyed())
        {
            player.CurrentSkin("Red");
        }
        UpdateValues();
    }

    public void BuyFreezeBullets()
    {
        if (money.GetMoney() >= costForFreezeBullet && player.GetFreezeIsBuyed() == false)
        {
            player.FreezeIsBuyed();
            money.EraseMoney(costForFreezeBullet);
            UpdateValues();
        }        
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
