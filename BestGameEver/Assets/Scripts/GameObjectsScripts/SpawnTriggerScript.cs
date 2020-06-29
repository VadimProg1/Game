using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTriggerScript : MonoBehaviour
{
    public Transform spawnPoint;
    [SerializeField] public PlayerHealth playerHealth;
    [SerializeField] public Respawn fallTrigger;
    public LayerMask fallTriggersMask;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            playerHealth.spawnPoint = spawnPoint;
            fallTrigger.spawnPoint = spawnPoint;
            Collider2D[] fallTriggers = Physics2D.OverlapCircleAll(spawnPoint.position, 1000000f, fallTriggersMask);
            for (int i = 0; i < fallTriggers.Length; i++)
            {
                fallTriggers[i].GetComponent<Respawn>().spawnPoint = spawnPoint;
            }            
        }
    }
}
