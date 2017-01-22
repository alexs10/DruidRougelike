using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

    [Serializable]
    public class Count
    {
        public int min;
        public int max;
        
        public Count(int min, int max)
        {
            this.min = min;
            this.max = max;
        } 
    }

    public int columns = 8;
    public int rows = 8;
    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5);
    public GameObject exit;                                         //Prefab to spawn for exit.
    public GameObject[] floorTiles;                                 //Array of floor prefabs.
    public GameObject[] wallTiles;                                  //Array of wall prefabs.
    public GameObject[] foodTiles;                                  //Array of food prefabs.
    public GameObject[] enemyTiles;                                 //Array of enemy prefabs.
    public GameObject[] outerWallTiles;                             //Array of outer tile prefabs.

    private Transform boardHolder;
    private List<Vector2> gridPositions = new List<Vector2>();

    void initGridPositions()
    {
        gridPositions.Clear();

        for (int i=1; i<columns-1; i++)
        {
            for (int j=1; j<rows-1; j++)
            {
                gridPositions.Add(new Vector2(i, j));
            }
        }
    }

    void initBoard()
    {
        boardHolder = new GameObject("Board").transform;
        for (int i=-1; i<columns + 1; i++)
        {
            for (int j=-1; j<rows+1; j++)
            {
                GameObject toInstantiate = (i == -1 || j == -1 || i == columns || j == rows ) ?
                    outerWallTiles[Random.Range(0, outerWallTiles.Length)] :
                    floorTiles[Random.Range(0, floorTiles.Length)];
                GameObject instance = Instantiate(toInstantiate, new Vector2(i, j), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    Vector2 randomUnusedPosition()
    {
        int index = Random.Range(0, gridPositions.Count);
        Vector2 returnIndex = gridPositions[index];
        gridPositions.RemoveAt(index);
        return returnIndex;
    }

    void layoutObjectsAtRandom(GameObject[] objects, int min, int max)
    {
        int objectCount = Random.Range(min, max);

        for (int i=0; i< objectCount; i++)
        {
            Instantiate(
                objects[Random.Range(0, objects.Length)],
                randomUnusedPosition(),
                Quaternion.identity);
        }
    }

    public void setup(int level)
    {
        initBoard();
        initGridPositions();

        int enemyCount = (int)Math.Log(level, 2f);

        layoutObjectsAtRandom(foodTiles, foodCount.min, foodCount.max);
        layoutObjectsAtRandom(wallTiles, wallCount.min, wallCount.max);
        layoutObjectsAtRandom(enemyTiles, enemyCount, enemyCount);
        Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);

    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
