using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Versioning;
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
    private bool death = false;

    public Transform bar;
    private bool healthBarCheck;
    [SerializeField] private HealthBarScript healthBar;
    Vector3 startPos;
    private Material matWhite;
    private Material matDefault;
    private UnityEngine.Object explosionRef;
    private UnityEngine.Object meleeEnemyRef;
    SpriteRenderer sr;

    GameObject obj;
    GameObject objShake;

    void Start()
    {
        objShake = GameObject.FindGameObjectWithTag("Shaker");
        startPos = transform.position;
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;
        explosionRef = Resources.Load("Explosion");
        meleeEnemyRef = Resources.Load("MeleeEnemy");
        obj = GameObject.FindGameObjectWithTag("Player");
        health = maxHealth;
    }

    void Update()
    {
        if (!death)
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
                    if (Mathf.Abs(attackPos.position.x - PlayerPos.position.x) > 1.5f)
                    {
                        transform.Translate(-1 * Vector2.left * speed * Time.deltaTime);
                        if (!facingRight)
                        {
                            Flip();
                        }
                    }
                }
                else
                {
                    if (Mathf.Abs(attackPos.position.x - PlayerPos.position.x) > 1.5f)
                    {
                        transform.Translate(Vector2.left * speed * Time.deltaTime);
                        if (facingRight)
                        {
                            Flip();
                        }
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
    }

    public float GetHealthPercent()
    {
        return (float)health / (float)maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!death)
        {
            //objShake.GetComponent<CameraShakerScript>().Shake();
            //SoundManagerScript.PlaySound("playerHit");

            health -= damage;
            Debug.Log("Damage taken");
            sr.material = matWhite;
            if (health <= 0)
            {
                SoundManagerScript.PlaySound("enemyDeath");
                GameObject explosion = (GameObject)Instantiate(explosionRef);
                explosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                death = true;
                sr.enabled = false;
                GetComponent<BoxCollider2D>().isTrigger = true;
                healthBar.gameObject.SetActive(false);
            }
            else
            {
                Invoke("ResetMaterial", .1f);
            }
            healthBar.SetSize(GetHealthPercent());
        }
    }

    public void Respawn()
    {
        if (death)
        {
            health = maxHealth;
            sr.enabled = true;
            death = false;
            GetComponent<BoxCollider2D>().isTrigger = false;
            ResetMaterial();
            healthBar.gameObject.SetActive(true);
            healthBar.SetSize(GetHealthPercent());
            transform.position = new Vector3(startPos.x, startPos.y, startPos.z);
        }
        else
        {
            health = maxHealth;
            healthBar.SetSize(GetHealthPercent());
            transform.position = new Vector3(startPos.x, startPos.y, startPos.z);
        }
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
