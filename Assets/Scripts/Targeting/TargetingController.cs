using System.Collections.Generic;
using UnityEngine;

public class TargetingController : MonoBehaviour, Controllable {

    public GameObject targetSquarePrefab;

    [HideInInspector]
    public Transform target = null;
    private int targetingLayer;
    private PlayerController controller;

    private List<TargetSquare> activeSquares;
    private TargetingStrategy targetingStrategy;

	// Use this for initialization
	void Start () {
        this.controller = GameObject.Find("Player").GetComponent<PlayerController>();
        targetingLayer = LayerMask.GetMask("Targeting");
        activeSquares = new List<TargetSquare>();
        targetingStrategy = new SingleTargetStrategy();
        initTargeting();
    }
     
    public void initTargeting() {
        for (int i = 0; i<8; i++) {
            for (int j = 0; j<8; j++) {
                GameObject instance = Instantiate(targetSquarePrefab, new Vector2(i, j), Quaternion.identity) as GameObject;
                instance.transform.SetParent(this.transform);
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
            if(hit) {
                hit.collider.GetComponent<TargetSquare>().SetTargeted();
                activeSquares.Add(hit.collider.GetComponent<TargetSquare>());
            }
            
        }


        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f, targetingLayer);


            if (hit) {
                Debug.Log(hit.transform.name);
                target = hit.transform;
                StopTargeting();
                //hit.collider.GetComponent<TargetSquare>().SetTargeted();
            } else {
                Debug.Log("MISS");
            }


        }
        if (Input.GetKey(KeyCode.Escape)) {
            controller.ReturnState();
        }
    }

    private void StopTargeting() {
        controller.ReturnState();
    }



}
