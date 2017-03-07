using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenMediator : MonoBehaviour, Observer, Observable {

	private FloorPlanner floorPlanner;
	private Realizer realizer;

    private List<Observer> observers = new List<Observer>();

	// Use this for initialization
	void Start () {
		

	}

    public void GenerateMap() {
        floorPlanner = GetComponent<FloorPlanner>();
        realizer = GetComponent<Realizer>();
        floorPlanner.Register(this);
        floorPlanner.PlanFloor();
    }

	public void Notify() {
		Debug.Log ("LEVEL GEN MED WAS NOTIFIED");
        //lets add stuff to all the template rooms
        floorPlanner.floorGraph.nodes[0].GetSubject().AddRoomElement(new PlayerSpawn());

		realizer.Realize (floorPlanner.floorGraph);

        foreach(Observer o in observers) {
            o.Notify();
        }
	}

    public void Register(Observer o) {
        observers.Add(o);
    }

    public void Deregister(Observer o) {
        observers.Remove(o);
    }
}
