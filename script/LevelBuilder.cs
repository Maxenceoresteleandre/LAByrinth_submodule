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
                        GameObject newChild = Instantiate(startPlatform, new Vector3(), Quaternion.identity);
                        newChild.transform.parent = pillarsParent.transform;
                        newChild.transform.position = ref_obj.transform.position + new Vector3(i*pillar_offset, 0f, j*pillar_offset-0.5f*pillar_offset);
                        newChild.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    }
                }
                else if(i==-1){
                    if(j==endingX){
                        GameObject newChild = Instantiate(endPlatform, new Vector3(), Quaternion.identity);
                        newChild.transform.parent = pillarsParent.transform;
                        newChild.transform.position = ref_obj.transform.position + new Vector3(i*pillar_offset, 0f, j*pillar_offset-0.5f*pillar_offset);
                        newChild.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    }
                }
            }
        }
        for (int i=0; i<nbOfPillars; i++) {
            for (int j=0; j<nbOfPillars; j++) {
                yield return new WaitForSeconds(0.5f);
                GameObject newChild = Instantiate(pillarPrefab, new Vector3(), Quaternion.identity);
                newChild.transform.parent = pillarsParent.transform;
                newChild.transform.position = ref_obj.transform.position + new Vector3(i*pillar_offset, 0f, j*pillar_offset);
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
            newChild = Instantiate(grid3x3Prefab, new Vector3(), Quaternion.identity);
            newChild.GetComponent<GridLab>().endingY = 6;
            
        } else {
            ref_obj = origin_grid_4x4;
            newChild = Instantiate(grid4x4Prefab, new Vector3(), Quaternion.identity);
            newChild.GetComponent<GridLab>().endingY = 8;
        }
        newChild.transform.parent = pillarsParent.transform;
        newChild.transform.position = ref_obj.transform.position;
        
        newChild.GetComponent<GridLab>().startingX = startingX * 2;
        newChild.GetComponent<GridLab>().endingX = endingX * 2;

        newChild.GetComponent<GridLab>().startingY = 0;
        
        Debug.Log("grid created");
    }
}
