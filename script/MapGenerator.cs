using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public LevelBuilder level;

    public GameObject Map3x3;
    public GameObject Map4x4;

    public Color color0 = Color.blue;
    public Color color1 = Color.red;
    public Color color2 = new Color32(135, 206, 235, 255);
    public Color color3 = Color.yellow;
    
    public GameObject Hex;
    public GameObject Square;
    public GameObject Sun;
    public GameObject Starting;
    public GameObject Ending;

    public float pos_z = -0.04f;
    // Start is called before the first frame update
    void Start()
    {
        if (level.dim == 3)
        {
            Map3x3.SetActive(true);
            Map4x4.SetActive(false);

            createStart3x3((float)level.endingX);
            createEnd3x3((float)level.startingX);

            for (int i=0; i<level.hexPositions.Count; i++)
            {
                createHex3x3((float)level.hexPositions[i].x, (float)level.hexPositions[i].y);
                // Debug.Log(level.hexPositions[i].x);
                // Debug.Log(level.hexPositions[i].y);
            }
            int colorindex = 0;
            foreach (ListOfVector2 squarePositions in level.squarePositionsByColor){
                foreach (Vector2 squarePos in squarePositions.positions)
                {
                    createSquare3x3((float)squarePos.x, (float)squarePos.y, colorindex);
                }
                colorindex++;
            }
            colorindex = 0;
            foreach (ListOfVector2 sunPositions in level.sunPositionsByColor){
                foreach (Vector2 sunPos in sunPositions.positions)
                {
                    createSun3x3((float)sunPos.x, (float)sunPos.y, colorindex);
                }
                colorindex++;
            }
        }
        else
        {
            Map3x3.SetActive(false);
            Map4x4.SetActive(true);
        }

    }

    public void createStart3x3(float gridX)
    {
        GameObject start = Instantiate(Starting, Map3x3.transform.position + new Vector3(-0.3f+gridX*0.1f,-0.3f,pos_z), Starting.transform.rotation, Map3x3.transform);
        start.transform.parent = Map3x3.transform;
    }

    public void createEnd3x3(float gridX)
    {
        GameObject end = Instantiate(Ending, Map3x3.transform.position + new Vector3(-0.3f+gridX*0.1f,0.3f,pos_z), Ending.transform.rotation, Map3x3.transform);
        end.transform.parent = Map3x3.transform;
    }


    public void createHex3x3(float gridX, float gridY)
    {
        // Debug.Log(Map3x3.transform.position.x);
        // Debug.Log(Map3x3.transform.position.y);
        GameObject hex = Instantiate(Hex, Map3x3.transform.position + new Vector3(-0.3f+gridY*0.1f,-0.3f+gridX*0.1f,pos_z), Hex.transform.rotation, Map3x3.transform);
        hex.transform.parent = Map3x3.transform;
    }

    public void createSquare3x3(float gridX, float gridY, int colorindex)
    {
        Color color = Color.white;
        if (colorindex == 0)
            color = color0;
        if (colorindex == 1)
            color = color1;
        if (colorindex == 2)
            color = color2;
        if (colorindex == 3)
            color = color3;
        GameObject square = Instantiate(Square, Map3x3.transform.position + new Vector3(-0.3f+gridY*0.1f,-0.3f+gridX*0.1f,pos_z), Square.transform.rotation, Map3x3.transform);
        square.GetComponent<Renderer>().material.color = color;
        square.transform.parent = Map3x3.transform;
    }

    public void createSun3x3(float gridX, float gridY, int colorindex)
    {
        Color color = Color.white;
        if (colorindex == 0)
            color = color0;
        if (colorindex == 1)
            color = color1;
        if (colorindex == 2)
            color = color2;
        if (colorindex == 3)
            color = color3;
        GameObject sun = Instantiate(Sun, Map3x3.transform.position + new Vector3(-0.3f+gridY*0.1f,-0.3f+gridX*0.1f,pos_z), Sun.transform.rotation, Map3x3.transform);
        sun.GetComponent<Renderer>().material.color = color;
        sun.transform.parent = Map3x3.transform;
    }
}
