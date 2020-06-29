using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeetScript : MonoBehaviour
{
    public GameObject player;
    PlayerController playerController;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Platform"))
        {
            player.transform.parent = other.gameObject.transform;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Platform"))
        {
            player.transform.parent = null;
        }
    }
}
