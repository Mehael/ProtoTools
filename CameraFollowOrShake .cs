using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraFollowOrShake : MonoBehaviour
{
    public Transform Target;
    public Vector3 TargetShift;
    public float DampingSpeed; //1f

    private static CameraFollowOrShake instance;
    private Vector3 currentVelocity;
    private static bool isCoroutineControlled;
    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (isCoroutineControlled)
            return;
        
        var targetPos = TargetShift + new Vector3(
                            Target.position.x,
                            Target.position.y,
                            transform.position.z);

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPos,
            ref currentVelocity,
            DampingSpeed);
    }

    public static IEnumerator Shake(float magnitude, float duration, float decreaseFactor = 1f)
    {
        isCoroutineControlled = true;
        var transform = instance.transform;
        var originalPos = transform.localPosition;
        while (duration > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * magnitude;
            duration -= Time.deltaTime * decreaseFactor; 
            yield return new WaitForEndOfFrame();
        }

        transform.localPosition = originalPos;
        isCoroutineControlled = false;
    }
}
