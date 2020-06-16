using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
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

    public Transform bar;
    private bool healthBarCheck;
    [SerializeField] private HealthBarScript healthBar;

    Object bulletRef;
    GameObject obj;

    void Start()
    {
        health = maxHealth;
        obj = GameObject.FindGameObjectWithTag("Player");
        bulletRef = Resources.Load("BulletEnemy");
    }

    void Update()
    {
        if (health <= 0)
        {
            SoundManagerScript.PlaySound("enemyDeath");
            Destroy(gameObject);
        }

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
                if (timeAttack <= 0)
                {
                    GameObject bul = (GameObject)Instantiate(bulletRef);
                    if (facingRight)
                    {
                        bul.transform.position = new Vector2(transform.position.x + 2f, transform.position.y);
                        playerX = PlayerPos.position.x;
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
                        playerX = PlayerPos.position.x;
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

    public float GetHealthPercent()
    {
        return (float)health / (float)maxHealth;        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Damage taken");
        healthBar.SetSize(GetHealthPercent());
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
