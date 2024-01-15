using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringObject : MonoBehaviour
{
    public float hoverHeight = 1.0f;
    public float hoverSpeed = 1.0f;
    private bool hovering = false;

    private Vector3 initialPosition;

    public void StartHovering(){
        // Store the initial position of the object
        initialPosition = transform.position;
        hovering = true;
    }

    void Update()
    {
        if (hovering) {
            // Calculate the vertical offset using a sine wave for smooth oscillation
            float verticalOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

            // Apply the hover effect to the object's position
            transform.position = initialPosition + new Vector3(0.0f, verticalOffset, 0.0f);
        }
    }
}
