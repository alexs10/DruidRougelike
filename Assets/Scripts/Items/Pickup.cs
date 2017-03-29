using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
	private ActionCommand item;
	//this will be changed a bit once inventory is working
	private bool hasItem = true;
	private SpriteRenderer sprite;
	void Start() {
        Debug.Log(item.GetName());
		//hardcoded for now
		//item = PickupDictionary.GetInstance().Get(itemName);
		sprite = GetComponent<SpriteRenderer> ();
	}


    public void SetItem(ActionCommand item) {
        Debug.Log("setting item to: " + item.GetName());
        this.item = item;
    }

    public ActionCommand GetItem() {
        return this.item;
    }


	void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Item: " + item.GetName());
		if (item == null) {
			Debug.Log ("Item on pickup is null");
		}
		if (hasItem && other.GetComponent<Player> () != null) {
			Debug.Log ("Picking up");
			hasItem = false;
			other.GetComponent<InventoryController> ().AddItem (item);
			//other.GetComponent<Player> ().EquipAction (item, "2");
			sprite.color = Color.clear;
		}
	}


}
