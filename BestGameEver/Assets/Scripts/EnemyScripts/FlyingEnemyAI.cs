using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Threading;

public class FlyingEnemyAI : MonoBehaviour
{
    public Transform attackPos;
    public Transform target;
    public Transform bar;
    public LayerMask whatIsEnemies;

    public int maxHealth;
    private int health;
    int currentWaypoint = 0;
    public int damage;

    private float speed;
    public float speedMax;
    public float nextWaypointDistance = 3f;
    public float seeRange;
    public float timeAttack = 0;
    public float startTimeAttack;
    public float attackRange;
    private float timeFreeze = -1f;

    bool reachedEndOfPath = false;
    bool facingRight = false;
    public bool attack;
    private bool timeAttackCheck = true;
    public bool SeePlayer;
    private bool death = false;
    private bool frozen = false;
    private bool healthBarCheck;

    Path path;
    Seeker seeker;
    Rigidbody2D rb;
    Vector3 startPos;
    GameObject obj;
    GameObject objShake;
    SpriteRenderer sr;

    [SerializeField] private HealthBarScript healthBar;

    private Material matWhite;
    private Material matDefault;
    private UnityEngine.Object explosionRef;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        startPos = transform.position;
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;
        explosionRef = Resources.Load("Explosion");
        obj = GameObject.FindGameObjectWithTag("Player");
        health = maxHealth;
        timeAttack = startTimeAttack;
        speed = speedMax;

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (rb.velocity.x >= 0.01f)
        {
            if (!facingRight)
            {
                Flip();
            }
        }
        else if (rb.velocity.x <= 0.01f)
        {
            if (facingRight)
            {
                Flip();
            }
        }     
    }

    void Update()
    {
        if (!death && !frozen)
        {
            attack = Physics2D.OverlapCircle(attackPos.position, attackRange, whatIsEnemies);

            if (attack)
            {
                if (timeAttack <= 0)
                {
                    obj.GetComponent<PlayerHealth>().TakeDamage(damage);
                    timeAttack = startTimeAttack;
                }
                else
                {
                    timeAttackCheck = false;
                    timeAttack -= Time.deltaTime;
                }
            }
        }
    }

    public void Respawn()
    {
        if (death)
        {
            speed = speedMax;
            health = maxHealth;
            sr.enabled = true;
            death = false;
            transform.position = new Vector3(startPos.x, startPos.y, startPos.z);
            GetComponent<CircleCollider2D>().isTrigger = false;
            ResetMaterial();
            healthBar.gameObject.SetActive(true);
            healthBar.SetSize(GetHealthPercent());
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
            //objShake.GetComponent<CameraShakerScript>().Shake();
            //SoundManagerScript.PlaySound("playerHit");

            speed = 0;
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
                GetComponent<CircleCollider2D>().isTrigger = true;
                healthBar.gameObject.SetActive(false);
            }
            else
            {
                Invoke("ResetMaterial", .1f);
            }
            healthBar.SetSize(GetHealthPercent());
        }
    }

    void ResetMaterial()
    {
        sr.material = matDefault;
    }

    public float GetHealthPercent()
    {
        return (float)health / (float)maxHealth;
    }

    public void Freeze(int damage, float freezeTime)
    {
        timeFreeze = freezeTime;
        frozen = true;
        TakeDamage(damage);
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
