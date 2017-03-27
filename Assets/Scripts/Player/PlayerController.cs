using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //maybe this is better as a stack of states?
    private Controllable defaultState;
	private Controllable inventoryState;
    private Controllable currentState;

    public bool inactive = true;
	// Use this for initialization
	void Start () {
        defaultState = GetComponent<Player>();
		inventoryState = GetComponent<InventoryController> ();
        currentState = defaultState;
	}
	
	// Update is called once per frame
	void Update () {
		if (!inactive) {
			currentState.ControlUpdate ();
		}
    }

	public void OpenInventory() {
		ChangeState (inventoryState);
	}

    public void ChangeState(Controllable newState) {
		currentState.TearDown ();
        currentState = newState;
		currentState.Setup ();
    }

    public void ReturnState() {
		ChangeState (defaultState);
    }


    public void setInactive(bool inactive) {
        this.inactive = inactive;
    }
}
