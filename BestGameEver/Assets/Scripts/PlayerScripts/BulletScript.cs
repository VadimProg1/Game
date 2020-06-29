using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{    
    Rigidbody2D rb;
    public float bulletSpeed;
    public int damage;
    public float timeToDestroy;

    private bool checkDestroy = false;
    private float checkTime;

    public Transform attackPos;
    public LayerMask meleeEnemies;
    public LayerMask rangeEnemies;
    public LayerMask turretEnemies;
    public LayerMask miniMeleeEnemies;
    public LayerMask bossEnemy;

    GameObject objPlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objPlayer = GameObject.FindGameObjectWithTag("Player");
        checkTime = timeToDestroy;
    }

    void FixedUpdate()
    {
        if (!checkDestroy)
        {
            checkDestroy = true;
            if (objPlayer.GetComponent<PlayerController>().facingRight == true)
            {
                rb.velocity = new Vector2(bulletSpeed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-bulletSpeed, 0);
            }           
        }
        timeToDestroy -= Time.deltaTime;
        if(timeToDestroy <= 0)
        {
            DestroyBullet();
            checkDestroy = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            DestroyBullet();
            checkDestroy = false;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, 1, meleeEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<MeleeEnemy>().TakeDamage(damage);            
            }
            enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, 1, rangeEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<RangeEnemy>().TakeDamage(damage);
            }          
            enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, 1, turretEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<TurretEnemy>().TakeDamage(damage);
            }
            enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, 1, bossEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<BossScript>().TakeDamage(damage);
            }
            enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, 1, miniMeleeEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<MiniMeleeEnemy>().TakeDamage(damage);
            }
            DestroyBullet();
            checkDestroy = false;
        }

    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
