using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public Material[] materials;
    
    void Start()
    {
        // Ensure that there's at least one material in the array
        if (materials.Length == 0)
        {
            Debug.LogError("No materials assigned in the array!");
        }
    }

    public void SetMaterialByIndex(int index)
    {
        // Check if the index is valid
        if (index >= 0 && index < materials.Length)
        {
            // Set the material based on the index
            GetComponent<Renderer>().material = materials[index];
        }
        else
        {
            Debug.LogError("Invalid material index!");
        }
    }
}
