using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineResetter : MonoBehaviour
{
    private Transform player;
    public GameObject startPlatformCenter;
    public bool reloadLevel = false;
    private bool isLevelFinished = false;
    private LevelSequencer levelSequencer;

    // Start is called before the first frame update
    void Start()
    {
        player = Camera.main.transform;
        
        levelSequencer = GameObject.Find("LevelSequencer").GetComponent<LevelSequencer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLevelFinished){
            return;
        }
        // if player is too close to the start, we reset the line in levelGrid
        if (startPlatformCenter.GetComponent<Collider>().bounds.Contains(player.position)){
            if (reloadLevel){
                isLevelFinished = true;
                CreateNewLevel();
            } else {
                GameObject.FindWithTag("LevelGrid").GetComponent<GridLab>().SetLastGridPositionNone();
                GridLab.ResetLine();
            }
        }
    }

    void CreateNewLevel(){
        UnwantedChildrenKiller uck = GameObject.Find("PillarsParent").GetComponent<UnwantedChildrenKiller>();
        StartCoroutine(uck.KillStartAndEnd());
        //StartCoroutine(GameObject.Find("LevelBuilder").GetComponent<LevelBuilder>().DelayedGenerateLevel());
        StartCoroutine(levelSequencer.DelayedNextLevel());
    }
}
