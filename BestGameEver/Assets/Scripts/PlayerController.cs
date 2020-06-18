using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpForce;
    public float moveInput;
    public float speed;

    private bool isGrounded;
    private bool standingOnEnemy;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int jumpCount;

    public bool facingRight = true;
    public float dashTime;
    private float tempDashTime;
    public bool dashing;
    public float dashSpeed;
    public float dashTimeCooldown;
    public float shootingCooldown;
    private float tempShootingCooldown;
    private float  tempDashTimeCooldown;
    private float walkSoundTime = 0.3f;
    private float tempWalkSoundTime = 0.4f;
    public bool isOnHead;
    public bool dashHit = false;
    private Transform bar;
    private bool healthBarCheck;
    private TrailRenderer trail;

    GameObject objShake;

    Object bulletRef;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
        trail.enabled = false;
        tempDashTime = dashTime;
        tempDashTimeCooldown = dashTimeCooldown;
        bulletRef = Resources.Load("BulletPlayer");
        bar = transform.Find("HealthBar");
        objShake = GameObject.FindGameObjectWithTag("Shaker");
        tempShootingCooldown = shootingCooldown;
    }

    private void FixedUpdate()
    {
        if (!dashing)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
            if (isGrounded)
            {
                jumpCount = 1;
            }
            moveInput = Input.GetAxis("Horizontal");            
            tempWalkSoundTime -= Time.deltaTime;
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && tempWalkSoundTime <= 0 && isGrounded)
            {
                SoundManagerScript.PlaySound("playerWalk");
                tempWalkSoundTime = walkSoundTime;
            }             
            
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            if ((!facingRight && moveInput > 0) || (facingRight && moveInput < 0))
            {
                Flip();
            }                      
        }
    }

    private void Update()
    {
        //Dashing
        if (Input.GetKeyDown(KeyCode.F) && tempDashTimeCooldown <= 0)
        {
            trail.enabled = true;
            objShake.GetComponent<CameraShakerScript>().Shake();
            tempDashTimeCooldown = dashTimeCooldown;
            dashing = true;
        }
    
        if (!dashing)
        {
            //Jumping
            if (Input.GetKeyDown(KeyCode.W))
            {
                Jump();
            }
            //Shooting
            if (Input.GetKeyDown(KeyCode.X) && tempShootingCooldown <= 0)
            {
                tempShootingCooldown = shootingCooldown;
                objShake.GetComponent<CameraShakerScript>().Shake();
                SoundManagerScript.PlaySound("playerFire");
                GameObject bul = (GameObject)Instantiate(bulletRef);
                if (facingRight)
                {                
                    bul.transform.position = new Vector2(transform.position.x + 1f, transform.position.y + .2f);
                }
                else
                {
                    bul.transform.position = new Vector2(transform.position.x - 1f, transform.position.y - .2f);
                }
            }
            else
            {
                tempShootingCooldown -= Time.deltaTime;
            }

            tempDashTimeCooldown -= Time.deltaTime;
        }
        else
        {
            if (tempDashTime > 0)
            {
                tempDashTime -= Time.deltaTime;
                Dash();
            }
            else
            {
                dashHit = false;
                dashing = false;                       
                tempDashTime = dashTime;
                Invoke("TrailOff", .3f);
            }            
        }
    }
    
    private void TrailOff()
    {
        trail.enabled = false;
    }
  
    private void Dash()
    {
        if (facingRight)
        {
            rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-dashSpeed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        if(isGrounded || jumpCount < 2)
        {
            SoundManagerScript.PlaySound("jump");
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
            isGrounded = false;
            jumpCount++;
        }
        else if(!isGrounded && jumpCount == 1)
        {
            SoundManagerScript.PlaySound("jump");
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
            jumpCount++;
        }
    }

    void Flip()
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
