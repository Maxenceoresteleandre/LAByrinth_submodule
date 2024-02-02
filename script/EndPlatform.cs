using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlatform : MonoBehaviour
{
    private Transform player;
    public GameObject endPlatformCenter;
    // Start is called before the first frame update
    void Start()
    {
        player = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // if player is too close to the start, we reset the line in levelGrid
        if (endPlatformCenter.GetComponent<Collider>().bounds.Contains(player.position)){
            UnwantedChildrenKiller uck = GameObject.Find("PillarsParent").GetComponent<UnwantedChildrenKiller>();
            StartCoroutine(uck.SendUnwantedChildrenToSpace());
        }
    }
}
