using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController: MonoBehaviour, Controllable {

	private GameObject inventory;
	private PlayerController playerController;
	private GameObject selector;
	private Transform slots;
	private Text selectedDescription;
	private Text selectedName;
	private Image selectedImage;

	private List<ActionCommand> items;
	private const int INVENTORY_SIZE = 9;

	private int currentIndex = 0;

	private Vector2 selectorOffset;

    private ActionBar actionBar;

	public void Start() {
		selector = GameObject.Find ("Selector");
		selectorOffset = selector.transform.position;

		inventory = GameObject.Find ("Inventory");
		slots = inventory.transform.GetChild (2).transform;
		selectedImage = inventory.transform.GetChild (3).GetComponent<Image> ();
		selectedName = inventory.transform.GetChild (4).GetComponent<Text> ();
		selectedDescription = inventory.transform.GetChild (5).GetComponent<Text>();
	

		playerController = GetComponent<PlayerController> ();
        actionBar = GameObject.Find("ActionBar").GetComponent<ActionBar> ();

		items = new List<ActionCommand> ();
		for (int i = 0; i < INVENTORY_SIZE; i++) {
			items.Add (new NullActionCommand ());
		}

		inventory.SetActive (false);
	}

	public void ControlUpdate() {
		if (Input.GetKeyDown (KeyCode.I) || Input.GetKeyDown(KeyCode.Escape)) {
			playerController.ReturnState ();
		}

		int horizontal = 0;
		int vertical = 0;

		horizontal = (int)(Input.GetAxisRaw("Horizontal"));
		vertical = (int)(Input.GetAxisRaw("Vertical"));
		if (Input.GetKeyDown(KeyCode.RightArrow) && currentIndex < 6) {
			ChangeIndex(3);
		} else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentIndex > 2) {
			ChangeIndex(-3);
		} else if (Input.GetKeyDown(KeyCode.DownArrow) && currentIndex % 3 != 2) {
			ChangeIndex(1);
		} else if (Input.GetKeyDown(KeyCode.UpArrow) && currentIndex % 3 != 0) {
			ChangeIndex(-1);
		} else if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ActionCommand command = actionBar.Equip(items[currentIndex], 0);
            items[currentIndex] = new NullActionCommand();
            AddItem(command);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ActionCommand command = actionBar.Equip(items[currentIndex], 1);
            items[currentIndex] = new NullActionCommand();
            AddItem(command);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            ActionCommand command = actionBar.Equip(items[currentIndex], 2);
            items[currentIndex] = new NullActionCommand();
            AddItem(command);
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            ActionCommand command = actionBar.Equip(items[currentIndex], 3);
            items[currentIndex] = new NullActionCommand();
            AddItem(command);
        }


    }

	private void ChangeIndex(int diff) {
		currentIndex = currentIndex + diff;

		ActionCommand item = items [currentIndex];
		selectedImage.sprite = item.GetSprite ();
		selectedName.text = item.GetName ();
		selectedDescription.text = item.GetDescription ();

		selector.transform.position = GetIndexCoordinates ();

	}

	public void AddItem(ActionCommand item) {
		Debug.Log ("Trying to add item");
		for (int i = 0; i < INVENTORY_SIZE; i++) {
			if (items [i].GetName () == "") {
				Debug.Log ("Adding item at: " + i);
				items [i] = item;
				slots.GetChild (i).GetComponent<Image> ().sprite = item.GetSprite ();
				ChangeIndex (0);
				return;
			}
		}
	}

    private bool HasItem(int index) {
        return items[index].GetName() == "";
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


