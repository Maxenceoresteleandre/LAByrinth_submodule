using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelBuilder : MonoBehaviour
{
    public const bool SIZE_3x3 = false;
    public const bool SIZE_4x4 = true;

    public const int BLUE = 0;
    public const int RED = 1;
    public const int WHITE = 2;
    public const int YELLOW = 3;

    public int dim;
    public int nWalls;
    public int nFakeWalls;
    public int nHexagon;
    public List<int> nSquareByColor;
    public List<int> nSunByColor;
    public GameObject pillarsParent;
    public GameObject origin_4x4;
    public GameObject origin_3x3;
    public GameObject origin_grid_4x4;
    public GameObject origin_grid_3x3;
    public GameObject pillarPrefab;
    public GameObject hexPrefab;
    public List<GameObject> squarePrefabsByColor;
    public List<GameObject> sunPrefabsByColor;
    public GameObject grid3x3Prefab;
    public GameObject grid4x4Prefab;
    public GameObject startPlatform;
    public GameObject endPlatform;
    public GameObject solutionLine;
    private GridLab gridLevel;


    private float pillar_offset = 1.17f;
    // z c'est dans le sens sortie (petit) vers entrée (grand)
    // x c'est marcher sur le côté
    private PlayerPath solution;
    private Panel panel;
    public RunBKT runBKT;
    public MapGenerator mapGenerator;

    void Start()
    {
        // Generate a random level
        System.Console.SetOut(new DebugLogWriter());
        Debug.Log("Generating a random level...");
        if(nSquareByColor.Count != nSunByColor.Count){
            throw new System.ArgumentException("The number of colors for squares and suns must be the same");
        }
        try{
            solution = Generator.GenerateLevel(dim, dim, nWalls, nHexagon, nSquareByColor.Count, nSquareByColor, nSunByColor);
        }
        catch(System.Exception e){
            Debug.Log(e.Message);
            Debug.Log("Level generation failed!");
            return;
        }
        panel = solution.GetPanel();
        // panel.WriteToFile(Application.dataPath + "/demoScene/LAByrinth/Levels/level1.txt");
        // Debug.Log(panel.GetStart().Second + ", " + panel.GetStart().First);
        // Debug.Log(panel.GetEnd().Second + ", " + panel.GetEnd().First);
        //Debug.Log("Level generated!");
        gridLevel = CreateGrid();
        Vector3[] solPoints = new Vector3[solution.GetPoints().Count];
        for (int i = 0; i < solution.GetPoints().Count; i++)
        {
            // Debug.Log("solPoint: " + solution.GetPoints()[i].Second + ", " + solution.GetPoints()[i].First);
            solPoints[i] = gridLevel.GetCellWorldPosition(solution.GetPoints()[i].Second, solution.GetPoints()[i].First) + new Vector3(0f, 0.15f, 0f);
            // Debug.Log("solPoint in world: " + solPoints[i]);
        }
        solutionLine.GetComponent<LineRenderer>().positionCount = solPoints.Length;
        solutionLine.GetComponent<LineRenderer>().SetPositions(solPoints);
        StartCoroutine(CreateLevelPillars());
    }

    IEnumerator CreateLevelPillars() {
        ForceFieldManager ffm = GameObject.Find("ForceFieldManager").GetComponent<ForceFieldManager>();
        //ffm.RemoveAllForceFields();
        GameObject ref_obj;
        List<Tuple<int, int>> squares = solution.GetPanel().GetSquarePositions();
        List<Tuple<int, int>> suns = solution.GetPanel().GetSunPositions();
        // foreach(Tuple<int, int> square in squares){
        //     Debug.Log("square at " + square.Second + ", " + square.First);
        // }
        // foreach(Tuple<int, int> sun in suns){
        //     Debug.Log("sun at " + sun.Second + ", " + sun.First);
        // }
        if (dim == 3) {
            ref_obj = origin_3x3;
        } else {
            ref_obj = origin_4x4;
        }
        for (int i=-1; i<=dim; i++) {
            for (int j=0; j<dim; j++) {
                if(i==dim){
                    int pillar_j = dim * 2 - (j * 2);
                    if(pillar_j==solution.GetPanel().GetStart().Second){
                        GameObject newChild = Instantiate(startPlatform, new Vector3(), pillarsParent.transform.rotation);
                        newChild.transform.parent = pillarsParent.transform;
                        newChild.transform.Translate(ref_obj.transform.position + new Vector3(i*pillar_offset, 0f, j*pillar_offset - 0.5f*pillar_offset), Space.Self);
                        newChild.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        GameObject.Find("Player").transform.position = newChild.transform.position;
                        InitiatePlayerLine(newChild);
                    }
                }
                else if(i==-1){
                    int pillar_j = dim * 2 - (j * 2);
                    if(pillar_j==solution.GetPanel().GetEnd().Second){
                        GameObject newChild = Instantiate(endPlatform, new Vector3(), pillarsParent.transform.rotation);
                        newChild.transform.parent = pillarsParent.transform;
                        newChild.transform.Translate(ref_obj.transform.position + new Vector3(i*pillar_offset, 0f, j*pillar_offset - 0.5f*pillar_offset), Space.Self);
                        newChild.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    }
                }
                else{
                    yield return new WaitForSeconds(0.2f);
                    int pillar_i = dim * 2 - (j * 2 + 1);
                    int pillar_j = dim * 2 - (i * 2 + 1);
                    // Debug.Log("pillar at " + pillar_j + ", " + pillar_i);
                    GameObject newChild;
                    if (squares.Contains(new Tuple<int, int>(pillar_j, pillar_i))) {
                        newChild = Instantiate(squarePrefabsByColor[solution.GetPanel().GetSymbol(pillar_j, pillar_i).GetColorId()], new Vector3(), pillarsParent.transform.rotation);
                    } else if (suns.Contains(new Tuple<int, int>(pillar_j, pillar_i))) {
                        newChild = Instantiate(sunPrefabsByColor[solution.GetPanel().GetSymbol(pillar_j, pillar_i).GetColorId()], new Vector3(), pillarsParent.transform.rotation);
                    } else {
                        newChild = Instantiate(pillarPrefab, new Vector3(), pillarsParent.transform.rotation);
                    }
                    newChild.transform.parent = pillarsParent.transform;
                    newChild.transform.Translate(ref_obj.transform.position + new Vector3(i*pillar_offset, 0f, j*pillar_offset), Space.Self);
                    newChild.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    newChild.GetComponent<HeightInterpolator>().StartInterpolation();
                }
            }
        }
        ffm.StartComputingForceFields();
        Debug.Log("gridLevel = " + gridLevel.ToString());
        Debug.Log("endDoor = " + GameObject.FindObjectOfType<EndDoor>().ToString());
        GameObject.FindObjectOfType<EndDoor>().AddGridLab(gridLevel);
        mapGenerator.generateMap(this, panel);
        runBKT.runBKTwithPython(0,1,0);
    }
    public static void InitiatePlayerLine(GameObject startBlock) {
        GameObject playerLine = GameObject.Find("PlayerLine");
        playerLine.GetComponent<LineManager>().StartDrawingLine(startBlock);
    }

    public Panel GetPanel(){
        return panel;
    }

    private GridLab CreateGrid() {
        GameObject ref_obj;
        GameObject newChild;
        if (dim == 3) {
            ref_obj = origin_grid_3x3;
            newChild = Instantiate(grid3x3Prefab, new Vector3(), pillarsParent.transform.rotation);
            newChild.GetComponent<GridLab>().endingY = 6;
            
        } else {
            ref_obj = origin_grid_4x4;
            newChild = Instantiate(grid4x4Prefab, new Vector3(), pillarsParent.transform.rotation);
            newChild.transform.Translate(new Vector3(0.264f, 0.0f, -0.738f ), Space.Self);
            newChild.GetComponent<GridLab>().endingY = 8;
        }

        newChild.transform.parent = pillarsParent.transform;
        newChild.transform.Translate(ref_obj.transform.position, Space.Self);
        
        newChild.GetComponent<GridLab>().startingX = solution.GetPanel().GetStart().Second;
        newChild.GetComponent<GridLab>().endingX = solution.GetPanel().GetEnd().Second;
        newChild.GetComponent<GridLab>().SetPanel(panel);
        newChild.GetComponent<GridLab>().startingY = 0;
        foreach(Tuple<int, int> hexPos in solution.GetPanel().GetHexagonPositions()){
            // Debug.Log("hexagon at " + hexPos.Second + ", " + hexPos.First);
            newChild.GetComponent<GridLab>().instantiateAt((int)hexPos.Second, (int)hexPos.First, hexPrefab);
        }

        List<Tuple<int, int>> allwalls = solution.GetPanel().GetWallPositions();
        foreach(Tuple<int, int> wallPos in allwalls){
            // Debug.Log("wall at " + wallPos.Second + ", " + wallPos.First);
            newChild.GetComponent<GridLab>().ActivateWallFF((int)wallPos.Second, (int)wallPos.First, true);
        }
        // Create a List with all possible wall positions such as y%2==1
        List<Tuple<int, int>> wallPositions = new List<Tuple<int, int>>();
        for (int i = 0; i < dim * 2 + 1; i++)
        {
            for (int j = 0; j < dim * 2 + 1; j++)
            {
                if (i % 2 == 0 && j % 2 == 1)
                {
                    Tuple<int, int> nwallPos = new Tuple<int, int>(i, j);
                    if (!allwalls.Contains(nwallPos.Invert()))
                    {
                        wallPositions.Add(nwallPos);
                    }
                }
            }
        }
        // Randomly select nFakeWalls positions from the List
        List<Tuple<int, int>> fakeWallPositions = new List<Tuple<int, int>>();
        for (int i = 0; i < nFakeWalls; i++)
        {
            if (wallPositions.Count != 0){
                int index = Random.Range(0, wallPositions.Count);
                fakeWallPositions.Add(wallPositions[index]);
                newChild.GetComponent<GridLab>().ActivateWallFF((int)wallPositions[index].First, (int)wallPositions[index].Second, false);
                wallPositions.RemoveAt(index);
            }
        }

        // Create a List with all
        return newChild.GetComponent<GridLab>();
    }
}

[System.Serializable]
    public class ListOfVector2
    {
        public List<Vector2> positions;
    }
