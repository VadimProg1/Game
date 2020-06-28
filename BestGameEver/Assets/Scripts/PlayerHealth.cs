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
    public LayerMask meleeEnemies;
    public LayerMask rangeEnemies;
    public LayerMask turretEnemies;
    public LayerMask boss;
    public LayerMask heal;
    public Animator animator;


    void Start()
    {
        health = healthMax;
    }

    public void RespawnPlayer()
    {        
        player.position = spawnPoint.position;
        health = healthMax;
        healthBar.SetSize(GetHealthPercent());

        Collider2D[] respawn = Physics2D.OverlapCircleAll(player.position, 1000000f, meleeEnemies);
        for (int i = 0; i < respawn.Length; i++)
        {
            respawn[i].GetComponent<MeleeEnemy>().Respawn();
        }
        respawn = Physics2D.OverlapCircleAll(player.position, 1000000f, rangeEnemies);
        for (int i = 0; i < respawn.Length; i++)
        {
            respawn[i].GetComponent<RangeEnemy>().Respawn();
        }
        respawn = Physics2D.OverlapCircleAll(player.position, 1000000f, turretEnemies);
        for (int i = 0; i < respawn.Length; i++)
        {
            respawn[i].GetComponent<TurretEnemy>().Respawn();
        }
        respawn = Physics2D.OverlapCircleAll(player.position, 1000000f, boss);
        for (int i = 0; i < respawn.Length; i++)
        {
            respawn[i].GetComponent<BossScript>().Respawn();
        }
        respawn = Physics2D.OverlapCircleAll(player.position, 1000000f, heal);
        for (int i = 0; i < respawn.Length; i++)
        {
            respawn[i].GetComponent<AidKitScript>().Respawn();
        }
    }


    public float GetHealthPercent()
    {
        return (float)health / (float)healthMax;
    }

    public void TakeDamage(int damage)
    {
        SoundManagerScript.PlaySound("takenDamage");
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
