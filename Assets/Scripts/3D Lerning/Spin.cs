﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0, speed, 0);
    }
}
