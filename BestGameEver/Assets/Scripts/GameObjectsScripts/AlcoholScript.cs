using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcoholScript : MonoBehaviour
{
    GameObject objShake;
    private bool used = false;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        objShake = GameObject.FindGameObjectWithTag("Shaker");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") && !used)
        {
            objShake.GetComponent<CameraShakerScript>().ShakeDuration = 13f;
            objShake.GetComponent<CameraShakerScript>().ShakeAmplitude = 50f;
            objShake.GetComponent<CameraShakerScript>().ShakeFrequency = 0.02f;
            objShake.GetComponent<CameraShakerScript>().Shake();
            Invoke("ResetShake", 14f);
            used = true;
            sr.enabled = false;
        }
    }

    void ResetShake()
    {
        objShake.GetComponent<CameraShakerScript>().ShakeDuration = 0.3f;
        objShake.GetComponent<CameraShakerScript>().ShakeAmplitude = 1.2f;
        objShake.GetComponent<CameraShakerScript>().ShakeFrequency = 2.0f;
    }

    public void Respawn()
    {
        sr.enabled = true;
        used = false;
    }
}
