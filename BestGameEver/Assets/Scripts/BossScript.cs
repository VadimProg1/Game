using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
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
    public float meleeAttackRange;
    public float rangeAttackRange;
    public float shootingSpeed;
    public float flyingSectionTime;
    public float tempFlyingSectionTime;
    public float flyingSectionShootingSpeed;
    private float tempFlyingSectionShootingSpeed;
    public float peekingTime;
    public float tempPeekingTime;
    public float upPeekingTime;
    public float tempUpPeekingTime;
    public float waitForFlyAttack;
    private float tempWaitForFlyAttack;

    public bool rangeAttack;
    public bool meleeAttack;
    private bool timeAttackCheck = true;
    public bool SeePlayer;
    public int secondPeekAttack;
    public int firstPeekAttack;
    private bool facingRight;
    private bool stopMainAttacks = false;
    private bool peekingAbility = false;
    private bool peeking = false;
    private bool startCooldownForPeeking;
    private bool firstFly = false;
    private bool secondFly = false;
    public Transform bar;
    private bool healthBarCheck;
    [SerializeField] private HealthBarScript healthBar;

    private Material matWhite;
    private Material matDefault;
    SpriteRenderer sr;

    Rigidbody2D rb;
    Object bulletRef;
    GameObject obj;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;
        rb = GetComponent<Rigidbody2D>();
        obj = GameObject.FindGameObjectWithTag("Player");
        bulletRef = Resources.Load("BulletEnemy");
        tempFlyingSectionShootingSpeed = flyingSectionShootingSpeed;
        tempUpPeekingTime = upPeekingTime;
        tempPeekingTime = peekingTime;
        tempFlyingSectionTime = flyingSectionTime;
        tempWaitForFlyAttack = waitForFlyAttack;
        health = maxHealth;
        
    }

   
    void FixedUpdate()
    {              
        if (!stopMainAttacks)
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
                    if (Mathf.Abs(attackPos.position.x - PlayerPos.position.x) > 1f)
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
                    if (Mathf.Abs(attackPos.position.x - PlayerPos.position.x) > 1f)
                    {
                        transform.Translate(Vector2.left * speed * Time.deltaTime);
                        if (facingRight)
                        {
                            Flip();
                        }
                    }
                }
                rangeAttack = Physics2D.OverlapCircle(attackPos.position, rangeAttackRange, whatIsEnemies);
                meleeAttack = Physics2D.OverlapCircle(attackPos.position, meleeAttackRange, whatIsEnemies);

                if (rangeAttack)
                {
                    if (timeAttack <= 0)
                    {
                        GameObject bul = (GameObject)Instantiate(bulletRef);
                        if (facingRight)
                        {
                            bul.transform.position = new Vector2(transform.position.x + 1.5f, transform.position.y + .2f);
                            bul.GetComponent<Rigidbody2D>().velocity = new Vector2(shootingSpeed, 0);
                        }
                        else
                        {
                            bul.transform.position = new Vector2(transform.position.x - 1.5f, transform.position.y - .2f);
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

                if (meleeAttack)
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
        if (health <= firstPeekAttack && !firstFly && tempFlyingSectionTime >= 0)
        {           
            if(tempWaitForFlyAttack >= 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 6f);
                rb.gravityScale = 0;
                tempWaitForFlyAttack -= Time.deltaTime;
            }
            else
            {
                stopMainAttacks = true;
                tempFlyingSectionTime -= Time.deltaTime;
                FlyingAttack();
            }
        }

        if (health <= secondPeekAttack && !secondFly && tempFlyingSectionTime >= 0)
        {
            peekingAbility = false;
            if (tempWaitForFlyAttack >= 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 6f);
                rb.gravityScale = 0;
                tempWaitForFlyAttack -= Time.deltaTime;
            }
            else
            {
                stopMainAttacks = true;
                tempFlyingSectionTime -= Time.deltaTime;
                FlyingAttack();
            }
        }

        if (tempFlyingSectionTime <= 0 && tempFlyingSectionTime >= -1f)
        {
            if (firstFly)
            {
                secondFly = true;
            }
            firstFly = true;
            stopMainAttacks = false;
            peekingAbility = true;
            rb.gravityScale = 2;
            rb.velocity = new Vector2(rb.velocity.x, -6f);
            startCooldownForPeeking = true;
            tempFlyingSectionTime = flyingSectionTime;
            tempWaitForFlyAttack = waitForFlyAttack;
        }

        if (startCooldownForPeeking)
        {
            tempPeekingTime -= Time.deltaTime;
        }

        if (peekingAbility && tempPeekingTime <= 0)
        {
            peeking = true;
            stopMainAttacks = true;
            float playerX = PlayerPos.position.x;
            float playerY = PlayerPos.position.y;
            float vecX = playerX - attackPos.position.x;
            float vecY = playerY - attackPos.position.y;
            float normVecX = vecX / Mathf.Sqrt(vecX * vecX + vecY * vecY);
            float normVecY = vecY / Mathf.Sqrt(vecX * vecX + vecY * vecY);
            if (tempUpPeekingTime > 0)
            {
                rb.gravityScale = 0;
                rb.velocity = new Vector2(rb.velocity.x, 1f);
                tempUpPeekingTime -= Time.deltaTime;
            }
            else if(tempUpPeekingTime >= -0.3f)
            {
                tempUpPeekingTime -= Time.deltaTime;               
                rb.velocity = new Vector2(normVecX * 10f, normVecY * 10f);
            }
            else
            {
                bool checkMeleeAttack = Physics2D.OverlapCircle(attackPos.position, 3, whatIsEnemies);
                if (checkMeleeAttack)
                {
                    obj.GetComponent<PlayerHealth>().TakeDamage(damage);
                    timeAttackCheck = true;
                }
                rb.gravityScale = 2;
                tempPeekingTime = peekingTime;
                tempUpPeekingTime = upPeekingTime;
                stopMainAttacks = false;
            }
        }
        
    }

    private void FlyingAttack()
    {
        if (tempFlyingSectionShootingSpeed <= 0)
        {
            GameObject bul = (GameObject)Instantiate(bulletRef);
            bul.transform.position = new Vector2(transform.position.x - 2, transform.position.y - 2);
            float x = Mathf.Cos(Random.Range(30f, 150f));       
            bul.GetComponent<Rigidbody2D>().velocity = new Vector2(x * 15, -4f);
            GameObject bul2 = (GameObject)Instantiate(bulletRef);
            bul2.transform.position = new Vector2(transform.position.x + 2, transform.position.y - 2);
            x = Mathf.Cos(Random.Range(30f, 150f));
            bul2.GetComponent<Rigidbody2D>().velocity = new Vector2(x * 15, -6f);
            GameObject bul3 = (GameObject)Instantiate(bulletRef);
            bul3.transform.position = new Vector2(transform.position.x + 2, transform.position.y - 2);
            x = Mathf.Cos(Random.Range(30f, 150f));
            bul3.GetComponent<Rigidbody2D>().velocity = new Vector2(x * 15, -8f);
            tempFlyingSectionShootingSpeed = flyingSectionShootingSpeed;
        }
        tempFlyingSectionShootingSpeed -= Time.deltaTime;
    }

    public float GetHealthPercent()
    {
        return (float)health / (float)maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Damage taken");
        healthBar.SetSize(GetHealthPercent());
        sr.material = matWhite;
        if (health <= 0)
        {
            SoundManagerScript.PlaySound("enemyDeath");
            Destroy(gameObject);
            Application.Quit();
        }
        else
        {
            Invoke("ResetMaterial", .1f);
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
