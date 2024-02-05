using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoEnd : MonoBehaviour
{
    public GameObject platformEnd;

    void Start()
    {
        GameObject.Find("Player").transform.position = new Vector3(2.768f,0.386f,3f);
        GameObject.Find("Player").transform.rotation = Quaternion.Euler(0f, 180.0f, 0f);
    }

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
