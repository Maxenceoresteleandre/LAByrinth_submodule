using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLab : MonoBehaviour
{
    // initialized at none
    private Tuple<int,int> lastGridPosition = Tuple.New(-1,-1);
    private List<Tuple<int,int>> currentSequenceOfPositions = new List<Tuple<int,int>>();
    public List<Vector3> playerPath = new List<Vector3>();
    public GameObject playerLine;
    private LineRenderer lineRenderer;
    
    private Tuple<int,int> startingPosition;
    private Tuple<int,int> endingPosition;

    public int startingX;
    public int startingY;
    public int endingX;
    public int endingY;
    
    void Start()
    {
        startingPosition = Tuple.New(startingX, startingY);
        endingPosition = Tuple.New(endingX, endingY);
        playerLine = GameObject.Find("PlayerLine");
        lineRenderer = playerLine.GetComponent<LineRenderer>();
    }

    void Update(){
        if (IsInGrid(Camera.main.transform.position)){
            Tuple<int,int> gridPosition = GetGridPosition(Camera.main.transform.position);
            if (gridPosition.First != lastGridPosition.First || gridPosition.Second != lastGridPosition.Second){
                Debug.Log("Player is in grid cell: " + gridPosition.First + ", " + gridPosition.Second);
                if (gridPosition.First == endingPosition.First && gridPosition.Second == endingPosition.Second){
                    Debug.Log("Player has reached the end of the maze!");
                    List<Tuple<int,int>> sequence = stopRecordingSequence();
                    string sequenceString = "";
                    foreach (Tuple<int,int> position in sequence){
                        sequenceString += position.First + "," + position.Second + ";";
                    }
                    Debug.Log("Player's sequence of positions: " + sequenceString);
                }
                if (gridPosition.First == startingPosition.First && gridPosition.Second == startingPosition.Second){
                    Debug.Log("Player has reached the start of the maze!");
                    startRecordingSequence();
                }
                lastGridPosition = gridPosition;
                currentSequenceOfPositions.Add(gridPosition);

                // Update the line renderer
                Vector3 gridWorldPosition = GetGridWorldPosition(Camera.main.transform.position);
                if (playerPath.Count>8){
                    Debug.Log("gridWordlPosition = " + gridWorldPosition.ToString());
                    Debug.Log("playerPath[Count-1] = " + playerPath[playerPath.Count-1].ToString());
                    Debug.Log("playerPath[Count-2] = " + playerPath[playerPath.Count-2].ToString());
                    Debug.Log("playerPath[Count-3] = " + playerPath[playerPath.Count-3].ToString());
                    Debug.Log("playerPath[Count-4] = " + playerPath[playerPath.Count-4].ToString());
                    Debug.Log("playerPath[Count-5] = " + playerPath[playerPath.Count-5].ToString());
                    Debug.Log("playerPath[Count-6] = " + playerPath[playerPath.Count-6].ToString());
                }
                if (gridWorldPosition.y > -100){
                    // ensure the line is erased if the player backtracks
                    if (playerPath.Count>1 && gridWorldPosition == (playerPath[playerPath.Count-2])) {
                        playerPath.RemoveAt(playerPath.Count-1);
                        // ensure two consecutive points are not the same
                        for (int i=0; i<playerPath.Count-1; i++){
                            if (playerPath[i] == playerPath[i+1]){
                                playerPath.RemoveAt(i);
                            }
                        }
                        // reset the line renderer
                        lineRenderer.positionCount = playerPath.Count+2;
                        for (int i=0; i<playerPath.Count; i++){
                            lineRenderer.SetPosition(i+1, new Vector3(playerPath[i].x, 0.1f, playerPath[i].z));
                        }
                    } else {
                        playerPath.Add(gridWorldPosition);
                    }
                    lineRenderer.positionCount = playerPath.Count+2;
                    lineRenderer.SetPosition(playerPath.Count, new Vector3(gridWorldPosition.x, 0.1f, gridWorldPosition.z));
                }
            }
        }
    }

    public Vector3 GetGridWorldPosition(Vector3 position)
    {
        // Return the grid cell that the player is currently in
        // Test for each grid cell in the grid
        for (int x = 0; x < transform.childCount; x++)
        {
            Transform cell = transform.GetChild(x);
            if (cell.GetComponent<Collider>().bounds.Contains(position))
            {
                return cell.position;
            }
        }
        return new Vector3(0, -999, 0);
    }

    public Tuple<int,int> GetGridPosition(Vector3 position)
    {
        // Return the grid cell that the player is currently in
        // Test for each grid cell in the grid
        for (int x = 0; x < transform.childCount; x++)
        {
            Transform cell = transform.GetChild(x);
            if (cell.GetComponent<Collider>().bounds.Contains(position))
            {
                // Return the grid cell's position
                Cell c = cell.GetComponent<Cell>();
                return Tuple.New(c.x, c.y);
            }
        }
        return Tuple.New(-1, -1);
    }
    public bool IsInGrid(Vector3 position)
    {
        // Return the grid cell that the player is currently in
        // Test for each grid cell in the grid
        for (int x = 0; x < transform.childCount; x++)
        {
            Transform cell = transform.GetChild(x);
            if (cell.GetComponent<Collider>().bounds.Contains(position))
            {
                // Return the grid cell's position
                return true;
            }
        }
        return false;
    }

    public void startRecordingSequence(){
        currentSequenceOfPositions.Clear();
    }
    public List<Tuple<int,int>> stopRecordingSequence(){
        return currentSequenceOfPositions;
    }

    public void instantiateAt(int x, int y, GameObject prefab){
        for (int i=0; i<transform.childCount; i++){
            Transform cell = transform.GetChild(i);
            Cell c = cell.GetComponent<Cell>();
            if (c.x == x && c.y == y){
                GameObject newChild = Instantiate(prefab, new Vector3(), Quaternion.identity);
                newChild.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                newChild.transform.parent = cell.transform;
                newChild.transform.position = cell.transform.position;
            }
        }
    }
}