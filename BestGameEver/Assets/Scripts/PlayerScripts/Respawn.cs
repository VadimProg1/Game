using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform spawnPoint;
    public PlayerHealth res;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            //other.transform.position = spawnPoint.position;
            res.RespawnPlayer();
        }
    }
}
