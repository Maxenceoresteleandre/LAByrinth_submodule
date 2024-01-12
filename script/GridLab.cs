using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLab : MonoBehaviour
{
    private Tuple<int, int> lastGridPosition = Tuple.New(-1, -1);
    void Start()
    {

    }

    void Update(){
        // Log the player's grid position when it changes
        Tuple<int, int> gridPosition = GetGridPosition(Camera.main.transform.position);
        if (gridPosition.First != -1 && gridPosition.Second != -1){
            if (gridPosition.First != lastGridPosition.First || gridPosition.Second != lastGridPosition.Second){
                Debug.Log("Player is in grid cell: " + gridPosition.First + ", " + gridPosition.Second);
                lastGridPosition = gridPosition;
            }
        }
    }

    public Tuple<int, int> GetGridPosition(Vector3 position)
    {
        // Return the grid cell that the player is currently in
        // Test for each grid cell in the grid
        for (int x = 0; x < transform.childCount; x++)
        {
            for (int z = 0; z < transform.GetChild(x).childCount; z++)
            {
                // Get the current grid cell
                Transform cell = transform.GetChild(x).GetChild(z);
                if (cell.GetComponent<Collider>().bounds.Contains(position))
                {
                    // Return the grid cell's position
                    return Tuple.New(x, z);
                }
            }
        }
        return Tuple.New(-1, -1);
    }
}

public class Tuple<T1, T2>
{
    public T1 First { get; private set; }
    public T2 Second { get; private set; }
    internal Tuple(T1 first, T2 second)
    {
        First = first;
        Second = second;
    }
}

public static class Tuple
{
    public static Tuple<T1, T2> New<T1, T2>(T1 first, T2 second)
    {
        var tuple = new Tuple<T1, T2>(first, second);
        return tuple;
    }
}