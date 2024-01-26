using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineResetter : MonoBehaviour
{
    private Transform player;
    public GameObject startPlatformCenter;
    // Start is called before the first frame update
    void Start()
    {
        player = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // if player is too close to the start, we reset the line in levelGrid
        if (Vector3.Distance(player.position, startPlatformCenter.transform.position) < 1.5f){
            GridLab.ResetLine();
        }
    }
}
