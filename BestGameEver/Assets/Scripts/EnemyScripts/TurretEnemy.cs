using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    public int maxHealth;
    private int health;
    public int damage;
    public int maxFreezeBulletCounter = 2;
    public int freezeBulletCounter = 0;

    public Transform attackPos;
    public Transform PlayerPos;
    public LayerMask whatIsEnemies;

    public float speed;
    public float seeRange;
    private float timeAttack;
    public float startTimeAttack;
    public float attackRange;
    public float shootingSpeed;
    public float playerX;
    public float playerY;
    private float lenghtToPlayer;
    public float vecX;
    public float vecY;
    public float normVecX;
    public float normVecY;

    public bool attack;
    private bool timeAttackCheck = true;
    public bool SeePlayer;
    private bool facingRight;
    private bool checkPlayerPosition;
    private bool death = false;
    private bool firstDeath = true;

    public Transform bar;
    private bool healthBarCheck;
    [SerializeField] private HealthBarScript healthBar;

    private Material matWhite;
    private Material matDefault;
    private UnityEngine.Object explosionRef;
    private UnityEngine.Object coinRef;
    SpriteRenderer sr;

    Object bulletRef;
    GameObject obj;
    GameObject objShake;

    private bool frozen = false;
    private float timeFreeze = -1;

    void Start()
    {
        objShake = GameObject.FindGameObjectWithTag("Shaker");
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        coinRef = Resources.Load("Coin");
        matDefault = sr.material;
        explosionRef = Resources.Load("Explosion");
        health = maxHealth;
        obj = GameObject.FindGameObjectWithTag("Player");
        bulletRef = Resources.Load("BulletEnemy");
    }

    void Update()
    {
        if (timeFreeze > 0)
        {
            timeFreeze -= Time.deltaTime;
        }
        else
        {
            frozen = false;
        }

        if (!death && !frozen)
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
                    if (!facingRight)
                    {
                        Flip();
                    }
                }
                else
                {
                    if (facingRight)
                    {
                        Flip();
                    }
                }
                attack = Physics2D.OverlapCircle(attackPos.position, attackRange, whatIsEnemies);

                if (attack)
                {
                    if (timeAttack <= 0 && PlayerPos.position.y < attackPos.position.y)
                    {
                        GameObject bul = (GameObject)Instantiate(bulletRef);
                        if (facingRight)
                        {
                            bul.transform.position = new Vector2(transform.position.x + 2f, transform.position.y);
                            playerX = PlayerPos.position.x - 2f;
                            playerY = PlayerPos.position.y;
                            vecX = playerX - attackPos.position.x;
                            vecY = playerY - attackPos.position.y;
                            normVecX = vecX / Mathf.Sqrt(vecX * vecX + vecY * vecY);
                            normVecY = vecY / Mathf.Sqrt(vecX * vecX + vecY * vecY);
                            bul.GetComponent<Rigidbody2D>().velocity = new Vector2(normVecX * speed, normVecY * speed);
                        }
                        else
                        {
                            bul.transform.position = new Vector2(transform.position.x - 2f, transform.position.y);
                            playerX = PlayerPos.position.x + 2f;
                            playerY = PlayerPos.position.y;
                            vecX = playerX - attackPos.position.x;
                            vecY = playerY - attackPos.position.y;
                            normVecX = vecX / Mathf.Sqrt(vecX * vecX + vecY * vecY);
                            normVecY = vecY / Mathf.Sqrt(vecX * vecX + vecY * vecY);
                            bul.GetComponent<Rigidbody2D>().velocity = new Vector2(normVecX * speed, normVecY * speed);
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

    public void Freeze(int damage, float freezeTime)
    {
        freezeBulletCounter++;
        if (freezeBulletCounter >= maxFreezeBulletCounter)
        {
            timeFreeze = freezeTime;
            frozen = true;
            freezeBulletCounter = 0;
        }
        TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        if (!death)
        {           
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
                healthBar.gameObject.SetActive(false);
                if (firstDeath)
                {
                    firstDeath = false;
                    GameObject coin = (GameObject)Instantiate(coinRef);
                    coin.transform.position = new Vector2(transform.position.x, transform.position.y - 1f);
                    coin.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 4);
                }
            }
            else
            {
                Invoke("ResetMaterial", .1f);
            }
        }
    }

    public void Respawn()
    {        
        if (death)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
            health = maxHealth;
            sr.enabled = true;
            death = false;
            GetComponent<BoxCollider2D>().enabled = true;
            ResetMaterial();
            healthBar.gameObject.SetActive(true);
            healthBar.SetSize(GetHealthPercent());
        }
        else
        {
            health = maxHealth;
            healthBar.SetSize(GetHealthPercent());
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
