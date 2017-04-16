using UnityEngine;
using System.Collections.Generic;

public class LockFactory {
	private Dictionary<string, createLockDelegate> lockDictionary;


	public LockFactory () {
		lockDictionary = new Dictionary<string, createLockDelegate> ();
		lockDictionary.Add ("White", coloredLock(Color.white));
		lockDictionary.Add ("Yellow", coloredLock (Color.yellow));
		lockDictionary.Add ("Magenta", coloredLock (Color.magenta));
		lockDictionary.Add ("Blue", coloredLock (Color.blue));
		lockDictionary.Add ("Boss", bossLock ());
	}

	private createLockDelegate coloredLock(Color color) {
		return () => {
			GameObject prefab = Resources.Load ("LockArt") as GameObject;
			prefab.GetComponent<Lock> ().color = color;
			return prefab;
		};
	}

	private createLockDelegate bossLock() {
		return () => {
			Debug.Log("Instantiating boss lock");
			GameObject prefab = Resources.Load ("LockArt") as GameObject;
			prefab.GetComponent<Lock> ().color = Color.red;
			return prefab;
		};
	}

	public GameObject CreateLock(string name) {
		return lockDictionary [name] ();
	}

	private delegate GameObject createLockDelegate();
}


