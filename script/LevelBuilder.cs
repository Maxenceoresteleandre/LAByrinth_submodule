using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public GameObject pillarsParent;
    public const bool SIZE_3x3 = false;
    public const bool SIZE_4x4 = true;
    public Vector3 min_4x4_pos;
    public Vector3 min_3x3_pos;
    private float pillar_offset = 1.17f;
    // z c'est dans le sens sortie (petit) vers entrée (grand)
    // x c'est marcher sur le côté
    
    public GameObject pillarPrefab;
    public GameObject startPlatform;
    public GameObject endPlatform;

    void Start()
    {
        StartCoroutine(CreateLevelPillars(SIZE_4x4));
        StartCoroutine(CreateStart(2, SIZE_4x4));
        StartCoroutine(CreateEnd(1, SIZE_4x4));
    }

    IEnumerator CreateLevelPillars(bool size) {
        Vector3 initialPos;
        int nbOfPillars;
        if (size == SIZE_3x3) {
            initialPos = min_3x3_pos;
            nbOfPillars = 3;
        } else {
            initialPos = min_4x4_pos;
            nbOfPillars = 4;
        }
        for (int i=0; i<nbOfPillars; i++) {
            for (int j=0; j<nbOfPillars; j++) {
                yield return new WaitForSeconds(1.0f);
                GameObject newChild = Instantiate(pillarPrefab, new Vector3(), Quaternion.identity);
                newChild.transform.parent = pillarsParent.transform;
                newChild.transform.position = initialPos - new Vector3(i*pillar_offset, 0f, j*pillar_offset);
                newChild.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                Debug.Log("pillar["+i.ToString()+","+j.ToString()+"]: "+newChild.transform.position.ToString());
            }
        }
    }

    IEnumerator CreateStart(int position_offset, bool size) {
        float z_pos;
        yield return new WaitForSeconds(15.0f);
        if (size == SIZE_3x3) z_pos = min_3x3_pos.z + 3 * pillar_offset;
        else z_pos = min_4x4_pos.z + 4 * pillar_offset;

        Vector3 pos = new Vector3();
        yield return new WaitForSeconds(15.0f);
        if (size == SIZE_3x3) {
            pos.z = min_3x3_pos.z + 3*pillar_offset;
            pos.y = min_3x3_pos.y;
            pos.x = min_3x3_pos.x + position_offset*pillar_offset;
        } else {
            pos.z = min_4x4_pos.z + 3*pillar_offset;
            pos.y = min_4x4_pos.y;
            pos.x = min_4x4_pos.x + position_offset*pillar_offset;
        }
        AddStartOrEnd(startPlatform, pos);
    }

    IEnumerator CreateEnd(int position_offset, bool size) {
        Vector3 pos = new Vector3();
        yield return new WaitForSeconds(15.0f);
        if (size == SIZE_3x3) {
            pos.z = min_3x3_pos.z - pillar_offset;
            pos.y = min_3x3_pos.y;
            pos.x = min_3x3_pos.x + position_offset*pillar_offset;
        } else {
            pos.z = min_4x4_pos.z - pillar_offset;
            pos.y = min_4x4_pos.y;
            pos.x = min_4x4_pos.x + position_offset*pillar_offset;
        }
        AddStartOrEnd(endPlatform, pos);
    }

    private void AddStartOrEnd(GameObject go, Vector3 pos){
        GameObject newChild = Instantiate(go, new Vector3(), Quaternion.identity);
        newChild.transform.parent = pillarsParent.transform;
        newChild.transform.position = pos;
    }
}
