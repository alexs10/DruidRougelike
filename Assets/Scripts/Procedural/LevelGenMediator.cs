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
        //change this to traverse the rooms and add stuff in then

        //this will cause a bug with players spawning in walls
        floorPlanner.floorGraph.nodes[0].GetSubject().AddRoomElement(new PlayerSpawn());
        IRoomPlanner roomPlanner = new PlainsRoomPlanner();

        foreach(Node<TemplateRoom> node in floorPlanner.floorGraph.nodes) {
            roomPlanner.Plan(node.GetSubject(), 2);
        }

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
