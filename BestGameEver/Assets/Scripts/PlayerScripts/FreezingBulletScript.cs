using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingBulletScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float bulletSpeed;
    public int damage;
    public float timeToDestroy;
    public float freezeTime;

    private bool checkDestroy = false;
    private bool checkPlayerHit = false;
    private float checkTime;

    public Transform attackPos;
    public LayerMask meleeEnemies;
    public LayerMask miniMeleeEnemies;
    public LayerMask flyingEnemies;
    public LayerMask rangeEnemies;
    public LayerMask turretEnemies;
    public LayerMask boss;

    GameObject objPlayer;
    private UnityEngine.Object explosionRef;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objPlayer = GameObject.FindGameObjectWithTag("Player");
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
            }
            else
            {
                rb.velocity = new Vector2(-bulletSpeed, 0);
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
            GameObject explosion = (GameObject)Instantiate(explosionRef);
            explosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            DestroyBullet();
            checkDestroy = false;
            
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, 1f, meleeEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<MeleeEnemy>().Freeze(damage, freezeTime);
            }

            enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, 1f, miniMeleeEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<MiniMeleeEnemy>().Freeze(damage, freezeTime);
            }

            enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, 1f, rangeEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<RangeEnemy>().Freeze(damage, freezeTime);
            }

            enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, 1f, turretEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<TurretEnemy>().Freeze(damage, freezeTime);
            }

            enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, 1f, flyingEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<FlyingEnemyAI>().Freeze(damage, freezeTime);
            }

            enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, 1f, boss);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<BossScript>().TakeDamage(damage);
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
