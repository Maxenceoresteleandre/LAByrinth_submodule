using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldManager : MonoBehaviour
{
    private Vector3 targetPosition;
    private SecurityManager Sm=null;
    private List<Vector3> obstaclePositions = new List<Vector3>();
    public GameObject door;
    private GameObject player;
    public GameObject dummyToMove;
    private bool isClosestObstacleDoor = false;
    public Vector3 doorClosePose;
    public Vector3 doorOpenPose;
    private bool computingForceFields = false;

    void Start()
    {
        obstaclePositions.Clear();
        PrintObstacles("VERYSTART");
    }

    public void StartComputingForceFields()
    {
        iTween.Defaults.easeType = iTween.EaseType.easeInOutQuad;
        player = GameObject.Find("Player");
        Debug.Log("player = " + player.ToString());
        door = GameObject.FindGameObjectWithTag("TangibleDoor");
        Debug.Log("door = " + door.ToString());

        GameObject secu = GameObject.FindGameObjectWithTag("ColumControl");
        if (secu != null)
        {
            Sm = secu.GetComponent<SecurityManager>();
            Debug.Log("found security controller");
        }
        else
        {
            Debug.Log("can't find security controller");
        }
        computingForceFields = true;
        PrintObstacles("Start");
    }

    private void PrintObstacles(string prefix){
        string str = prefix;
        for (int i = 0; i < obstaclePositions.Count; i++)
        {
            str += " ; obstaclePositions[" + i + "]=" + obstaclePositions[i].ToString();
        }
        Debug.Log(str);
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
        Vector3 doorPos = door.transform.position;
        float distanceToDoor = Vector3.Distance(new Vector3(doorPos.x, 0f, doorPos.z), playerPos);
        if (distanceToDoor < minDistance)
        {
            closestObstacle = door.transform.position;
            isClosestObstacleDoor = true;
        }
        else
        {
            closestObstacle = obstaclePositions[i];
            isClosestObstacleDoor = false;
        }
        return closestObstacle;
    }

    public void RemoveAllForceFields()
    {
        obstaclePositions.Clear();
    }

    public void AddForceField(Vector3 position)
    {
        obstaclePositions.Add(new Vector3(position.x, 0f, position.z));
        PrintObstacles("AddForceField");
    }

    // Update is called once per frame
    void Update()
    {
        if (! computingForceFields)
        {
            return;
        }
        targetPosition = FindClosestObstacle();
        Sm.ChangeDefaultTrackedObjectPos(targetPosition);
        // move dummy to targetPosition
        if (dummyToMove != null && targetPosition != dummyToMove.transform.position)
        {
            dummyToMove.transform.position = targetPosition;
            Debug.Log("dummyToMove.transform.position = " + dummyToMove.transform.position.ToString());
        }
    }
}