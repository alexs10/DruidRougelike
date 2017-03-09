using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //maybe this is better as a stack of states?
    private Controllable defaultState;
    private Controllable currentState;

    public bool inactive = true;
	// Use this for initialization
	void Start () {
        defaultState = GetComponent<Player>();
        currentState = defaultState;
	}
	
	// Update is called once per frame
	void Update () {
        if (!inactive) {
            currentState.ControlUpdate();
        }
    }

    public void ChangeState(Controllable newState) {
        this.currentState = newState;
    }

    public void ReturnState() {
        this.currentState = defaultState;
    }


    public void setInactive(bool inactive) {
        this.inactive = inactive;
    }
}
