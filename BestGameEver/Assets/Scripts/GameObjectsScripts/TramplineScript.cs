using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TramplineScript : MonoBehaviour
{
    public PlayerController player;
    public float jumpForce;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
        }
    }
}
