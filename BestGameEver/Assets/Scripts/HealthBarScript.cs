using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    private Transform bar;
    public SpriteRenderer background;
    public SpriteRenderer border;
    public SpriteRenderer barSprite;
    public bool Player;


    private void Start()
    {
        bar = transform.Find("Bar");
        if (!Player)
        {
            background.enabled = false;
            border.enabled = false;
            barSprite.enabled = false;
        }
    }

    public void SetSize(float sizeNormalized)
    {
        if (!Player)
        {
            background.enabled = true;
            border.enabled = true;
            barSprite.enabled = true;
        }
        bar.localScale = new Vector3(sizeNormalized, 1f);      
    }
}
