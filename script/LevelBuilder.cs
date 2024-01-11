using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public const bool SIZE_3x3 = false;
    public const bool SIZE_4x4 = true;
    public Vector3 min_4x4_pos;
    public Vector3 min_3x3_pos;
    
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
        if (size == SIZE_3x3) {

        } else {
            for (int i=0; i<4; i++) {
                Instantiate(pillarPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
    }
}
