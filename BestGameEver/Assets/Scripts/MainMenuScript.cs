using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int healthForHits;
    public int healthForAverage;
    public int healthForEasy;

    public void HitsMode()
    {
        playerHealth.SetMaxHealth(healthForHits);
        SceneManager.LoadScene(1);
    }

    public void AverageMode()
    {
        playerHealth.SetMaxHealth(healthForAverage);
        SceneManager.LoadScene(1);
    }

    public void EasyMode()
    {
        playerHealth.SetMaxHealth(healthForEasy);
        SceneManager.LoadScene(1);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {       
        Application.Quit();
    }
}
