using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public static class ExtensionMethods
{
    private const float DefaultMovementSpeed = 5;

    private const int SecondsInHour = 60 * 60;
    private const int SecondsInDay = SecondsInHour * 24;
    public static string ToTimeString(this float seconds, int fontSizeOfLetter = -1)
    {
        if (seconds <= 60)
            return scaledLabel(seconds, "c.");
        
        if (seconds <= SecondsInHour)
            return scaledLabel(seconds / 60, "м.");
        
        if (seconds <= SecondsInDay)
            return scaledLabel(seconds / SecondsInHour, "ч.");

        return scaledLabel(seconds / SecondsInDay,"д.");

        string scaledLabel(float value, string letter)
        {
            return Mathf.CeilToInt(value) + 
                (fontSizeOfLetter < 0 ? " " + letter : 
                " <size=" + fontSizeOfLetter + ">" + letter + "</size>");
        }
    }
    
    public static IEnumerator MoveTo(this Transform transform, Vector3 target, float speed = DefaultMovementSpeed, 
        Action callback = null)
    {
        var start = transform.position;
        var t = 0f;
        while (t <= 1.0f) {
            t += Time.deltaTime * speed; // Goes from 0 to 1, incrementing by step each time
            if (transform == null)
                yield break;
            
            transform.position = Vector3.Lerp(start, target, t);
            yield return new WaitForEndOfFrame();       
        }
        
        if (transform != null)
            transform.position = target;

        callback?.Invoke();
    }
    
    public static IEnumerator MoveTo(this Transform transform, Transform target, float speed = DefaultMovementSpeed, 
        Action callback = null)
    {
        var distance = 0f;
        var delta = Vector3.zero;
        do
        {
            if (transform == null || target == null)
                yield break;
            
            transform.Translate(delta);
            
            var direction = target.transform.position - transform.position;
            distance = direction.magnitude;
            direction = direction.normalized;

            delta = Time.deltaTime * speed * direction;
            yield return new WaitForEndOfFrame();
        } while (delta.magnitude < distance);
        
        if (transform != null)
            transform.position = target.position;

        callback?.Invoke();
    }
    
    public static IEnumerator LocalMoveTo(this Transform transform, Vector3 target, float speed = DefaultMovementSpeed)
    {
        var start = transform.localPosition;
        var distance = (target - start).magnitude;
        var time = distance / speed;
        var t = 0f;
        while (t <= 1.0f) {
            t += Time.deltaTime / time; 
            if (transform == null)
                yield break;
            
            transform.localPosition = Vector3.Lerp(start, target, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        transform.localPosition = target;
    }

    private static List<Transform> WobblesList = new List<Transform>();
    private const float DefaultWobbleSpeed = 2;
    public static IEnumerator Wobble(this Transform transform, float growPercent = 0.2f, float speed = DefaultWobbleSpeed)
    {
        if (WobblesList.Contains(transform))
            yield break;
        
        WobblesList.Add(transform);
        var baseScale = transform.localScale;
        var newScale = new Vector3(
            baseScale.x * (1 - growPercent), 
            baseScale.y * (1 + growPercent), 
            baseScale.y);
        
        yield return transform.ScaleFromTo(baseScale,newScale, speed);
        yield return transform.ScaleFromTo(newScale, baseScale, speed);
        WobblesList.Remove(transform);
    }
    
    private const float DefaultScaleSpeed = 50; 
    public static IEnumerator ScaleFromTo(this Transform transform, Vector3 start, Vector3 end, float speed = DefaultScaleSpeed)
    {
        var t = 0f;
        while (t <= 1.0f) {
            t += Time.deltaTime * speed;
            if (transform == null)
                yield break;
            
            transform.localScale = Vector3.Lerp(start, end, t); 
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = end;
    }

    public static IEnumerator ScaleAppear(this Transform transform, Vector3 targetScale, float speed = DefaultScaleSpeed)
    {
        return transform.ScaleFromTo(Vector3.zero, targetScale, speed);
    }
    
    public static IEnumerator ScaleHide(this Transform transform, float speed = DefaultScaleSpeed)
    {
        return transform.ScaleFromTo(transform.localScale, Vector3.zero, speed);
    }
    
    public static Color GetRandomColor()
    {
        return new Color(
            Random.value,
            Random.value,
            Random.value
        );
    }
    
    public static Vector2 ToVector2(this Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.y);
    }
    
    public static Vector3 ToVector3(this Vector2 vector2)
    {
        return new Vector3(vector2.x, vector2.y, 0);
    }
    
    public static Vector2 WorldToCanvas(this Canvas canvas, Vector3 worldPosition)
    {
        var camera = Camera.main;
        var viewportPosition = camera.WorldToViewportPoint(worldPosition);
        var canvasRect = canvas.GetComponent<RectTransform>();

        var sizeDelta = canvasRect.sizeDelta;
        return new Vector2((viewportPosition.x * sizeDelta.x) - (sizeDelta.x * 0.5f),
            (viewportPosition.y * sizeDelta.y) - (sizeDelta.y * 0.5f));
    }

    public static void LookAt2d(this Transform transform, Transform target)
    {
        var diff = target.position - transform.position;
        transform.rotation = diff.GetRotation(); 
    }

    public static void DestroyAllChildren(this Transform transform)
    {
        if (transform.childCount > 0)
            for (var i = transform.childCount - 1; i >= 0; i--)
                Object.Destroy(transform.GetChild(i).gameObject);
    }

    public static float GetRandomPercent => Random.Range(0, 1f);

    public static T GetRandom<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static Quaternion GetRotation(this Vector3 vector)
    {
        var diff = vector.normalized;
        var rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    public static Transform GetRandomChild(this Transform transform)
    {
        return transform.GetChild(Random.Range(0, transform.childCount));
    }

    public static TKey RandomByWeight<TKey>(this Dictionary<TKey, int> dictionary)
    {
        var weightSum = dictionary.Sum(pair => pair.Value);
        var roll = Random.Range(0, weightSum);
        var skippedWeight = 0;
        foreach (var key in dictionary.Keys)
        {
            skippedWeight += dictionary[key];
            if (skippedWeight >= roll)
                return key;
        }

        return dictionary.FirstOrDefault().Key;
    }
    
    public static bool Contains<T>(this Tuple<T, T> tuple, T a, T b)
    {
        return (tuple.Item1.Equals(a) && tuple.Item2.Equals(b))
               || (tuple.Item1.Equals(b) && tuple.Item2.Equals(a));
    }
}