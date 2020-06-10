using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRangeEnemy : MonoBehaviour
{
    GameObject obj;

    public int damageFromBullet;
    public float timeToDestroy;
    private float checkTime;
    private bool checkDestroy = false;

    void Start()
    {
        obj = GameObject.FindGameObjectWithTag("Player");
        checkTime = timeToDestroy;
    }

    void FixedUpdate()
    {
        if (!checkDestroy)
        {
            checkDestroy = true;                            
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
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Enemy"))
        {
            DestroyBullet();
            checkDestroy = false;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            obj.GetComponent<PlayerHealth>().TakeDamage(damageFromBullet);
            DestroyBullet();
            checkDestroy = false;
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
