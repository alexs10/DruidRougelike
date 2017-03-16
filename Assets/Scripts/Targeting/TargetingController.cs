using System.Collections.Generic;
using UnityEngine;

public class TargetingController : MonoBehaviour, Controllable, Observable {

    public GameObject targetSquarePrefab;

    private int BOARDWIDTH = 12;
    private int BOARDHEIGHT = 8;

    private List<Observer> observers;
    private Transform target = null;
    private int targetingLayer;
    private PlayerController controller;

    private List<TargetSquare> activeSquares;
    private TargetingStrategy targetingStrategy;

	// Use this for initialization
	void Start () {
        observers = new List<Observer>();

        this.controller = GameObject.Find("Player").GetComponent<PlayerController>();
        targetingLayer = LayerMask.GetMask("Targeting");
        activeSquares = new List<TargetSquare>();
        targetingStrategy = new SingleTargetStrategy();
        initTargeting();
    }
     
    public void initTargeting() {
		for (int i = 0; i< BOARDWIDTH; i++) {
			for (int j = 0; j< BOARDHEIGHT; j++) {
                GameObject instance = Instantiate(targetSquarePrefab, new Vector2(i, j), Quaternion.identity) as GameObject;
                instance.transform.SetParent(this.transform);
            }
        }
    }

    public void DefineValidTargets(List<Vector2> validTargets) {
        for (int i = 0; i < BOARDWIDTH; i++) {
            for (int j = 0; j < BOARDHEIGHT; j++) {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(i, j), Vector2.zero, 0f, targetingLayer);
                if (hit) {
                    hit.collider.GetComponent<TargetSquare>().SetNontargetable();
                }
            }
        }
        foreach (Vector2 target in validTargets) {
            RaycastHit2D hit = Physics2D.Raycast(target, Vector2.zero, 0f, targetingLayer);
            if (hit) {
                hit.collider.GetComponent<TargetSquare>().SetDefault();
            }
        }
    }

    public void ControlUpdate() {
        
        //Unselect each previously selected targetSquare
        foreach (TargetSquare square in activeSquares) {
            square.SetDefault();
        }
        activeSquares.Clear();
        //Find all the new targetSquares
        foreach(Vector2 vector in targetingStrategy.GetTargets(Input.mousePosition)) {
            Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(vector).x, Camera.main.ScreenToWorldPoint(vector).y);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f, targetingLayer);
            if(hit && !hit.collider.GetComponent<TargetSquare>().IsNontargetable()) {
                hit.collider.GetComponent<TargetSquare>().SetTargeted();
                activeSquares.Add(hit.collider.GetComponent<TargetSquare>());
            }
            
        }


        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f, targetingLayer);


            if (hit && !hit.collider.GetComponent<TargetSquare>().IsNontargetable()) {
                target = hit.transform;
                StopTargeting();
            } else {
                Debug.Log("MISS");
            }


        }
        if (Input.GetKey(KeyCode.Escape)) {
            StopTargeting();
        }
    }

    private void StopTargeting() {
        Reset();
        controller.ReturnState();
        NotifyObservers();
    }

    private void Reset() {
        activeSquares.Clear();
        for (int i = 0; i < BOARDWIDTH; i++) {
            for (int j = 0; j < BOARDHEIGHT; j++) {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(i, j), Vector2.zero, 0f, targetingLayer);
                if (hit) {
                    hit.collider.GetComponent<TargetSquare>().SetDefault();
                }
            }
        }
    }

    public bool HasTarget() {
        return target != null;
    }

    public Transform TakeTarget() {
        Transform tempTarget = target;
        target = null;
        return tempTarget;
    }


    public void Register(Observer o) {
        observers.Add(o);
    }

    public void Deregister(Observer o) {
        observers.Remove(o);
    }

    private void NotifyObservers() {
        //This cannot be a foreach loop because concurancy garbage
        for (int i = 0; i<observers.Count; i++) {
            observers[i].Notify();
        }
    }
}
