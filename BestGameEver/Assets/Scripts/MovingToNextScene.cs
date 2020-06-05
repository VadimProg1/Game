using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingToNextScene : MonoBehaviour
{
    [SerializeField] private int indexOfNewLevel;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(indexOfNewLevel);
        }
    }
}
