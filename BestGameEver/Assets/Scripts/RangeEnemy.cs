using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
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
    public float shootingSpeed;

    public bool attack;
    private bool timeAttackCheck = true;
    public bool SeePlayer;
    private bool facingRight;
    private bool death;

    public Transform bar;
    private bool healthBarCheck;
    [SerializeField] private HealthBarScript healthBar;

    private Material matWhite;
    private Material matDefault;
    private UnityEngine.Object explosionRef;
    SpriteRenderer sr;
    Vector3 startPos;

    Object bulletRef;
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
        health = maxHealth;
        obj = GameObject.FindGameObjectWithTag("Player");
        bulletRef = Resources.Load("BulletEnemy");
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
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                    if (!facingRight)
                    {
                        Flip();
                    }
                }
                else
                {
                    transform.Translate(-1 * Vector2.left * speed * Time.deltaTime);
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
                        GameObject bul = (GameObject)Instantiate(bulletRef);
                        if (facingRight)
                        {
                            bul.transform.position = new Vector2(transform.position.x + 1f, transform.position.y + .2f);
                            bul.GetComponent<Rigidbody2D>().velocity = new Vector2(shootingSpeed, 0);
                        }
                        else
                        {
                            bul.transform.position = new Vector2(transform.position.x - 1f, transform.position.y - .2f);
                            bul.GetComponent<Rigidbody2D>().velocity = new Vector2(-shootingSpeed, 0);
                        }
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

    public void Respawn()
    {
        if (death)
        {
            health = maxHealth;
            sr.enabled = true;
            death = false;
            GetComponent<BoxCollider2D>().isTrigger = false;
            GetComponent<EdgeCollider2D>().isTrigger = false;
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

    public void TakeDamage(int damage)
    {
        if (!death)
        {
           // objShake.GetComponent<CameraShakerScript>().Shake();
            //SoundManagerScript.PlaySound("playerHit");

            health -= damage;
            Debug.Log("Damage taken");
            healthBar.SetSize(GetHealthPercent());
            sr.material = matWhite;
            if (health <= 0)
            {
                GameObject explosion = (GameObject)Instantiate(explosionRef);
                explosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                SoundManagerScript.PlaySound("enemyDeath");

                death = true;
                sr.enabled = false;
                GetComponent<BoxCollider2D>().isTrigger = true;
                GetComponent<EdgeCollider2D>().isTrigger = true;
                healthBar.gameObject.SetActive(false);
            }
            else
            {
                Invoke("ResetMaterial", .1f);
            }
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
