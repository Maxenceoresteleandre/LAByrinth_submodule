using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Material validLineMaterial;
    public Material invalidLineMaterial;
    private GameObject startBlock;
    public bool updatingLine = false;

    void StartDrawingLine()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        startBlock = GameObject.Find("StartPlateform");
        Vector3 startBlockPosition = startBlock.transform.position;
        lineRenderer.SetPosition(0, new Vector3(startBlockPosition.x, 0.1f, startBlockPosition.z));
        updatingLine = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (! updatingLine){
            return;
        }
        Vector3 playerPosition = Camera.main.transform.position;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, new Vector3(playerPosition.x, 0.1f, playerPosition.z));
        if (DoesLineIntersect()){
            lineRenderer.material = invalidLineMaterial;
        } else {
            lineRenderer.material = validLineMaterial;
        }
        //Debug.Log(lineRenderer.positions);
    }

    private bool DoesLineIntersect(){
        for (int i=0; i<lineRenderer.positionCount-1; i++){
            for (int j=i+1; j<lineRenderer.positionCount-1; j++){
                if (lineRenderer.GetPosition(i) == lineRenderer.GetPosition(j)){
                    return true;
                }
            }
        }
        return false;
    }
}
