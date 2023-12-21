using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoVR : MonoBehaviour
{
    public GameObject start;
    public GameObject end;
    public SecurityManager Sm=null;
    void Start()
    {
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
        Debug.Log("start.transform.position");
        Sm.ChangeDefaultTrackedObjectPos(start.transform.position);
        // Sm.ChangeTrackingStatue true false
        //PP statue true false

    }
}
