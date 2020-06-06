using System.Collections;
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

    private int extraJumps;
    public int amountOfJumps;
    private int jumpCount;

    private bool facingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isGrounded)
        {
            jumpCount = 1;
        }
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if((!facingRight && moveInput > 0) || (facingRight && moveInput < 0))
        {
            Flip();
        }      
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.W)){
            //CheckGrounded();
            Jump();
        }
    }

    void CheckGrounded()
    {
        if(Physics2D.OverlapCircle(groundCheck.position, 0.5f, whatIsGround))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void Jump()
    {
        if(isGrounded || jumpCount < 2)
        {
            rb.velocity = new Vector2(rb.velocity.x, 11f);
            isGrounded = false;
            jumpCount++;
        }
        else if(!isGrounded && jumpCount == 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, 11f);
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
