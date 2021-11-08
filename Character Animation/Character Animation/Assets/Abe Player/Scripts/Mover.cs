﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 2;
    void Update()
    {
        transform.Translate ( Vector3.forward * speed * Time.deltaTime );
    }
}
