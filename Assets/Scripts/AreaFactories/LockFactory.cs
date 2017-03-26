using UnityEngine;
using System.Collections.Generic;

public class LockFactory {
	private Dictionary<string, createLockDelegate> lockDictionary;


	public LockFactory () {
		lockDictionary = new Dictionary<string, createLockDelegate> ();
		lockDictionary.Add ("red", coloredLock(Color.red));
		lockDictionary.Add ("blue", coloredLock (Color.blue));
		lockDictionary.Add ("green", coloredLock (Color.green));
		lockDictionary.Add ("magenta", coloredLock (Color.magenta));
	}

	private createLockDelegate coloredLock(Color color) {
		return () => {
			GameObject prefab = Resources.Load ("LockArt") as GameObject;
			prefab.GetComponent<Lock> ().color = color;
			return prefab;
		};
	}

	public GameObject CreateLock(string name) {
		return lockDictionary [name] ();
	}

	private delegate GameObject createLockDelegate();
}


