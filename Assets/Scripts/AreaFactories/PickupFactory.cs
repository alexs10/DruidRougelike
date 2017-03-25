using System.Collections.Generic;
using UnityEngine;

public class PickupFactory {
	private Dictionary<string, createPickupDelagate> pickupDictionary;

	public PickupFactory () {
		pickupDictionary = new Dictionary<string, createPickupDelagate> ();
		pickupDictionary.Add("red", ColoredKey(Color.red));
		pickupDictionary.Add("green", ColoredKey(Color.green));
		pickupDictionary.Add("blue", ColoredKey(Color.blue));
		pickupDictionary.Add("magenta", ColoredKey(Color.magenta));
			
	}

	public GameObject CreateKey(string color) {
		return pickupDictionary [color] ();
	}

	private createPickupDelagate ColoredKey(Color color) {
		return () => {
			GameObject prefab = Resources.Load ("Key") as GameObject;
			prefab.GetComponent<Pickup> ().item = ActionCommandFactory.GetInstance ().CreateKeyAction (color);
			prefab.GetComponent<SpriteRenderer>().color = color;
			Debug.Log("key item: " + prefab.GetComponent<Pickup>().item);
			return prefab;
		};
	}

	private delegate GameObject createPickupDelagate();
}

