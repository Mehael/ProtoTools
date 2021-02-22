using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftClickSpeedUp : MonoBehaviour
{
    public static bool isFast;
    private static LeftClickSpeedUp instance;
    public bool isDenied;

    private void Awake()
    {
        instance = this;
        if (isDenied)
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = .02f * Time.timeScale;
            Destroy(this);
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(1) != isFast)
        {
            isFast = Input.GetMouseButton(1);
            if (isFast)
                Time.timeScale = 3f;
            else 
                Time.timeScale = 1;
            
            Time.fixedDeltaTime = .02f * Time.timeScale;
        }
    }
}
