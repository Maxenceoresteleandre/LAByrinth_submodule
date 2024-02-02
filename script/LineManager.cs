using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Material validLineMaterial;
    public Material invalidLineMaterial;
    public bool updatingLine = true;
    public bool isLineValid = true;

    public void StartDrawingLine(GameObject startBlock)
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        Vector3 startBlockPosition = startBlock.transform.position;
        lineRenderer.SetPosition(0, new Vector3(startBlockPosition.x, 0.15f, startBlockPosition.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (lineRenderer != null) {
            if (! updatingLine){
                return;
            }
            Vector3 playerPosition = Camera.main.transform.position;
            // Debug.Log(lineRenderer == null);
            lineRenderer.SetPosition(lineRenderer.positionCount-1, new Vector3(playerPosition.x, 0.15f, playerPosition.z));
            if (DoesLineIntersect()){
                isLineValid = false;
                lineRenderer.material = invalidLineMaterial;
            } else {
                isLineValid = true;
                lineRenderer.material = validLineMaterial;
            }
            Vector3[] newPos = new Vector3[lineRenderer.positionCount];
            //Debug.Log("line positions = " + lineRenderer.GetPositions(newPos).ToString());
        }
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
