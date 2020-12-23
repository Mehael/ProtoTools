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
    public float lifeTime;
    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        var t = 0f;
        while (lifeTime < float.Epsilon || t < lifeTime)
        {
            rBody.velocity = velocity * transform.up;
            t += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        rBody.velocity = Vector2.zero;
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
