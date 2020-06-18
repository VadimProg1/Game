using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidKitScript : MonoBehaviour
{
    public int heal;

    GameObject objPlayer;

    void Start()
    {
        objPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            objPlayer.GetComponent<PlayerHealth>().TakeHeal(heal);
            Destroy(gameObject);
        }
    }
}
