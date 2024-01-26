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


    private float pillar_offset = 1.17f;
    // z c'est dans le sens sortie (petit) vers entrée (grand)
    // x c'est marcher sur le côté
    private PlayerPath solution;
    private Panel panel;

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
        panel.PrintPanel();
        Debug.Log("Level generated!");
        CreateGrid();
        StartCoroutine(CreateLevelPillars());
    }

    IEnumerator CreateLevelPillars() {
        GameObject ref_obj;
        List<Tuple<int, int>> squares = solution.GetPanel().GetSquarePositions();
        List<Tuple<int, int>> suns = solution.GetPanel().GetSunPositions();
        if (dim == 3) {
            ref_obj = origin_3x3;
        } else {
            ref_obj = origin_4x4;
        }
        for (int i=-1; i<=dim; i++) {
            for (int j=0; j<dim; j++) {
                if(i==dim){
                    if(j==(solution.GetPanel().GetStart().Second-1) / 2){
                        GameObject newChild = Instantiate(startPlatform, new Vector3(), pillarsParent.transform.rotation);
                        newChild.transform.parent = pillarsParent.transform;
                        newChild.transform.Translate(ref_obj.transform.position + new Vector3(i*pillar_offset, 0f, j*pillar_offset), Space.Self);
                        newChild.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        InitiatePlayerLine();
                    }
                }
                else if(i==-1){
                    if(j==(solution.GetPanel().GetEnd().Second-1) / 2){
                        GameObject newChild = Instantiate(endPlatform, new Vector3(), pillarsParent.transform.rotation);
                        newChild.transform.parent = pillarsParent.transform;
                        newChild.transform.Translate(ref_obj.transform.position + new Vector3(i*pillar_offset, 0f, j*pillar_offset), Space.Self);
                        newChild.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    }
                }
                else{
                    yield return new WaitForSeconds(0.15f);
                    int pillar_i = i * 2 + 1;
                    int pillar_j = j * 2 + 1;
                    if (squares.Contains(new Tuple<int, int>(pillar_j, pillar_i))) {
                        GameObject newChild = Instantiate(squarePrefabsByColor[solution.GetPanel().GetSymbol(pillar_j, pillar_i).GetColorId()], new Vector3(), pillarsParent.transform.rotation);
                        newChild.transform.parent = pillarsParent.transform;
                        newChild.transform.Translate(ref_obj.transform.position + new Vector3(i*pillar_offset, 0f, j*pillar_offset), Space.Self);
                        newChild.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    } else if (suns.Contains(new Tuple<int, int>(pillar_j, pillar_i))) {
                        GameObject newChild = Instantiate(sunPrefabsByColor[solution.GetPanel().GetSymbol(pillar_j, pillar_i).GetColorId()], new Vector3(), pillarsParent.transform.rotation);
                        newChild.transform.parent = pillarsParent.transform;
                        newChild.transform.Translate(ref_obj.transform.position + new Vector3(i*pillar_offset, 0f, j*pillar_offset), Space.Self);
                        newChild.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    } else {
                        GameObject newChild = Instantiate(pillarPrefab, new Vector3(), pillarsParent.transform.rotation);
                        newChild.transform.parent = pillarsParent.transform;
                        newChild.transform.Translate(ref_obj.transform.position + new Vector3(i*pillar_offset, 0f, j*pillar_offset), Space.Self);
                        newChild.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    }
                }
            }
        }
    }

    private void InitiatePlayerLine() {
        GameObject playerLine = GameObject.Find("PlayerLine");
        playerLine.GetComponent<LineManager>().StartDrawingLine();
    }

    void CreateGrid() {
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

        newChild.GetComponent<GridLab>().startingY = 0;
        foreach(Tuple<int, int> hexPos in solution.GetPanel().GetHexagonPositions()){
            newChild.GetComponent<GridLab>().instantiateAt((int)hexPos.Second, (int)hexPos.First, hexPrefab);
        }
        
        Debug.Log("grid created");
    }
}

[System.Serializable]
    public class ListOfVector2
    {
        public List<Vector2> positions;
    }
