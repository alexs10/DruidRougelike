using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPlanner : MonoBehaviour, Observable {
    private const int TOTAL_ROOM_COUNT = 40;
    private const int MIN_ROOM_SIZE = 3;
    private const int MAX_ROOM_SIZE = 15;
    private const int INITIAL_ROOM_RADIUS = 25;
    private const int AREA_THRESHOLD = 100;
    private Transform level;

	private List<Observer> observers;

	public Graph<TemplateRoom> floorGraph;

	void Awake() {
		observers = new List<Observer> ();
	}

	// Use this for initialization
	void Start () {
		//observers = new List<Observer> ();
        Random.InitState(1337);
        //CreateFloor();
        //StartCoroutine(CheckFloorStopMoving());
        //wait for floor to stop moving

	}

	public void PlanFloor() {
		CreateFloor ();
		StartCoroutine (CheckFloorStopMoving ());
	}

    void ContinueRoomGen() {
        List<TemplateRoom> mainRooms = FindMainRooms();
		List<Node<TemplateRoom>> graphNodes = new List<Node<TemplateRoom>> ();
		foreach (TemplateRoom room in mainRooms) {
			graphNodes.Add (new Node<TemplateRoom> (room));
		}
		Graph<TemplateRoom> mainRoomGraphConnected = new ConnectedGraph<TemplateRoom> (new DistanceWeightStrategy<TemplateRoom> (), graphNodes);
		floorGraph = mainRoomGraphConnected.MinimumSpanningTree ();
		foreach (Observer observer in observers) {
			observer.Notify ();
		}
    }
	
	void CreateFloor() {
        level = new GameObject("Level").transform;
        for (int i=0; i<TOTAL_ROOM_COUNT; i++) {

            GameObject templateRoom = new GameObject("TemplateRoom");

            //Add a rigid body for seperation physics
            Rigidbody2D rb2d = templateRoom.AddComponent<Rigidbody2D>();
            rb2d.freezeRotation = true;
            rb2d.gravityScale = 0;

            //Add a box collider with a random size
            BoxCollider2D box = templateRoom.AddComponent<BoxCollider2D>();
            box.size = GetRandomRoomSize();
			box.offset = box.size / 2;

            //so I can see the boxes in testing
            //templateRoom.AddComponent<TextureDrawer>();

            templateRoom.AddComponent<TemplateRoom>();

            //finish setting up room
            templateRoom.transform.position = GetRandomPointInCircle();
            templateRoom.transform.SetParent(level);
        }
    }

    List<TemplateRoom> FindMainRooms() {
        List<TemplateRoom> output = new List<TemplateRoom>();
        for (int i=0; i<level.childCount; i++) {
            if (level.GetChild(i).GetComponent<TemplateRoom>().GetArea() >= AREA_THRESHOLD) {
                output.Add(level.GetChild(i).GetComponent<TemplateRoom>());
            }
        }
        Debug.Log("Found " + output.Count + " main rooms");
        return output;
    }
    

    private Vector2 GetRandomRoomSize() {
        return new Vector2(Mathf.Floor(Random.value * (MAX_ROOM_SIZE - MIN_ROOM_SIZE) + MIN_ROOM_SIZE),
                Mathf.Floor(Random.value * (MAX_ROOM_SIZE - MIN_ROOM_SIZE) + MIN_ROOM_SIZE));
    }

    private Vector2 GetRandomPointInCircle() {
        Vector2 randomRad = Random.insideUnitCircle* INITIAL_ROOM_RADIUS;
        return new Vector2(Mathf.Floor(randomRad.x), Mathf.Floor(randomRad.y));
    }

    IEnumerator CheckFloorStopMoving() {
        print("checking... ");
        List<Rigidbody2D> templateRooms = new List<Rigidbody2D>();
        for (int i = 0; i < level.childCount; i++) {
            templateRooms.Add(level.GetChild(i).GetComponent<Rigidbody2D>());
        }
        bool allSleeping = false;

        while (!allSleeping) {
            allSleeping = true;
            print(templateRooms.Count);

            foreach (Rigidbody2D templateRoom in templateRooms) {
                if (!templateRoom.IsSleeping()) {
                    print("objects still moving");
                    allSleeping = false;
                    yield return null;
                    break;
                }
            }

        }
        print("All objects sleeping");
        ContinueRoomGen();
    }

	public void Register(Observer o) {
		observers.Add(o);
	}

	public void Deregister(Observer o) {
		observers.Remove (o);
	}


}
