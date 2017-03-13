using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaFactory : MonoBehaviour {

    public GameObject[] floorTiles;                                 //Array of floor prefabs.
    public GameObject[] wallTiles;                                  //Array of wall prefabs.
    public GameObject[] enemyTiles;                                 //Array of enemy prefabs.
    public GameObject[] outerWallTiles;                             //Array of outer tile prefabs.

    private Transform board;

    void Awake() {
        board = GameObject.Find("Board").transform;
    }

    public void clearBoard() {
        foreach (Transform child in board) {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void createEnemy(int index, Position pos) {

    }

    public void createWall(int index, Position pos) {

    }

    public void createOuterWall(int index, Position pos) {

    }

    public void createFloor(int index, Position pos) {
        GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
        GameObject instance = Instantiate(toInstantiate, new Vector3(pos.x, pos.y, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(board);
    }

    public void createLock(string name, Position pos) {

    }

    public void createDoor(Assets.Scripts.Map.Room room, Position pos) {

    }
}
