using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public const bool SIZE_3x3 = false;
    public const bool SIZE_4x4 = true;
    public Vector3 min_4x4_pos;
    public Vector3 min_3x3_pos;
    public float floor_height;
    public double pillar_offset = 1.17;
    // z c'est dans le sens sortie (petit) vers entrée (grand)
    // x c'est marcher sur le côté
    
    public GameObject pillarPrefab;
    public GameObject startPlatform;
    public GameObject endPlatform;

    void Start()
    {
        CreateLevelPillars(SIZE_4x4);
        CreateStart(2, SIZE_4x4);
        CreateEnd(1, SIZE_4x4);
    }

    public void CreateLevelPillars(bool size) {
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
                Instantiate(pillarPrefab, initialPos + new Vector3(), Quaternion.identity);
            }
        }
    }

    public void CreateStart(int position_offset, bool size) {
        float z_pos;
        if (size == SIZE_3x3) z_pos = min_3x3_pos.z + 3 * pillar_offset;
        else z_pos = min_4x4_pos.z + 4 * pillar_offset;
        AddStartOrEnd(startPlatform, position_offset, z_pos)
    }

    public void CreateEnd(int position_offset, bool size) {
        float z_pos;
        if (size == SIZE_3x3) z_pos = min_3x3_pos.z - pillar_offset;
        else z_pos = min_4x4_pos.z - pillar_offset;
        AddStartOrEnd(endPlatform, position_offset, z_pos)
    }

    private void AddStartOrEnd(GameObject go, int x_pos_offset, float z_position){
        Instantiate(go, new Vector3(x_pos_offset * pillar_offset, floor_height, z_position), Quaternion.identity);
    }
}
