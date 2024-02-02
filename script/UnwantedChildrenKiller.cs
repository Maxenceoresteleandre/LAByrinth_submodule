using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnwantedChildrenKiller : MonoBehaviour
{
    public GameObject bigGround;
    private List<string> tagsToKeep = new List<string> {"Keep", "Start", "End"};
    public IEnumerator SendUnwantedChildrenToSpace(){
        foreach (Transform child in transform)
        {
            if (IsTagToKeep(child.tag)){
                continue;
            }
            yield return new WaitForSeconds(0.2f);
            child.gameObject.AddComponent<GoDieInSpace>();
        }
        InstantiateGround();
    }

    private void InstantiateGround(){
        Instantiate(bigGround, new Vector3(0f, 0f, 0f), Quaternion.identity);
    }

    public bool IsTagToKeep(string tag){
       for (int i=0; i<tagsToKeep.Count; i++){
            if (tagsToKeep[i] == tag){
                return true;
            }
        }
        return false;
    }
}
