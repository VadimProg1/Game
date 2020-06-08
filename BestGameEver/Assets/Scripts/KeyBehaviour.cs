using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    public bool collected = false;

    GameObject obj;

    void Start()
    {
        obj = GameObject.FindGameObjectWithTag("Door1");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") && collected == false)
        {
            collected = true;
            obj.GetComponent<MovingToNextScene>().collectedPasses += 1;
        }
    }
}
