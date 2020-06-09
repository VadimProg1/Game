using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int indexOfSceneToRespawn;

    void Start()
    {
        
    }

    void Update()
    {
        if(health <= 0)
        {
            SceneManager.LoadScene(indexOfSceneToRespawn);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player taken Damage");
    }
}
