using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public const bool SIZE_3x3 = false;
    public const bool SIZE_4x4 = true;
    public Vector3 min_4x4_pos;
    public Vector3 min_3x3_pos;
    public double pillar_offset_x;
    public double pillar_offset_z;
    public float end_pos_z_4x4;
    public float end_pos_z_3x3;
    public float start_pos_z;
    public float start_min_x_pos;
    public float end_min_x_pos;
    
    public GameObject pillarPrefab;
    public GameObject startPlatform;
    public GameObject endPlatform;

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

    public void CreateStart(int position_offset) {

    }

    private void AddStartOrEnd(GameObject go, int pos_)
}
