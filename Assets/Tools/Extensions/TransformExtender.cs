using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class TransformExtender
{
    public static IEnumerator RotateTowards(this Transform transform, Vector3 lookToPoint, float timeBudget, Action onComplete = null)
    {
        Vector3 directionVec = (lookToPoint - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(lookToPoint - transform.position, Vector3.up);
        float startTime = Time.time;
        Quaternion startRotation = transform.rotation;
        while (Time.time < startTime + timeBudget)
        {
            float p = Mathf.Clamp01((Time.time - startTime) / timeBudget);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, p);
            yield return null;
        }
        transform.rotation = targetRotation;
        onComplete?.Invoke();
    }

    public static IEnumerator MoveToPoint(this Transform transform, Vector3 moveToPoint, float speed, float accelration = 100, float maxSpeed = 1000, float rotRate = -1, Action onComplete = null, Action<float> onSpeedSet = null)
    {
        Vector3 directionVec = (moveToPoint - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionVec, Vector3.up);
        Vector3 currentDistVec;
        do
        {

            if (rotRate > 0) transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotRate * Time.deltaTime);

            currentDistVec = moveToPoint - transform.position;

            speed += accelration * Time.deltaTime;
            speed = Mathf.Clamp(speed, 0, maxSpeed);

            transform.Translate(directionVec * speed * Time.deltaTime, Space.World);
            onSpeedSet?.Invoke(speed);
            yield return null;

        } while (currentDistVec.magnitude > 0.2f && Vector3.Dot(currentDistVec, directionVec) > 0);
        speed = 0;
        onSpeedSet?.Invoke(speed);
        onComplete?.Invoke();
    }

    public static IEnumerator SteadyWalk(this Transform transform, Vector3 pointToMove, float speed, float rotationLerpRate = -1)
    {
        Vector3 directionVec = (pointToMove - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionVec, Vector3.up);
        Vector3 currentDistVec;
        do
        {

            if (rotationLerpRate > 0) transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationLerpRate * Time.deltaTime);

            currentDistVec = pointToMove - transform.position;

            transform.Translate(directionVec * speed * Time.deltaTime, Space.World);
            yield return null;

        } while (currentDistVec.magnitude > 0.2f && Vector3.Dot(currentDistVec, directionVec) > 0);
    }


    public static IEnumerator ReTransform(this Transform transform, Transform target, float timeBudget, bool doPosition = true, bool doRotation = true, bool doScale = false, Action onComplete = null)
    {
        Quaternion startRotation = transform.rotation;
        Vector3 startPosition = transform.position;
        Vector3 startScale = transform.localScale;

        float startTime = Time.time;
        while (Time.time < startTime + timeBudget)
        {
            float p = Mathf.Clamp01((Time.time - startTime) / timeBudget);
            if (doRotation) transform.rotation = Quaternion.Lerp(startRotation, target.rotation, p);
            if (doPosition) transform.position = Vector3.Lerp(startPosition, target.position, p);
            if (doScale) transform.localScale = Vector3.Lerp(startScale, target.localScale, p);
            yield return null;
        }
        if (doRotation) transform.rotation = target.rotation;
        if (doPosition) transform.position = target.position;
        if (doScale) transform.localScale = target.localScale;
        onComplete?.Invoke();
    }


}