using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ConstantMovement : MonoBehaviour
{
    private Rigidbody2D rBody;
    public float velocity;
    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rBody.velocity = velocity * transform.up;
    }
}
