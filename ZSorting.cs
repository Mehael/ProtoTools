using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZSorting : MonoBehaviour
{
    public float StartingZ;
    public float ExtraScaling = 1f;
    void Update()
    {
        var pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y,
            StartingZ + pos.y * 0.01f * ExtraScaling);
    }
}
