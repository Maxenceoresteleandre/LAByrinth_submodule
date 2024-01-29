using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // public GameObject StartPlatform;
    // public GameObject Map3x3 = GameObject.Find("/StartPlateform(Clone)/MapFond3x3");
    // public GameObject Map4x4 = GameObject.Find("/StartPlateform(Clone)/MapFond4x4");

    public GameObject Map3x3;
    public GameObject Map4x4;
    // public Color colorN = Color.pink;
    public Color color0 = Color.blue;
    public Color color1 = Color.red;
    public Color color2 = new Color32(135, 206, 235, 255);
    public Color color3 = Color.yellow;
    public GameObject Hex;
    public GameObject Square;
    public GameObject Sun;
    public GameObject Wall;
    public GameObject FakeWall;


    public GameObject Starting;
    public GameObject Ending;

    public float pos_z = 0;

    // void Start(){
    //     StartPlatform = GameObject.FindGameObjectsWithTag("Start")[0];
    //     Debug.Log("taaaag : " + StartPlatform.tag);
    //     // Map3x3 = GameObject.FindGameObjectsWithTag("Map3x3")[0];
    //     // Map4x4 = GameObject.FindGameObjectsWithTag("Map4x4")[0];

    // }
    // Start is called before the first frame update
    public void generateMap(LevelBuilder level, Panel panel)
    {
        // GameObject StartPlatform = GameObject.FindGameObjectsWithTag("Start")[0];
        // Debug.Log("taaaag : " + StartPlatform.tag);
        if (level.dim == 3)
        {
            Debug.Log("dimension : "+level.dim);
            Map3x3.SetActive(true);
            Map4x4.SetActive(false);
            createStart3x3((float)panel.GetStart().Second);
            createEnd3x3((float)panel.GetEnd().Second);

            foreach (Tuple<int, int> hexPos in panel.GetHexagonPositions())
            {
                // Debug.Log("hex pos: " + hexPos.Second + ", " + hexPos.First);

                createHex3x3((float)hexPos.Second, (float)hexPos.First);
                
            }
            foreach (Tuple<int, int> squarePos in panel.GetSquarePositions())
            {
                Debug.Log("square couleur: " + panel.GetSymbol(squarePos.First, squarePos.Second).GetColorId());

                Debug.Log("square pos: " + squarePos.Second + ", " + squarePos.First);
                createSquare3x3((float)squarePos.Second, (float)squarePos.First, panel.GetSymbol(squarePos.First, squarePos.Second).GetColorId());
            }
            foreach (Tuple<int, int> sunPos in panel.GetSunPositions())
            {
                createSun3x3((float)sunPos.Second, (float)sunPos.First, panel.GetSymbol(sunPos.First, sunPos.Second).GetColorId());
            }

            foreach (Tuple<int, int> wallPos in panel.GetWallPositions())
            {
                createWall3x3((float)wallPos.Second, (float)wallPos.First);
            }
            // foreach (Tuple<int, int> fakeWallPos in level.GetFakeWallPositions())
            // {
            //     createFakeWall3x3((float)fakeWallPos.Second, (float)fakeWallPos.First);
            // }
        }
        else
        {
            Map3x3.SetActive(false);
            Map4x4.SetActive(true);
            createStart4x4((float)panel.GetStart().Second);
            createEnd4x4((float)panel.GetEnd().Second);

            foreach (Tuple<int, int> hexPos in panel.GetHexagonPositions())
            {
                // Debug.Log("hex pos: " + hexPos.Second + ", " + hexPos.First);
                createHex4x4((float)hexPos.Second, (float)hexPos.First);
            }
            foreach (Tuple<int, int> squarePos in panel.GetSquarePositions())
            {
                Debug.Log("square couleur: " + panel.GetSymbol(squarePos.First, squarePos.Second).GetColorId());

                Debug.Log("square pos: " + squarePos.Second + ", " + squarePos.First);
                createSquare4x4((float)squarePos.Second, (float)squarePos.First, panel.GetSymbol(squarePos.First, squarePos.Second).GetColorId());
            }
            foreach (Tuple<int, int> sunPos in panel.GetSunPositions())
            {
                createSun4x4((float)sunPos.Second, (float)sunPos.First, panel.GetSymbol(sunPos.First, sunPos.Second).GetColorId());
            }
            foreach (Tuple<int, int> wallPos in panel.GetWallPositions())
            {
                createWall4x4((float)wallPos.Second, (float)wallPos.First);
            }
            // foreach (Tuple<int, int> fakeWallPos in level.GetFakeWallPositions())
            // {
            //     createFakeWall4x4((float)fakeWallPos.Second, (float)fakeWallPos.First);
            // }
        }

    }

    public void createStart3x3(float gridX)
    {
        GameObject start = Instantiate(Starting, Map3x3.transform.position + new Vector3(0.3f-gridX*0.1f,-0.3f,pos_z), Starting.transform.rotation, Map3x3.transform);
        start.transform.parent = Map3x3.transform;
    }

    public void createEnd3x3(float gridX)
    {
        GameObject end = Instantiate(Ending, Map3x3.transform.position + new Vector3(0.3f-gridX*0.1f,0.3f,pos_z), Ending.transform.rotation, Map3x3.transform);
        end.transform.parent = Map3x3.transform;
    }


    public void createHex3x3(float gridX, float gridY)
    {
        // Debug.Log(Map3x3.transform.position.x);
        // Debug.Log(Map3x3.transform.position.y);
        GameObject hex = Instantiate(Hex, Map3x3.transform.position + new Vector3(0.3f-gridX*0.1f,-0.3f+gridY*0.1f,pos_z), Hex.transform.rotation, Map3x3.transform);
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
        GameObject square = Instantiate(Square, Map3x3.transform.position + new Vector3(0.3f-gridX*0.1f,-0.3f+gridY*0.1f,pos_z), Square.transform.rotation, Map3x3.transform);
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
        GameObject sun = Instantiate(Sun, Map3x3.transform.position + new Vector3(0.3f-gridX*0.1f,-0.3f+gridY*0.1f,pos_z), Sun.transform.rotation, Map3x3.transform);
        sun.GetComponent<Renderer>().material.color = color;
        sun.transform.parent = Map3x3.transform;
    }

    public void createWall3x3(float gridX, float gridY)
    {
        GameObject wall = Instantiate(Wall, Map3x3.transform.position + new Vector3(0.3f-gridX*0.1f,-0.3f+gridY*0.1f,pos_z), Wall.transform.rotation, Map3x3.transform);
        wall.transform.parent = Map3x3.transform;
    }

    // public void createFakeWall3x3(float gridX, float gridY)
    // {
    //     GameObject fakeWall = Instantiate(FakeWall, Map3x3.transform.position + new Vector3(0.3f-gridX*0.1f,-0.3f+gridY*0.1f,pos_z), FakeWall.transform.rotation, Map3x3.transform);
    //     fakeWall.transform.parent = Map3x3.transform;
    // }

    public void createStart4x4(float gridX)
    {
        GameObject start = Instantiate(Starting, Map4x4.transform.position + new Vector3(0.33f-gridX*0.0825f,-0.33f,pos_z), Starting.transform.rotation, Map4x4.transform);
        start.transform.parent = Map4x4.transform;
    }

    public void createEnd4x4(float gridX)
    {
        GameObject end = Instantiate(Ending, Map4x4.transform.position + new Vector3(0.33f-gridX*0.0825f,0.33f,pos_z), Ending.transform.rotation, Map4x4.transform);
        end.transform.parent = Map4x4.transform;
    }


    public void createHex4x4(float gridX, float gridY)
    {
        // Debug.Log(Map3x3.transform.position.x);
        // Debug.Log(Map3x3.transform.position.y);
        GameObject hex = Instantiate(Hex, Map4x4.transform.position + new Vector3(0.33f-gridX*0.0825f,-0.33f+gridY*0.0825f,pos_z), Hex.transform.rotation, Map4x4.transform);
        hex.transform.parent = Map4x4.transform;
    }

    public void createSquare4x4(float gridX, float gridY, int colorindex)
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
        GameObject square = Instantiate(Square, Map4x4.transform.position + new Vector3(0.33f-gridX*0.0825f,-0.33f+gridY*0.0825f,pos_z), Square.transform.rotation, Map4x4.transform);
        square.GetComponent<Renderer>().material.color = color;
        square.transform.parent = Map4x4.transform;
    }

    public void createSun4x4(float gridX, float gridY, int colorindex)
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
        GameObject sun = Instantiate(Sun, Map4x4.transform.position + new Vector3(0.33f-gridX*0.0825f,-0.33f+gridY*0.0825f,pos_z), Sun.transform.rotation, Map4x4.transform);
        sun.GetComponent<Renderer>().material.color = color;
        sun.transform.parent = Map4x4.transform;
    }

    public void createWall4x4(float gridX, float gridY)
    {
        GameObject wall = Instantiate(Wall, Map4x4.transform.position + new Vector3(0.33f-gridX*0.0825f,-0.33f+gridY*0.0825f,pos_z), Wall.transform.rotation, Map4x4.transform);
        wall.transform.parent = Map4x4.transform;
    }

    // public void createFakeWall3x3(float gridX, float gridY)
    // {
    //     GameObject fakeWall = Instantiate(FakeWall, Map3x3.transform.position + new Vector3(0.3f-gridX*0.1f,-0.3f+gridY*0.1f,pos_z), FakeWall.transform.rotation, Map3x3.transform);
    //     fakeWall.transform.parent = Map3x3.transform;
    // }
}
