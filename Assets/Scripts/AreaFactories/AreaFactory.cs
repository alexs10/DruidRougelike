using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaFactory : MonoBehaviour {

    public GameObject[] floorTiles;                                 //Array of floor prefabs.
    public GameObject[] wallTiles;                                  //Array of wall prefabs.
    public GameObject[] enemyTiles;                                 //Array of enemy prefabs.
    public GameObject[] outerWallTiles;                             //Array of outer tile prefabs.

    private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.


    public void clearBoard() {
        boardHolder = new GameObject("Board").transform;
    }

    public void createEnemy(int index, Position pos) {

    }

    public void createWall(int index, Position pos) {

    }

    public void createOuterWall(int index, Position pos) {

    }

    public void createFloor(int index, Position pos) {

    }

    public void createLock(string name, Position pos) {

    }

    public void createDoor(Assets.Scripts.Map.Room room, Position pos) {

    }
}
