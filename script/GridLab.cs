using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLab : MonoBehaviour
{
    // initialized at none
    private Tuple<int,int> lastGridPosition = Tuple.New(-1,-1);
    private List<Tuple<int,int>> currentSequenceOfPositions = new List<Tuple<int,int>>();
    private Panel panel;
    public static List<Vector3> playerPosPath = new List<Vector3>();
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
        // Deactivate all cells in the grid except the starting cell
        ActivateStartingCell();
    }

    public bool CheckPathValididy(){
        List<Tuple<int,int>> sequence = stopRecordingSequence();
        int[] result = new PlayerPath(panel, Utils.InvertTupleList(sequence)).isPathValid();
        return ((result[0] + result[1] + result[2]) == 0);
    }

    void Update(){
        if (IsInGrid(Camera.main.transform.position)){
            Tuple<int,int> gridPosition = GetGridPosition(Camera.main.transform.position);
            if (gridPosition.First != lastGridPosition.First || gridPosition.Second != lastGridPosition.Second){
                ActivateNeighboursOnly(gridPosition.First, gridPosition.Second);
                //Debug.Log("Player is in grid cell: " + gridPosition.First + ", " + gridPosition.Second);
                if (gridPosition.First == endingPosition.First && gridPosition.Second == endingPosition.Second){
                    Debug.Log("Player has reached the end of the maze!");
                    currentSequenceOfPositions.Add(gridPosition);
                    List<Tuple<int,int>> sequence = stopRecordingSequence();
                    int[] result = new PlayerPath(panel, Utils.InvertTupleList(sequence)).isPathValid();
                    Debug.Log("Validity: " + result[0] + ", " + result[1] + ", " + result[2]);
                }
                if (gridPosition.First == startingPosition.First && gridPosition.Second == startingPosition.Second){
                    Debug.Log("Player has reached the start of the maze!");
                    // LevelBuilder.InitiatePlayerLine();
                    startRecordingSequence();
                }
                lastGridPosition = gridPosition;
                currentSequenceOfPositions.Add(gridPosition);

                // Update the line renderer
                Vector3 gridWorldPosition = GetGridWorldPosition(Camera.main.transform.position);
                if (gridWorldPosition.y > -100){
                    // ensure the line is erased if the player backtracks
                    if (playerPosPath.Count>1 && gridWorldPosition == (playerPosPath[playerPosPath.Count-2])) {
                        playerPosPath.RemoveAt(playerPosPath.Count-1);
                        // ensure two consecutive points are not the same
                        for (int i=0; i<playerPosPath.Count-1; i++){
                            if (playerPosPath[i] == playerPosPath[i+1]){
                                playerPosPath.RemoveAt(i);
                            }
                        }
                        // reset the line renderer
                        lineRenderer.positionCount = playerPosPath.Count+2;
                        for (int i=0; i<playerPosPath.Count; i++){
                            lineRenderer.SetPosition(i+1, new Vector3(playerPosPath[i].x, 0.1f, playerPosPath[i].z));
                        }
                    } else {
                        playerPosPath.Add(gridWorldPosition);
                    }
                    lineRenderer.positionCount = playerPosPath.Count+2;
                    lineRenderer.SetPosition(playerPosPath.Count, new Vector3(gridWorldPosition.x, 0.1f, gridWorldPosition.z));
                    lineRenderer.SetPosition(playerPosPath.Count+1, new Vector3(Camera.main.transform.position.x, 0.1f, Camera.main.transform.position.z));
                }
            }
        }
    }

    public static void ResetLine(){
        LineRenderer lineRenderer = GameObject.Find("PlayerLine").GetComponent<LineRenderer>();
        playerPosPath.Clear();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(1, new Vector3(Camera.main.transform.position.x, 0.1f, Camera.main.transform.position.z));
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

    public Vector3 GetCellWorldPosition(int x, int y)
    {
        // Return the grid cell that the player is currently in
        // Test for each grid cell in the grid
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform cell = transform.GetChild(i);
            Cell c = cell.GetComponent<Cell>();
            if (c.x == x && c.y == y)
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

    public void SetLastGridPositionNone(){
        lastGridPosition = Tuple.New(-1,-1);
    }

    public void SetPanel(Panel panel){
        this.panel = panel;
    }

    public Panel GetPanel(){
        return panel;
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

    public void ActivateWallFF(int x, int y, bool isTrue){
        for (int i=0; i<transform.childCount; i++){
            Transform cell = transform.GetChild(i);
            Cell c = cell.GetComponent<Cell>();
            if (c.x == x && c.y == y){
                if(y%2==0){
                    cell.Find("Wall").gameObject.SetActive(true);
                }
                else if(isTrue){
                    cell.Find("Shield").gameObject.SetActive(true);
                    GameObject.Find("ForceFieldManager").GetComponent<ForceFieldManager>().AddForceField(cell.position);
                }
                else{
                    cell.Find("FakeShield").gameObject.SetActive(true);
                }
            }
        }
    }

    public void ActivateNeighboursOnly(int x, int y){
        for (int i=0; i<transform.childCount; i++){
            Transform cell = transform.GetChild(i);
            Cell c = cell.GetComponent<Cell>();
            if(!cell.Find("Shield").gameObject.activeSelf && !cell.Find("Wall").gameObject.activeSelf)
            {
                if (c.x == x && c.y == y){
                    cell.gameObject.GetComponent<Collider>().enabled = true;
                } 
                else if (c.x == x-1 && c.y == y){
                    cell.gameObject.GetComponent<Collider>().enabled = true;
                } 
                else if (c.x == x+1 && c.y == y){
                    cell.gameObject.GetComponent<Collider>().enabled = true;
                } 
                else if (c.x == x && c.y == y-1){
                    cell.gameObject.GetComponent<Collider>().enabled = true;
                } 
                else if (c.x == x && c.y == y+1){
                    cell.gameObject.GetComponent<Collider>().enabled = true;
                }
                else if(c.x != startingPosition.First || c.y != startingPosition.Second){
                    cell.gameObject.GetComponent<Collider>().enabled = false;
                }
            }
            else 
            {
                cell.gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }

    public void ActivateStartingCell(){
        for (int i=0; i<transform.childCount; i++){
            Transform cell = transform.GetChild(i);
            Cell c = cell.GetComponent<Cell>();
            if (c.x == startingPosition.First && c.y == startingPosition.Second){
                cell.gameObject.GetComponent<Collider>().enabled = true;
            } else {
                cell.gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }
}