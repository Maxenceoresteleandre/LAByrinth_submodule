using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnwantedChildrenKiller : MonoBehaviour
{
    private Array<string> tagsToKeep = new Array<string> {"Keep", "Start", "End"};
    public void SendUnwantedChildrenToSpace(){
        foreach (Transform child in transform)
        {
            // if tag is in tagsToKeep, we don't kill it
            if (child.gameObject.tag in tagsToKeep){
                continue;
            }
            child.gameObject.AddComponent<GoDieInSpace>();
        }
    }
}
