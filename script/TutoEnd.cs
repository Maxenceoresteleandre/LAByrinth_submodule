using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoEnd : MonoBehaviour
{
    public GameObject platformEnd;

    void Update()
    {
        if (platformEnd.GetComponent<Collider>().bounds.Contains(Camera.main.transform.position))
        {
            EndTuto();
        }        
    }

    private IEnumerator EndTuto()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.AddComponent<GoDieInSpace>();
            yield return new WaitForSeconds(0.2f);  
        }
        yield return new WaitForSeconds(0.8f);
        GameObject.Find("LevelBuilder").GetComponent<LevelBuilder>().GenerateLevel();  
    }
}
