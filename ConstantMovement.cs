using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ConstantMovement : MonoBehaviour
{
    private Rigidbody2D rBody;
    public float velocity;
    public float delay = 0;
    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
            return;
        }
        
        rBody.velocity = velocity * transform.up;
    }
}
