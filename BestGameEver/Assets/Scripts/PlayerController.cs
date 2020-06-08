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
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
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
