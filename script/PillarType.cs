using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarType : MonoBehaviour
{
    public GameObject sphere;

    public void SetPillarType(int type) {
        sphere.GetComponent<MaterialChanger>().SetMaterialByIndex(type);
    }
}
