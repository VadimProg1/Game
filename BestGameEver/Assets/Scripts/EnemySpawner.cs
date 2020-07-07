using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Object MeleeRef;
    Object FlyingRef;
    Object MiniRef;
    public Transform playerPos;

    void Start()
    {
        MeleeRef = Resources.Load("MeleeEnemy");
        MiniRef = Resources.Load("MiniMeleeEnemy");
        FlyingRef = Resources.Load("FlyingEnemy");
        Invoke("StartRespawn", 10f);
    }

    void SpawnEnemy()
    {
        int rand = Random.Range(0, 3);
        if(rand == 0)
        {
            GameObject enemy = (GameObject)Instantiate(MeleeRef);
            enemy.GetComponent<MeleeEnemy>().isSpawned = true;
            enemy.transform.position = new Vector2(transform.position.x, transform.position.y);
            enemy.GetComponent<MeleeEnemy>().PlayerPos = playerPos;
        }
        else if(rand == 1)
        {
            GameObject enemy = (GameObject)Instantiate(MiniRef);
            enemy.GetComponent<MiniMeleeEnemy>().isSpawned = true;
            enemy.transform.position = new Vector2(transform.position.x, transform.position.y);
            enemy.GetComponent<MiniMeleeEnemy>().PlayerPos = playerPos;
        }
        else if (rand == 2)
        {
            GameObject enemy = (GameObject)Instantiate(FlyingRef);
            enemy.GetComponent<FlyingEnemyAI>().isSpawned = true;
            enemy.transform.position = new Vector2(transform.position.x, transform.position.y);
            enemy.GetComponent<FlyingEnemyAI>().target = playerPos;
        }
    }

    void StartRespawn()
    {
        InvokeRepeating("SpawnEnemy", 0f, 20f); 
    }
}
