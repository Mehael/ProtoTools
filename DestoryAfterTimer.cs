using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryAfterTimer : MonoBehaviour
{
    public float timer;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    public void InstantKill()
    {
        Destroy(gameObject); 
    }
}
