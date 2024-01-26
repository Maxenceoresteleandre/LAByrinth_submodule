using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHider : MonoBehaviour
{
    public GameObject[] objectsToDeactivate;

    void Start()
    {
        for (int i=0; i<objectsToDeactivate.Length; i++){
            Debug.Log("Deactivated object: " + objectsToDeactivate[i].name);
            objectsToDeactivate[i].SetActive(false);
        }
    }
}
