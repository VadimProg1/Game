using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingToNextScene : MonoBehaviour
{
    public int indexOfNewLevel;
    public int amountOfPassesToMove = 0;
    public int collectedPasses = 0;
  
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && collectedPasses == amountOfPassesToMove)
        {
            SceneManager.LoadScene(indexOfNewLevel);
        }
     }  
}
