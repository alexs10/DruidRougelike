using System.Collections.Generic;
using UnityEngine;

public class PickupFactory {
	private Dictionary<string, createPickupDelagate> pickupDictionary;
    private Dictionary<string, Color> colorDictionary;
	public PickupFactory () {

        colorDictionary = new Dictionary<string, Color> ();
        colorDictionary.Add("red", (Color.red));
        colorDictionary.Add("yellow", (Color.yellow));
        colorDictionary.Add("blue", (Color.blue));
        colorDictionary.Add("magenta", (Color.magenta));
		colorDictionary.Add ("boss", (Color.black));
			
	}

	public GameObject CreateKey(string color) {
		return GenericKey();
	}

    public GameObject SetKey(GameObject obj, string color) {

        obj.GetComponent<Pickup>().SetItem(ActionCommandFactory.GetInstance().CreateKeyAction(colorDictionary[color]));
        obj.GetComponent<SpriteRenderer>().color = colorDictionary[color];
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

