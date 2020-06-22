using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int healthMax;
    public int health;
    public int indexOfSceneToRespawn;
    [SerializeField] private HealthBarScript healthBar;
    public Transform spawnPoint;
    public Transform player;

    void Start()
    {
        health = healthMax;
    }

    public void RespawnPlayer()
    {
        player.position = spawnPoint.position;
        health = healthMax;
        healthBar.SetSize(GetHealthPercent());
    }

    public float GetHealthPercent()
    {
        return (float)health / (float)healthMax;
    }

    public void TakeDamage(int damage)
    {
        SoundManagerScript.PlaySound("takenDamage");
        Debug.Log("Player taken Damage");
        healthBar.SetSize(GetHealthPercent());
        health -= damage;
        if (health <= 0)
        {
            RespawnPlayer();
        }       
    }

    public void TakeHeal(int heal)
    {
        health += heal;
        if(health > healthMax)
        {
            health = healthMax;
        }
        healthBar.SetSize(GetHealthPercent());
    }
}
