using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float movementSpeed = 1.0f;
    private float rotationSpeed = 15.0f;

    private bool isMouseButtonDown = false;
    private Vector3 lastMousePosition;
    private bool isShiftKeyDown = false;

    void Update()
    {
        HandleMovementInput();
        HandleMouseRotationInput();
    }

    void HandleMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Use only the horizontal component of the camera's forward vector
        Vector3 forward = Vector3.Scale(transform.forward, new Vector3(1, 0, 1)).normalized;

        // Calculate movement direction relative to the world forward vector
        Vector3 movement = (vertical * Vector3.forward + horizontal * Vector3.right).normalized;

        GameObject.Find("Player").transform.Translate(movement * movementSpeed * Time.deltaTime);
        if (Input.GetKeyUp(KeyCode.Z) && Input.GetKeyUp(KeyCode.S) && Input.GetKeyUp(KeyCode.Q) && Input.GetKeyUp(KeyCode.D))
        {
             GameObject.Find("Player").transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
        }
    }

    void HandleMouseRotationInput()
    {
        if (Input.GetMouseButtonDown(2))
        {
            isMouseButtonDown = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(2))
        {
            isMouseButtonDown = false;
        }


        if (isMouseButtonDown && !isShiftKeyDown)
        {
            Vector3 deltaMouse = Input.mousePosition;

            // Rotate horizontally
            float horizontalRotation = (deltaMouse.x - lastMousePosition.x) * rotationSpeed * Time.deltaTime;
            Quaternion horizontalQuaternion = Quaternion.Euler(0, horizontalRotation, 0);
            GameObject.Find("Player").transform.Rotate(Vector3.up, horizontalRotation);

            // Rotate vertically
            float verticalRotation = (deltaMouse.y - lastMousePosition.y) * rotationSpeed * Time.deltaTime;
            Quaternion verticalQuaternion = Quaternion.Euler(-verticalRotation, 0, 0);

            // Apply vertical rotation to the child camera object
            Transform cameraTransform = Camera.main.transform;
            cameraTransform.Rotate(Vector3.right, -verticalRotation);

            lastMousePosition = deltaMouse;
        }
    }

}