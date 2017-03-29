using System.Collections.Generic;
using UnityEngine;

public class InventoryController: MonoBehaviour, Controllable {

	private GameObject inventory;
	private PlayerController playerController;
	private GameObject selector;

	private List<ActionCommand> items;
	private int inventoryMax = 9;

	private int currentIndex = 0;

	private Vector2 selectorOffset;

	public void Start() {
		selector = GameObject.Find ("Selector");
		selectorOffset = selector.transform.position;

		inventory = GameObject.Find ("Inventory2");
		playerController = GetComponent<PlayerController> ();

		items = new List<ActionCommand> ();

		inventory.SetActive (false);
	}

	public void ControlUpdate() {
		Debug.Log ("Here");
		if (Input.GetKeyDown (KeyCode.I) || Input.GetKeyDown(KeyCode.Escape)) {
			Debug.Log ("Returning to playerstate");	
			playerController.ReturnState ();
		}

		int horizontal = 0;
		int vertical = 0;

		horizontal = (int)(Input.GetAxisRaw("Horizontal"));
		vertical = (int)(Input.GetAxisRaw("Vertical"));
		if (Input.GetKeyDown(KeyCode.RightArrow) && currentIndex < 6) {
			currentIndex += 3;
		} else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentIndex > 2) {
			currentIndex -= 3;
		} else if (Input.GetKeyDown(KeyCode.DownArrow) && currentIndex % 3 != 2) {
			currentIndex++;
		} else if (Input.GetKeyDown(KeyCode.UpArrow) && currentIndex % 3 != 0) {
			currentIndex--;
		}

		selector.transform.position = GetIndexCoordinates ();

	}

	public void AddItem(ActionCommand item) {
		if (items.Count < inventoryMax) {
			items.Add (item);
		}
	}

	public Vector2 GetIndexCoordinates() {
		return selectorOffset + new Vector2 (
			(currentIndex / 3) * 75f,
			(currentIndex % 3) * -75f);
	}

	public void Setup() {
		currentIndex = 0;
		inventory.SetActive (true);
	}

	public void TearDown() {
		inventory.SetActive (false);
	}


}


