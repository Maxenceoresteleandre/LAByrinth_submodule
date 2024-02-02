using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoDieInSpace : MonoBehaviour
{
    public float disappearDuration = 3.0f;
    public float endHeight = 50.0f;

    public void Start() {
        // Store the initial and target positions
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y + endHeight, transform.position.z);

        // Start the interpolation
        StartCoroutine(InterpolatePosition(initialPosition, targetPosition, disappearDuration));
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
        Destroy(gameObject);
    }
}
