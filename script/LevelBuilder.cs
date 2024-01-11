using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public const bool SIZE_3x3 = false;
    public const bool SIZE_4x4 = true;
    public Vector3 min_4x4_pos;
    public Vector3 min_3x3_pos;
    public double pillar_size_x;
    public double pillar_size_z;
    
    public GameObject pillarPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createLevelPillars(bool size) {
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
}
