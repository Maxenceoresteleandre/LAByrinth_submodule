using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FranticRotator : MonoBehaviour
{
    public float rotationSpeed = 500f;
    public float switchInterval = 3f;

    private float timer = 0f;
    private Vector3 currentRotationAxis;

    void Start()
    {
        currentRotationAxis = Random.onUnitSphere;
    }

    void Update()
    {
        // Rotate the object frantically
        transform.Rotate(currentRotationAxis * rotationSpeed * Time.deltaTime);

        // Update the timer
        timer += Time.deltaTime;

        // Check if it's time to switch direction
        if (timer >= switchInterval)
        {
            rotationSpeed = Random.Range(150f, 400f); // Adjust the range based on your preference
            switchInterval = Random.Range(0.5f, 2.0f) + 1.0f;
            currentRotationAxis = Random.onUnitSphere;
            timer = 0f;
        }
    }
}
