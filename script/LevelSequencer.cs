using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSequencer : MonoBehaviour
{
    private LevelBuilder levelBuilder;
    private RunBKT runBKT;

    private float lastDifficulty = 0.001f;

    public float p_hex = 0.5f;
    public float p_sq = 0.5f;
    public float p_su = 0.5f;

    private float p_seuil = 0.8f;

    private bool learning = true;
    private int lastLearning = 0;

    public void Start()
    {
        levelBuilder = GameObject.Find("LevelBuilder").GetComponent<LevelBuilder>();
        runBKT = GameObject.Find("RunBKT").GetComponent<RunBKT>();
        // levelBuilder.GenerateLevel();
        // levelBuilder.CreateLevel();
    }

    public IEnumerator DelayedNextLevel() {
        yield return new WaitForSeconds(3.0f);
        NextLevel();
    }

    public void NextLevel()
    {
        float nextDifficulty = lastDifficulty;
        levelBuilder.dim = 3;
        levelBuilder.nWalls = 0;
        levelBuilder.nFakeWalls = 0;
        if(learning){
            int i = 0;
            if(p_hex < p_seuil){
                levelBuilder.nSquareByColor = new List<int>();
                levelBuilder.nSunByColor = new List<int>();
                while(nextDifficulty <= lastDifficulty && i < 50){
                    Debug.Log("HEXAGON: " + nextDifficulty + " < " + lastDifficulty);
                    if(i != 5){
                        // if(levelBuilder.nHexagon % 2 == 1){
                        //     levelBuilder.nWalls += 1;
                        //     levelBuilder.nFakeWalls += 1;
                        // }
                        levelBuilder.nHexagon += 1;
                        lastLearning = 0;
                        nextDifficulty = levelBuilder.GenerateLevel() * (2 - p_hex);
                    }
                    else{
                        levelBuilder.dim = 4;
                        levelBuilder.nWalls = 0;
                        levelBuilder.nFakeWalls = 0;
                        levelBuilder.nHexagon = 0;
                    }
                    i += 1;
                }

            }
            else if(p_sq < p_seuil){
                int nCol = 2;
                i = 0;
                levelBuilder.nHexagon = 0;
                levelBuilder.nSquareByColor = new List<int>();
                levelBuilder.nSunByColor = new List<int>();
                if (lastLearning == 0){
                    lastDifficulty = 0.001f;
                }
                lastLearning = 1;
                while(levelBuilder.nSquareByColor.Count < nCol){
                    levelBuilder.nSquareByColor.Add(1);
                    levelBuilder.nSunByColor.Add(0);
                }
                nextDifficulty = levelBuilder.GenerateLevel() * (2 - p_sq);
                while(nextDifficulty <= lastDifficulty){
                    if(i==5){
                        nCol += 1;
                    }
                    if(i==10){
                        levelBuilder.dim = 4;
                        nCol = 2;
                        levelBuilder.nWalls = 0;
                        levelBuilder.nFakeWalls = 0;
                        levelBuilder.nHexagon = 0;
                        levelBuilder.nSquareByColor = new List<int>();
                        levelBuilder.nSunByColor = new List<int>();
                        i+= 1;
                        continue;
                    }
                    while(levelBuilder.nSquareByColor.Count < nCol){
                        levelBuilder.nSquareByColor.Add(1);
                        levelBuilder.nSunByColor.Add(0);
                    }
                    // draw a random color
                    int col = Random.Range(0, nCol);
                    levelBuilder.nSquareByColor[col] += 1;
                    int newWall = Random.Range(0, 4);
                    if(newWall == 0){
                        levelBuilder.nWalls += 1;
                    }
                    else{
                        levelBuilder.nFakeWalls += 1;
                    }
                    int newFake = Random.Range(0, 4);
                    if(newFake == 0){
                        levelBuilder.nFakeWalls += 1;
                    }
                    else{
                        levelBuilder.nWalls += 1;
                    }
                    nextDifficulty = levelBuilder.GenerateLevel() * (2 - p_sq);
                    i += 1;
                }
            }
            else if(p_su < p_seuil){
                levelBuilder.nHexagon = 0;
                levelBuilder.nSquareByColor = new List<int>();
                levelBuilder.nSunByColor = new List<int>();
                if (lastLearning == 1){
                    lastDifficulty = 0.001f;
                }
                lastLearning = 2;
                int nCol = 1;
                i = 0;
                while(levelBuilder.nSunByColor.Count < nCol){
                    levelBuilder.nSunByColor.Add(2);
                    levelBuilder.nSquareByColor.Add(0);
                }
                nextDifficulty = levelBuilder.GenerateLevel() * (2 - p_su);
                while(nextDifficulty <= lastDifficulty){
                    nCol += Random.Range(0, 2);
                    if(i==3){
                        levelBuilder.dim = 4;
                        nCol = 1;
                        levelBuilder.nWalls = 0;
                        levelBuilder.nFakeWalls = 0;
                        levelBuilder.nHexagon = 0;
                        levelBuilder.nSquareByColor = new List<int>();
                        levelBuilder.nSunByColor = new List<int>();
                        i+= 1;
                        continue;
                    }
                    while(levelBuilder.nSunByColor.Count < nCol){
                        levelBuilder.nSunByColor.Add(2);
                        levelBuilder.nSquareByColor.Add(0);
                    }
                    // draw a random color
                    int col = Random.Range(0, nCol);
                    levelBuilder.nSunByColor[col] += 2;
                    int newWall = Random.Range(0, 7);
                    if(newWall == 0){
                        levelBuilder.nWalls += 1;
                    }
                    else{
                        levelBuilder.nFakeWalls += 1;
                    }
                    int newFake = Random.Range(0, 7);
                    if(newFake == 0){
                        levelBuilder.nFakeWalls += 1;
                    }
                    else{
                        levelBuilder.nWalls += 1;
                    }
                    nextDifficulty = levelBuilder.GenerateLevel() * (2 - p_su);
                    i += 1;
                }
            }
            else{
                learning = false;
                NextLevel();
            }
        }
        else{
            if(lastLearning == 2){
                lastDifficulty = 0.001f;
            }
            lastLearning = -1;
            levelBuilder.nHexagon = 0;
            levelBuilder.nSquareByColor = new List<int>();
            levelBuilder.nSunByColor = new List<int>();
            levelBuilder.dim = 4;
            while(nextDifficulty <= lastDifficulty){
                Debug.Log("LAST DIFFICULTY: " + lastDifficulty);
                levelBuilder.nWalls = Random.Range(0, 5);
                levelBuilder.nFakeWalls = Random.Range(0, 4);
                levelBuilder.nHexagon = Random.Range(0, 5);
                int nCol = Random.Range(1, 4);
                levelBuilder.nSquareByColor = new List<int>();
                for(int i = 0; i < nCol; i++){
                    levelBuilder.nSquareByColor.Add(Random.Range(0, 4));
                }
                levelBuilder.nSunByColor = new List<int>();
                for(int i = 0; i < nCol; i++){
                    levelBuilder.nSunByColor.Add(Random.Range(0, 4));
                }
                float p = 1f;
                if(levelBuilder.nHexagon > 0){
                    p *= p_hex;
                }
                if(levelBuilder.nSquareByColor.Count > 0){
                    p *= p_sq;
                }
                if(levelBuilder.nSunByColor.Count > 0){
                    p *= p_su;
                }
                Debug.Log("HEXAGON: " + levelBuilder.nHexagon);
                Debug.Log("COLORS: " + levelBuilder.nSquareByColor.Count);
                string s = "";
                for(int i = 0; i < levelBuilder.nSquareByColor.Count; i++){
                    s += levelBuilder.nSquareByColor[i] + " ";
                }
                Debug.Log("SQUARES: " + s);
                s = "";
                for(int i = 0; i < levelBuilder.nSunByColor.Count; i++){
                    s += levelBuilder.nSunByColor[i] + " ";
                }
                Debug.Log("SUNS: " + s);
                Debug.Log("P: " + p);

                nextDifficulty = levelBuilder.GenerateLevel() * (2-p);
            }
        }
        Debug.Log("LAST DIFFICULTY: " + lastDifficulty);
        Debug.Log("NEXT DIFFICULTY: " + nextDifficulty);
        Debug.Log("LAST LEARNING: " + lastLearning);
        lastDifficulty = nextDifficulty;
        levelBuilder.CreateLevel();
    }
}