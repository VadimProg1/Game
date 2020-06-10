﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpForce;
    public float moveInput;
    public float speed;

    private bool isGrounded;
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
    private float  tempDashTimeCooldown;

    Object bulletRef;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tempDashTime = dashTime;
        tempDashTimeCooldown = dashTimeCooldown;
        bulletRef = Resources.Load("BulletPlayer");
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
            if (Input.GetKeyDown(KeyCode.X))
            {
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
               dashing = false;
               tempDashTime = dashTime;
            }
        }
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
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
            isGrounded = false;
            jumpCount++;
        }
        else if(!isGrounded && jumpCount == 1)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
            jumpCount++;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
