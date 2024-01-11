using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringObject : MonoBehaviour
{
    public float hoverHeight = 1.0f;
    public float hoverSpeed = 1.0f;

    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the object
        initialPosition = transform.position;
    }

    void Update()
    {
        // Calculate the vertical offset using a sine wave for smooth oscillation
        float verticalOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

        // Apply the hover effect to the object's position
        transform.position = transform.position + new Vector3(0.0f, verticalOffset, 0.0f);
    }
}
