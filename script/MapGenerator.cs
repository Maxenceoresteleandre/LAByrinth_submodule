using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public TextAsset jsonFile;
    // Start is called before the first frame update
    void Start()
    {
        JsonToMap(jsonFile);
    }

    public void JsonToMap(TextAsset file)
    {
        Panel panel = JsonUtility.FromJson<Panel>(jsonFile.text);
 
        Debug.Log("panel size: " + panel.gridSize + "+ grid " + panel.grid);

    }
}
