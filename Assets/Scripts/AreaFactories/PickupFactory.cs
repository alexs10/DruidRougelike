using System.Collections.Generic;
using UnityEngine;

public class PickupFactory {
	private Dictionary<string, createPickupDelagate> pickupDictionary;
    private Dictionary<string, Color> colorDictionary;
	public PickupFactory () {

        colorDictionary = new Dictionary<string, Color> ();
		colorDictionary.Add("White", (Color.white));
        colorDictionary.Add("Yellow", (Color.yellow));
        colorDictionary.Add("Blue", (Color.blue));
        colorDictionary.Add("Magenta", (Color.magenta));
		colorDictionary.Add ("Boss", (Color.red));
			
	}

	public GameObject CreateKey(string color) {
		return GenericKey();
	}

    public GameObject SetKey(GameObject obj, string color) {
		Debug.Log ("Key color: " + color);
        obj.GetComponent<Pickup>().SetItem(ActionCommandFactory.GetInstance().CreateKeyAction(colorDictionary[color], color));
		obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(color + " Key");
        return obj;
    }

	public GameObject CreatePickup(string name) {
		GameObject pickup = Resources.Load (name) as GameObject;
		return pickup;
	}

	public GameObject SetPickup(GameObject obj, string name) {
		obj.GetComponent<Pickup> ().SetItem (ActionCommandFactory.GetInstance ().CreateFromName (name));
		return obj;
	}

    private GameObject GenericKey() {
        GameObject prefab = Resources.Load("KeyArt") as GameObject;
        return prefab;

    }

    private createPickupDelagate ColoredKey(Color color) {
		return () => {
			GameObject prefab = Resources.Load ("KeyArt") as GameObject;
			//prefab.GetComponent<Pickup> ().SetItem(ActionCommandFactory.GetInstance ().CreateKeyAction (color));
			//prefab.GetComponent<SpriteRenderer>().color = color;
			//Debug.Log("key item: " + prefab.GetComponent<Pickup>().GetItem().GetName());
			return prefab;
		};
	}

	private delegate GameObject createPickupDelagate();
}

