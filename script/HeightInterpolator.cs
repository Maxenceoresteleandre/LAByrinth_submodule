using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class HeightInterpolator : MonoBehaviour
{
    
    public float appearDuration = 3.0f;
    public float startHeight = -15.0f;
    public UnityEvent interpolationFinished;

    public void StartInterpolation() {
        // Store the initial and target positions
        Vector3 initialPosition = new Vector3(transform.position.x, startHeight, transform.position.z);
        Vector3 targetPosition = transform.position;

        // Start the interpolation
        StartCoroutine(InterpolatePosition(initialPosition, targetPosition, appearDuration));
    }

    IEnumerator InterpolatePosition(Vector3 initialPosition, Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the final position is exactly the target position
        transform.position = targetPosition;

        InterpolationFinished();
    }

    void InterpolationFinished()
    {
        interpolationFinished.Invoke();
    }
}
