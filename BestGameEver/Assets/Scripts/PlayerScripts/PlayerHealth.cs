using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    static public int healthMax = 10;
    public int health;
    public int indexOfSceneToRespawn;
    [SerializeField] private HealthBarScript healthBar;
    public PlayerController playerController;
    public Transform spawnPoint;
    public Transform player;
    public LayerMask meleeEnemies;
    public LayerMask miniMeleeEnemies;
    public LayerMask rangeEnemies;
    public LayerMask turretEnemies;
    public LayerMask boss;
    public LayerMask flyingEnemies;
    public LayerMask heal;
    public Animator animator;
    private float respawnTime = 1f;
    private float tempRespawnTime = 1f;


    void Start()
    {
        health = healthMax;
    }

    public void RespawnPlayer()
    {
        animator.SetBool("Death", false);
        player.position = spawnPoint.position;
        health = healthMax;
        healthBar.SetSize(GetHealthPercent());
        Invoke("ReturnSpeed", .7f);

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

        respawn = Physics2D.OverlapCircleAll(player.position, 1000000f, miniMeleeEnemies);
        for (int i = 0; i < respawn.Length; i++)
        {
            respawn[i].GetComponent<MiniMeleeEnemy>().Respawn();
        }

        respawn = Physics2D.OverlapCircleAll(player.position, 1000000f, flyingEnemies);
        for (int i = 0; i < respawn.Length; i++)
        {
            respawn[i].GetComponent<FlyingEnemyAI>().Respawn();
        }

        respawn = Physics2D.OverlapCircleAll(player.position, 1000000f, heal);
        for (int i = 0; i < respawn.Length; i++)
        {
            respawn[i].GetComponent<AidKitScript>().Respawn();
        }
    }

    public void SetMaxHealth(int health)
    {
        healthMax = health;
    }

    void Update()
    {
        tempRespawnTime -= Time.deltaTime;
    }

    public float GetHealthPercent()
    {
        return (float)health / (float)healthMax;
    }

    void ReturnSpeed()
    {
        playerController.speed = playerController.speedMax;
    }

    public void TakeDamage(int damage)
    {
        SoundManagerScript.PlaySound("takenDamage");
        healthBar.SetSize(GetHealthPercent());
        health -= damage;
        if (health <= 0  && tempRespawnTime <= 0)
        {
            tempRespawnTime = respawnTime;
            animator.SetBool("Death", true);
            playerController.speed = 0f;
            Invoke("RespawnPlayer", 0.7f);
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
