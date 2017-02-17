using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenMediator : MonoBehaviour, Observer {

	private FloorPlanner floorPlanner;
	private Realizer realizer;
	// Use this for initialization
	void Start () {
		floorPlanner = GetComponent<FloorPlanner> ();
		realizer = GetComponent<Realizer> ();
		floorPlanner.Register (this);
		floorPlanner.PlanFloor ();
	}

	public void Notify() {
		Debug.Log ("LEVEL GEN MED WAS NOTIFIED");
		realizer.Realize (floorPlanner.floorGraph);
	}
}
