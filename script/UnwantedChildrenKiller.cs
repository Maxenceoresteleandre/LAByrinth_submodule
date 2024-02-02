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

    public IEnumerator KillStartAndEnd(){
        foreach (Transform child in transform)
        {
            if (child.tag == "Start" || child.tag == "End"){
                child.gameObject.AddComponent<GoDieInSpace>();
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    private void InstantiateGround(){
        GameObject ground = Instantiate(bigGround, new Vector3(2.5f, -0.3f, 0f), Quaternion.identity);
        ground.transform.parent = GameObject.Find("PillarsParent").transform;
        ground.GetComponent<HeightInterpolator>().StartInterpolation();
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
