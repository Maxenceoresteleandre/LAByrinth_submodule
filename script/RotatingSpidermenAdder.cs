using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSpidermenAdder : MonoBehaviour
{
    public GameObject rotatingSpiderman;
    public int howMany = 18;

    void Start()
    {
        for (int i=0; i<howMany; i++){
            GameObject newSpiderman = Instantiate(rotatingSpiderman, RandomPos(), Quaternion.identity);
            newSpiderman.transform.Rotate(0, 360.0f * i / howMany, 0);
            newSpiderman.transform.parent = transform;
        }
    }

    private Vector3 RandomPos()
    {
        return new Vector3(Random.Range(-5.0f, 5.0f) * 2.5f, Random.Range(-5.0f, 5.0f) * 2.5f, Random.Range(-5.0f, 5.0f) * 2.5f);
    }
}
