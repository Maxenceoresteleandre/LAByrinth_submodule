using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldManager : MonoBehaviour
{
    public GameObject target;
    public SecurityManager Sm=null;
    public List<Vector3> obstaclePositions = new List<Vector3>();
    private Vector3 doorPosition;

    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        Sm.ChangeDefaultTrackedObjectPos(target.transform.position);
    }
}
