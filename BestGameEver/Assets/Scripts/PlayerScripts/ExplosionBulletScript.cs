using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBulletScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float bulletSpeed;
    public int damage;
    public float timeToDestroy;

    private bool checkDestroy = false;
    private bool checkPlayerHit = false;
    private float checkTime;

    public Transform attackPos;
    public LayerMask meleeEnemies;
    public LayerMask miniMeleeEnemies;
    public LayerMask flyingEnemies;
    public LayerMask rangeEnemies;
    public LayerMask turretEnemies;
    public LayerMask bossEnemy;
    public LayerMask player;

    GameObject objShake;
    GameObject objPlayer;
    private UnityEngine.Object explosionRef;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objPlayer = GameObject.FindGameObjectWithTag("Player");
        objShake = GameObject.FindGameObjectWithTag("Shaker");
        checkTime = timeToDestroy;
        explosionRef = Resources.Load("ExplosionForBullet");
    }

    void FixedUpdate()
    {
        if (!checkDestroy)
        {
            checkDestroy = true;
            if (objPlayer.GetComponent<PlayerController>().facingRight == true)
            {
                rb.velocity = new Vector2(bulletSpeed, 0);
               // objPlayer.GetComponent<PlayerController>().rb.velocity = new Vector2(-30, 10);
            }
            else
            {
                rb.velocity = new Vector2(-bulletSpeed, 0);
               // objPlayer.GetComponent<PlayerController>().rb.velocity = new Vector2(30, 10);
            }
        }
        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy <= 0)
        {
            DestroyBullet();
            checkDestroy = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            objShake.GetComponent<CameraShakerScript>().Shake();
            GameObject explosion = (GameObject)Instantiate(explosionRef);
            explosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            DestroyBullet();          
            checkDestroy = false;
            checkPlayerHit = Physics2D.OverlapCircle(attackPos.position, 1f, player);
            if (checkPlayerHit)
            {
                checkPlayerHit = false;
                objPlayer.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            objShake.GetComponent<CameraShakerScript>().Shake();
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, 1f, meleeEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<MeleeEnemy>().TakeDamage(damage);
            }

            enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, 1f, rangeEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<RangeEnemy>().TakeDamage(damage);
            }

            enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, 1f, turretEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<TurretEnemy>().TakeDamage(damage);
            }

            enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, 1f, bossEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<BossScript>().TakeDamage(damage);
            }
            checkPlayerHit = Physics2D.OverlapCircle(attackPos.position, 1f, player);
            if (checkPlayerHit)
            {
                checkPlayerHit = false;
                objPlayer.GetComponent<PlayerHealth>().TakeDamage(damage);
            }

            enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, 1f, miniMeleeEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<MiniMeleeEnemy>().TakeDamage(damage);
            }

            enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, 1f, flyingEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<FlyingEnemyAI>().TakeDamage(damage);
            }

            GameObject explosion = (GameObject)Instantiate(explosionRef);
            explosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            DestroyBullet();
            checkDestroy = false;
        }

    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
