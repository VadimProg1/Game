﻿using System.Collections;
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
    public LayerMask whatIsEnemies;

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
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, 3, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<MeleeEnemy>().TakeDamage(damage);
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
