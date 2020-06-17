using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public int maxHealth;
    private int health;
    public int damage;

    public Transform attackPos;
    public Transform PlayerPos;
    public LayerMask whatIsEnemies;

    public float speed;
    public float seeRange;
    private float timeAttack;
    public float startTimeAttack;
    public float attackRange;

    public bool attack;
    private bool timeAttackCheck = true;
    public bool SeePlayer;
    private bool facingRight;

    public Transform bar;
    private bool healthBarCheck;
    [SerializeField] private HealthBarScript healthBar;

    private Material matWhite;
    private Material matDefault;
    private UnityEngine.Object explosionRef;
    SpriteRenderer sr;

    GameObject obj;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;
        explosionRef = Resources.Load("Explosion");
        obj = GameObject.FindGameObjectWithTag("Player");
        health = maxHealth;
    }

    void Update()
    {       
        SeePlayer = Physics2D.OverlapCircle(attackPos.position, seeRange, whatIsEnemies);
        if (SeePlayer)
        {
            if (timeAttackCheck)
            {
                timeAttack = startTimeAttack;
            }
            
            if (attackPos.position.x < PlayerPos.position.x)
            {
                transform.Translate(-1 * Vector2.left * speed * Time.deltaTime);
                if (!facingRight)
                {
                    Flip();
                }
            }
            else
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                if (facingRight)
                {
                    Flip();
                }
            }
            attack = Physics2D.OverlapCircle(attackPos.position, attackRange, whatIsEnemies);
           
            if (attack)
            {
                if (timeAttack <= 0)
                {
                    obj.GetComponent<PlayerHealth>().TakeDamage(damage);
                    timeAttackCheck = true;
                }
                else
                {
                    timeAttackCheck = false;
                    timeAttack -= Time.deltaTime;
                }
            }           
        }
    }

    public float GetHealthPercent()
    {
        return (float)health / (float)maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Damage taken");
        sr.material = matWhite;
        if (health <= 0)
        {
            SoundManagerScript.PlaySound("enemyDeath");
            GameObject explosion = (GameObject)Instantiate(explosionRef);
            explosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Destroy(gameObject);
        }
        else
        {
            Invoke("ResetMaterial", .1f);
        }
        healthBar.SetSize(GetHealthPercent());
    }

    void ResetMaterial()
    {
        sr.material = matDefault;
    }

    private void Flip()
    {
        healthBarCheck = facingRight;
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        if ((healthBarCheck && !facingRight) || (!healthBarCheck && facingRight))
        {
            Scaler = bar.localScale;
            Scaler.x *= -1;
            bar.localScale = Scaler;
        }
    }
}
