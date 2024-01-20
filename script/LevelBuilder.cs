using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public GameObject pillarsParent;
    public const bool SIZE_3x3 = false;
    public const bool SIZE_4x4 = true;

    public int dim;
    private bool levelSize = SIZE_3x3;
    public GameObject origin_4x4;
    public GameObject origin_3x3;
    public GameObject origin_grid_4x4;
    public GameObject origin_grid_3x3;
    public GameObject hexPrefab;
    public GameObject squarePrefab;
    public GameObject sunPrefab;

    public List<Vector2> hexPositions;
    public List<Vector2> squarePositions;
    public List<Vector2> sunPositions;

    private float pillar_offset = 1.17f;
    // z c'est dans le sens sortie (petit) vers entrée (grand)
    // x c'est marcher sur le côté
    
    public GameObject pillarPrefab;

    public GameObject grid3x3Prefab;
    public GameObject grid4x4Prefab;
    public GameObject startPlatform;
    public GameObject endPlatform;
    public int startingX;
    public int endingX;

    void Start()
    {
        if (dim == 3) {
            levelSize = SIZE_3x3;
        } else if (dim == 4) {
            levelSize = SIZE_4x4;
        }
        StartCoroutine(CreateLevelPillars(levelSize));
        CreateGrid(levelSize);
    }

    IEnumerator CreateLevelPillars(bool size) {
        GameObject ref_obj;
        int nbOfPillars;
        if (size == SIZE_3x3) {
            ref_obj = origin_3x3;
            nbOfPillars = 3;
        } else {
            ref_obj = origin_4x4;
            nbOfPillars = 4;
        }
        for (int i=-1; i<=nbOfPillars; i++) {
            for (int j=0; j<=nbOfPillars; j++) {
                if(i==nbOfPillars){
                    if(j==startingX){
                        GameObject newChild = Instantiate(startPlatform, new Vector3(), pillarsParent.transform.rotation);
                        newChild.transform.parent = pillarsParent.transform;
                        newChild.transform.Translate(ref_obj.transform.position + new Vector3(i*pillar_offset, 0f, j*pillar_offset-0.5f*pillar_offset), Space.Self);
                        newChild.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    }
                }
                else if(i==-1){
                    if(j==endingX){
                        GameObject newChild = Instantiate(endPlatform, new Vector3(), pillarsParent.transform.rotation);
                        newChild.transform.parent = pillarsParent.transform;
                        newChild.transform.Translate(ref_obj.transform.position + new Vector3(i*pillar_offset, 0f, j*pillar_offset-0.5f*pillar_offset), Space.Self);
                        newChild.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    }
                }
            }
        }
        for (int i=0; i<nbOfPillars; i++) {
            for (int j=0; j<nbOfPillars; j++) {
                yield return new WaitForSeconds(0.15f);
                // Test if the current position is a sun
                bool instantiated = false;
                GameObject newChild = null;
                
                foreach (Vector2 sunPos in sunPositions)
                {
                    if (sunPos.x == nbOfPillars - i - 1 && sunPos.y == j)
                    {
                        newChild = Instantiate(sunPrefab, new Vector3(), pillarsParent.transform.rotation);
                        instantiated = true;
                        break;
                    }
                }
                // Test if the current position is a square
                if (!instantiated){
                    foreach (Vector2 squarePos in squarePositions)
                    {
                        if (squarePos.x == nbOfPillars - i - 1 && squarePos.y == j)
                        {
                            newChild = Instantiate(squarePrefab, new Vector3(), pillarsParent.transform.rotation);
                            instantiated = true;
                            break;
                        }
                    }
                }
                if(!instantiated){
                    newChild = Instantiate(pillarPrefab, new Vector3(), pillarsParent.transform.rotation);
                }
                newChild.transform.parent = pillarsParent.transform;
                newChild.transform.Translate(ref_obj.transform.position + new Vector3(i*pillar_offset, 0f, j*pillar_offset), Space.Self);
                newChild.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                newChild.GetComponent<HeightInterpolator>().StartInterpolation();
            }
        }
    }

    void CreateGrid(bool size) {
        GameObject ref_obj;
        GameObject newChild;
        if (size == SIZE_3x3) {
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
        
        newChild.GetComponent<GridLab>().startingX = startingX * 2;
        newChild.GetComponent<GridLab>().endingX = endingX * 2;

        newChild.GetComponent<GridLab>().startingY = 0;
        for (int i=0; i<hexPositions.Count; i++){
            newChild.GetComponent<GridLab>().instantiateAt((int)hexPositions[i].x, (int)hexPositions[i].y, hexPrefab);
        }
        
        Debug.Log("grid created");
    }
}
