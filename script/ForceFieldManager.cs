using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldManager : MonoBehaviour
{
    public Vector3 targetPosition;
    public SecurityManager Sm=null;
    public List<Vector3> obstaclePositions = new List<Vector3>();
    private Vector3 doorPosition;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject door = GameObject.FindGameObjectWithTag("TangibleDoor");
        doorPosition = door.transform.position;
        GameObject secu = GameObject.FindGameObjectWithTag("ColumControl");
        if (secu != null)
        {
            Sm = secu.GetComponent<SecurityManager>();
        }
        else
        {
            Debug.Log("can't find security controller");
        }
    }

    private Vector3 FindClosestObstacle()
    {
        float minDistance = Mathf.Infinity;
        Vector3 playerPos = player.transform.position;
        Vector3 closestObstacle = Vector3.zero;
        for (int i = 0; i < obstaclePositions.Count; i++)
        {
            float distance = Vector3.Distance(obstaclePositions[i], playerPos);
            if (distance < minDistance)
            {
                minDistance = distance;
            }
        }
        return closestObstacle;
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = FindClosestObstacle();
        Sm.ChangeDefaultTrackedObjectPos(targetPosition);
    }
}
