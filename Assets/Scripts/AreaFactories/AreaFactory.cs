using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaFactory : MonoBehaviour {

    public GameObject[] floorTiles;                                 //Array of floor prefabs.
    public GameObject[] wallTiles;                                  //Array of wall prefabs.
    public GameObject[] enemyTiles;                                 //Array of enemy prefabs.
    public GameObject[] outerWallTiles;                             //Array of outer tile prefabs.

    public GameObject[] lockTiles;
    public GameObject[] doorTiles;
	public GameObject[] items; //added for items 

    private Transform board;
	private LockFactory lockFactory;
	private PickupFactory pickupFactory;

    void Awake() {
        board = GameObject.Find("Board").transform;
		lockFactory = new LockFactory ();
		pickupFactory = new PickupFactory ();
    }

    public void ClearBoard() {
        board = new GameObject("BoardHolder").transform;
    }

    public void createEnemy(int index, Position pos) {
        GameObject toInstantiate = enemyTiles[index % enemyTiles.Length];
        DoInstantiate(toInstantiate, pos);
    }
		

    public void createWall(int index, Position pos) {
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

	public void createItem(int index, Position pos) {
		GameObject toInstantiate = items[index % items.Length];
		DoInstantiate(toInstantiate, pos);
	} //added for items 

	public void createPickup(string name, Position pos) {
		GameObject instance = pickupFactory.CreatePickup (name);
		GameObject test = DoInstantiate (instance, pos);
		pickupFactory.SetPickup (test, name);
	}

    public void createLock(string name, Position pos) {
		GameObject instance = lockFactory.CreateLock (name);
		DoInstantiate (instance, pos);
    }

	public void createKey(string name, Position pos) {
		GameObject instance = pickupFactory.CreateKey (name);
		GameObject test = DoInstantiate (instance, pos) as GameObject;
        pickupFactory.SetKey(test, name);
        Debug.Log("AREAFACTOR CHECK: " + test.GetComponent<Pickup>().GetItem().GetName());

    }

    public void createDoor(Assets.Scripts.Map.Room room, Direction dir, Position pos) {
        GameObject door = doorTiles[0];
        GameObject realDoor = DoInstantiate(door, pos) as GameObject;
		if (realDoor.GetComponent<Door>() != null)
        	realDoor.GetComponent<Door>().SetDestination(room, dir);
    }

    private GameObject DoInstantiate(GameObject toInstantiate, Position pos) {
        GameObject instance = Instantiate(toInstantiate, new Vector3(pos.x, pos.y, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(board);
        return instance;
    }
}
