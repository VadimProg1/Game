using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidKitTimerScript : MonoBehaviour
{
    public int heal;
    public float timer;
    private float tempTimer;
    private bool used = false;
    GameObject objPlayer;
    SpriteRenderer sr;

    void Start()
    {
        tempTimer = timer;
        sr = GetComponent<SpriteRenderer>();
        objPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (used)
        {
            tempTimer -= Time.deltaTime;
            if(tempTimer <= 0)
            {
                used = false;
                sr.enabled = true;
                tempTimer = timer;
            }
        }
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
}
