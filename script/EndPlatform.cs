using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlatform : MonoBehaviour
{
    private Transform player;
    public GameObject endPlatformCenter;
    private bool isLevelFinished = false;
    // Start is called before the first frame update
    void Start()
    {
        player = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLevelFinished){
            return;
        }
        // if player is too close to the start, we reset the line in levelGrid
        if (endPlatformCenter.GetComponent<Collider>().bounds.Contains(player.position)){
            isLevelFinished = true;
            UnwantedChildrenKiller uck = GameObject.Find("PillarsParent").GetComponent<UnwantedChildrenKiller>();
            StartCoroutine(uck.SendUnwantedChildrenToSpace());
        }
    }
}
