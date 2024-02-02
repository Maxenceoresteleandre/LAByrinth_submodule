using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldManager : MonoBehaviour
{
    private Vector3 targetPosition;
    private SecurityManager Sm=null;
    public List<Vector3> obstaclePositions = new List<Vector3>();
    public GameObject door;
    private Vector3 doorPosition;
    private GameObject player;
    public GameObject dummyToMove;
    private bool isClosestObstacleDoor = false;
    public Vector3 doorClosePose;
    public Vector3 doorOpenPose;

    void Start()
    {
        iTween.Defaults.easeType = iTween.EaseType.easeInOutQuad;
        player = GameObject.FindGameObjectWithTag("Player");
        door = GameObject.FindGameObjectWithTag("TangibleDoor");
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

    public void RemoveAllForceFields()
    {
        obstaclePositions.Clear();
        obstaclePositions.Insert(0, doorPosition);
    }

    public void AddForceField(Vector3 position)
    {
        obstaclePositions.Add(position);
        for (int i = 0; i < obstaclePositions.Count; i++)
        {
            Debug.Log("obstaclePositions[" + i + "] = " + obstaclePositions[i].ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = FindClosestObstacle();
        Sm.ChangeDefaultTrackedObjectPos(targetPosition);
        // move dummy to targetPosition
        if (dummyToMove != null)
        {
            dummyToMove.transform.position = targetPosition;
            Debug.Log("dummyToMove.transform.position = " + dummyToMove.transform.position.ToString());
        }
    }
}