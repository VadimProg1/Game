using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    public int health;
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


    Object bulletRef;
    GameObject obj;

    void Start()
    {
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
                    //obj.GetComponent<PlayerHealth>().TakeDamage(damage);
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

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Damage taken");
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
