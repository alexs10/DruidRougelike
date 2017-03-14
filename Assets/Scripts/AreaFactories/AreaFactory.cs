﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaFactory : MonoBehaviour {

    public GameObject[] floorTiles;                                 //Array of floor prefabs.
    public GameObject[] wallTiles;                                  //Array of wall prefabs.
    public GameObject[] enemyTiles;                                 //Array of enemy prefabs.
    public GameObject[] outerWallTiles;                             //Array of outer tile prefabs.

    public GameObject[] lockTiles;
    public GameObject[] doorTiles;

    private Transform board;

    void Awake() {
        board = GameObject.Find("Board").transform;
    }

    public void ClearBoard() {
        board = new GameObject("BoardHolder").transform;
    }

    public void createEnemy(int index, Position pos) {
        GameObject toInstantiate = enemyTiles[index % enemyTiles.Length];
        DoInstantiate(toInstantiate, pos);
    }

    public void createWall(int index, Position pos) {
        Debug.Log(index);
        Debug.Log(index % wallTiles.Length);
        GameObject toInstantiate = wallTiles[index % wallTiles.Length];
        DoInstantiate(toInstantiate, pos);
    }

    public void createOuterWall(int index, Position pos) {
        GameObject toInstantiate = outerWallTiles[index % outerWallTiles.Length];
        DoInstantiate(toInstantiate, pos);
    }

    public void createFloor(int index, Position pos) {
        GameObject toInstantiate = floorTiles[index % floorTiles.Length];
        DoInstantiate(toInstantiate, pos);
    }

    public void createLock(string name, Position pos) {

    }

    public void createDoor(Assets.Scripts.Map.Room room, Position pos) {
        GameObject door = doorTiles[0];
        GameObject realDoor = DoInstantiate(door, pos) as GameObject;
        realDoor.GetComponent<Door>().SetDestination(room);
    }

    private GameObject DoInstantiate(GameObject toInstantiate, Position pos) {
        GameObject instance = Instantiate(toInstantiate, new Vector3(pos.x, pos.y, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(board);
        return instance;
    }
}