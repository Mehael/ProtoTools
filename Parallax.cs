using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Vector3 AxisMovementScale = Vector3.zero;
    private Transform myCamera;
    private Vector3 prevCamPos;
    private void Awake()
    {
        myCamera = Camera.main.transform;
        prevCamPos = myCamera.position;
    }

    private void LateUpdate()
    {
        var d = prevCamPos - myCamera.position;
        transform.Translate(
            d.x * AxisMovementScale.x,
            d.y * AxisMovementScale.y,
            d.z * AxisMovementScale.z
            );

        prevCamPos = myCamera.position;
    }
}
