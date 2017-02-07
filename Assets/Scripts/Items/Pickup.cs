using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
	private ActionCommand item;
	public string itemName;
	//this will be changed a bit once inventory is working
	private bool hasItem = true;
	private SpriteRenderer sprite;
	void Start() {
		//hardcoded for now
		this.item = ActionCommandFactory.GetInstance().CreateKeyAction(Color.magenta);
		sprite = GetComponent<SpriteRenderer> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Pickup");
		if (hasItem && other.GetComponent<Player> () != null) {
			Debug.Log ("Picking up");
			hasItem = false;
			other.GetComponent<Player> ().EquipAction (item, "2");
			sprite.color = Color.clear;
		}
	}


}
