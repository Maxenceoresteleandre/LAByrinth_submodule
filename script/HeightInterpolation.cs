using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightInterpolation : MonoBehaviour
{
    private bool isMoving = false;
    private float startY;
    private float targetY;
    private float startTime;
    private float duration;

    void Start()
    {
        // Start the interpolation when the script is initialized
        StartInterpolation();
    }

    void Update()
    {
        if (isMoving)
        {
            // Calculate the current progress of the interpolation
            float progress = (Time.time - startTime) / duration;

            // Linear interpolation for smooth movement
            float newY = Mathf.Lerp(startY, targetY, progress);

            // Apply easing towards the end of the interpolation
            float easedProgress = Mathf.SmoothStep(0f, 1f, Mathf.InverseLerp(0.9f, 1f, progress));
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(startY, targetY, easedProgress), transform.position.z);

            // Check if the interpolation is complete
            if (progress >= 1.0f)
            {
                isMoving = false;
            }
        }
    }

    public void StartInterpolation(float targetHeight)
    {
        targetY = targetHeight;
        InterpolationInit();
    }

    public void StartInterpolation()
    {
        targetY = transform.position.y;
        InterpolationInit();
    }

    private void InterpolationInit()
    {
        startY = Random.Range(1000.0f, 15000.0f);

        // Set random duration between 2.0 and 3.5 seconds
        duration = Random.Range(2.0f, 3.5f);

        // Store the initial height and start time
        startTime = Time.time;

        // Set the flag to indicate that the object is moving
        isMoving = true;
    }
}
