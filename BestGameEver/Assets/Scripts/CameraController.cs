﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, player.position.z);
    }
}
