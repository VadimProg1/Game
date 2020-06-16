using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int healthMax;
    public int health;
    public int indexOfSceneToRespawn;
    [SerializeField] private HealthBarScript healthBar;

    void Start()
    {
        health = healthMax;
    }

    void Update()
    {
        if(health <= 0)
        {
            SceneManager.LoadScene(indexOfSceneToRespawn);
        }
        else if(health > healthMax)
        {
            health = healthMax;
        }
    }  

    public float GetHealthPercent()
    {
        return (float)health / (float)healthMax;
    }

    public void TakeDamage(int damage)
    {
        SoundManagerScript.PlaySound("takenDamage");
        health -= damage;
        Debug.Log("Player taken Damage");
        healthBar.SetSize(GetHealthPercent());
    }
}
