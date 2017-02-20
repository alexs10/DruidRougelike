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

    public int columns = 12;
    public int rows = 12;
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

    private List<Vector2> unusedPositions = new List<Vector2>();

    private float xLeft = 0, xRight = 0, yBottom = 0, yTop = 0;

	void initGridPositions(int columns, int rows, int xOffset, int yOffset)
    {
        gridPositions.Clear();

		for (int i=1+xOffset; i<columns+xOffset-1; i++)
        {
			for (int j=1+xOffset; j<rows+xOffset-1; j++)
            {
                gridPositions.Add(new Vector2(i, j));
            }
        }
    }

	void initBoard(int columns, int rows, int xOffset, int yOffset)
    {
		Debug.Log("Creating board at: " + xOffset + ", " + yOffset + " with size(" + columns +  ", " + rows + ")");
        boardHolder = new GameObject("Board").transform;
		for (int i=xOffset; i<columns+xOffset; i++)
        {
			for (int j=yOffset; j<rows+yOffset; j++)
            {
//				Debug.Log ("i = " + i + " j = " + j);
//                GameObject toInstantiate = (i == 0 || j == 0 || i == columns-1 || j == rows-1 ) ?
//                    outerWallTiles[Random.Range(0, outerWallTiles.Length)] :
//                    floorTiles[Random.Range(0, floorTiles.Length)];

				GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                Vector2 location = new Vector2(i, j);
                GameObject instance = Instantiate(toInstantiate, location, Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
                UsePosition(location);
            }
        }
    }

    void UsePosition(Vector2 position) {
        //expand horizontal bound 
        if (position.x < xLeft) {
            Expand(position.x, xLeft - 1, yBottom, yTop);
            xLeft = position.x;
        } else if (position.x > xRight) {
            Expand(xRight + 1, position.x, yBottom, yTop);
            xRight = position.x;
        }

        //expand vertically
        if (position.y < yBottom) {
            Expand(xLeft, xRight, position.y, yBottom - 1);
            yBottom = position.y;
        }
        else if (position.x > yTop) {
            Expand(xLeft, xRight, yTop + 1, position.y);
            yTop = position.y;
        }

        unusedPositions.Remove(position);
    }

    void Expand(float xLeft, float xRight, float yBottom, float yTop) {
        for (float i = xLeft; i <= xRight; i++) {
            for (float j = yBottom; j <= yTop; j++) {
                unusedPositions.Add(new Vector2(i, j));
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
        initBoard(10, 10, 0, 0);
        initGridPositions(10, 10, 0, 0);

        int enemyCount = (int)Math.Log(level, 2f);

        layoutObjectsAtRandom(foodTiles, foodCount.min, foodCount.max);
        layoutObjectsAtRandom(wallTiles, wallCount.min, wallCount.max);
        layoutObjectsAtRandom(enemyTiles, enemyCount, enemyCount);
        //Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);

    }

    public void BuildVerticleHallway(float x, float yBottom, float yTop) {
        BuildHallway(x, yBottom, yTop, true);

    }

    public void BuildHorizontalHallway(float y, float xLeft, float xRight) {
        BuildHallway(y, xLeft, xRight, false);
    }

    private void BuildHallway(float constant, float min, float max, bool verticle) {
        Transform hallWay = new GameObject("Hallway").transform;
        for (float j = min; j <= max; j++) {
            GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

            Vector2 location = verticle ? new Vector2(constant, j) : new Vector2(j, constant);

            GameObject instance = Instantiate(toInstantiate, location, Quaternion.identity) as GameObject;
            instance.transform.SetParent(hallWay);

            UsePosition(location);
        }
    }

	public void BuildRoom(float xOffset, float yOffset, float width, float height) {
		//Debug.Log (((int)width % 2 == 0 ? 1 : 0));
		//int xOffset = (int)xCenter - (int)width / 2 + ((int)width % 2 == 0 ? 0 : 0);
		//int yOffset = (int)yCenter - (int)height / 2 + ((int)height % 2 == 0 ? 0 : 0);

		initBoard ((int)width, (int)height, (int)xOffset, (int)yOffset);
		initGridPositions ((int)width, (int)height, (int)xOffset, (int)yOffset);

		int enemyCount = (int)Math.Log(4f, 2f);

		//layoutObjectsAtRandom(foodTiles, foodCount.min, foodCount.max);
		//layoutObjectsAtRandom(wallTiles, wallCount.min, wallCount.max);
		//layoutObjectsAtRandom(enemyTiles, enemyCount, enemyCount);
	}

    public void FillWithWalls() {
        Debug.Log("left: " + xLeft + " right: " + xRight + " bottom: " + yBottom + " top: " + yTop);
        Transform walls = new GameObject("Walls").transform;
        foreach (Vector2 unusedPosition in unusedPositions) {
            GameObject toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
            GameObject instance = Instantiate(toInstantiate, unusedPosition, Quaternion.identity) as GameObject;
            instance.transform.SetParent(walls);
        }
    }
}
