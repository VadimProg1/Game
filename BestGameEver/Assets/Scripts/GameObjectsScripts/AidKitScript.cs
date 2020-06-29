using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidKitScript : MonoBehaviour
{
    public int heal;
    private bool used = false;
    GameObject objPlayer;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        objPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") && !used)
        {
            objPlayer.GetComponent<PlayerHealth>().TakeHeal(heal);
            used = true;
            sr.enabled = false;
        }
    }
    public void Respawn()
    {
        sr.enabled = true;
        used = false;
    }
}
