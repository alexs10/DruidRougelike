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

	public void Awake() {
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

    public InventoryState SnapshotState() {
        return new InventoryState(items);
    }

    public InventoryState DefaultState() {
        List<ActionCommand> blankItems = new List<ActionCommand>();

        for (int i = 0; i < INVENTORY_SIZE; i++) {
            blankItems.Add(new NullActionCommand());
        }
        return new InventoryState(blankItems);
    }

    public void LoadState(InventoryState state) {
        this.items = state.items;

        for (int i = 0; i < INVENTORY_SIZE; i++) {
            slots.GetChild(i).GetComponent<Image>().sprite = items[i].GetSprite();
        }

		UpdateUI (items [currentIndex]);
    }

	public void ControlUpdate() {
		if (Input.GetKeyDown (KeyCode.I) || Input.GetKeyDown(KeyCode.Escape)) {
			playerController.ReturnState ();
		}

		int horizontal = 0;
		int vertical = 0;

		horizontal = (int)(Input.GetAxisRaw("Horizontal"));
		vertical = (int)(Input.GetAxisRaw("Vertical"));
		if (Input.GetKeyDown(KeyCode.DownArrow) && currentIndex < 6) {
			ChangeIndex(3);
		} else if (Input.GetKeyDown(KeyCode.UpArrow) && currentIndex > 2) {
			ChangeIndex(-3);
		} else if (Input.GetKeyDown(KeyCode.RightArrow) && currentIndex % 3 != 2) {
			ChangeIndex(1);
		} else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentIndex % 3 != 0) {
			ChangeIndex(-1);
		} else if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ActionCommand command = actionBar.Equip(items[currentIndex], 0);
            RemoveItem(currentIndex);
            AddItem(command, currentIndex);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ActionCommand command = actionBar.Equip(items[currentIndex], 1);
            RemoveItem(currentIndex);
            AddItem(command, currentIndex);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            ActionCommand command = actionBar.Equip(items[currentIndex], 2);
            RemoveItem(currentIndex);
            AddItem(command, currentIndex);
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            ActionCommand command = actionBar.Equip(items[currentIndex], 3);
            RemoveItem(currentIndex);
            AddItem(command, currentIndex);
        }


    }

	private void ChangeIndex(int diff) {
		currentIndex = currentIndex + diff;

		ActionCommand item = items [currentIndex];
        UpdateUI(item);

		selector.transform.position = GetIndexCoordinates ();

	}

    public void PickupItem(ActionCommand item) {
        for (int i = 0; i < items.Count; i++) {
            if (!HasItem(i)) {
                AddItem(item, i);
                return;
            }
        }

    }

    private void UpdateUI(ActionCommand item) {
        selectedImage.sprite = item.GetSprite();
        selectedName.text = item.GetName();
        selectedDescription.text = item.GetDescription();
    }

    private void RemoveItem(int index) {
        items[index] = new NullActionCommand();
        slots.GetChild(index).GetComponent<Image>().sprite = items[index].GetSprite();
        UpdateUI(items[index]);

    }

    private void AddItem(ActionCommand item, int index) {

		items [index] = item;
		slots.GetChild (index).GetComponent<Image> ().sprite = item.GetSprite ();
        UpdateUI(items[index]);

    }

    private bool HasItem(int index) {
        return items[index].GetName() != "";
    }

	public Vector2 GetIndexCoordinates() {
		return selectorOffset + new Vector2 (
            (currentIndex % 3) * 75f,
            (currentIndex / 3) * -75f );
	}

	public void Setup() {
		currentIndex = 0;
		inventory.SetActive (true);
	}

	public void TearDown() {
		inventory.SetActive (false);
	}


}


