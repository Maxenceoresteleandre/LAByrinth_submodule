using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = Camera.main.transform.position;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, new Vector3(playerPosition.x, 0.1f, playerPosition.z));
    }
}
