using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineResetter : MonoBehaviour
{
    private Transform player;
    public GameObject startPlatformCenter;
    public bool reloadLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        player = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // if player is too close to the start, we reset the line in levelGrid
        if (startPlatformCenter.GetComponent<Collider>().bounds.Contains(player.position)){
            GameObject.FindWithTag("LevelGrid").GetComponent<GridLab>().SetLastGridPositionNone();
            GridLab.ResetLine();

            if (reloadLevel){
                reloadLevel = false;
                GameObject.Find("LevelBuilder").GetComponent<LevelBuilder>().GenerateLevel();
                UnwantedChildrenKiller uck = GameObject.Find("PillarsParent").GetComponent<UnwantedChildrenKiller>();
                StartCoroutine(uck.KillStartAndEnd());
            }
        }
    }
}
