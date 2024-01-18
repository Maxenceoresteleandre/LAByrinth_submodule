using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldManager : MonoBehaviour
{
    private Vector3 targetPosition;
    private SecurityManager Sm=null;
    public List<Vector3> obstaclePositions = new List<Vector3>();
    private Vector3 doorPosition;
    private GameObject player;
    private bool isClosestObstacleDoor = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject door = GameObject.FindGameObjectWithTag("TangibleDoor");
        doorPosition = door.transform.position;

        obstaclePositions.Insert(0, doorPosition);

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
        int i;
        for (i = 0; i < obstaclePositions.Count; i++)
        {
            float distance = Vector3.Distance(obstaclePositions[i], playerPos);
            if (distance < minDistance)
            {
                minDistance = distance;
            }
        }
        isClosestObstacleDoor = (i == 0);
        return closestObstacle;
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = FindClosestObstacle();
        Sm.ChangeDefaultTrackedObjectPos(targetPosition);
        if (isClosestObstacleDoor)
        {
            // passer en mode "locked" ou "can_open" selon l'état de la porte
        } else {
            // passer en mode "locked"
        }
    }
}
