using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Threading;



public class TurnKeeper : MonoBehaviour {
    public static TurnKeeper instance = null;
    public float turnDelay = 0.5f;
    private int currentTurn = 0;
    private Dictionary<int, List<Turnable>> turnables = new Dictionary<int, List<Turnable>>();
    private List<Turnable> currentTurnables = null;

    private bool turning = false;
    private bool nextTurn = false;

    public static TurnKeeper GetInstance() {
        return instance;
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Debug.Log("TurnKeeper");
        StartTurn();
    }

    public void StartTurn() {
        nextTurn = true;
    }

    public void Update() {
        if (nextTurn && HasTurnable()) {
            NextTurnable().TakeTurn(new EndTurnCallback(StandardEndTurnCallback));
            nextTurn = false;
            Debug.Log("finished taketurn");
        }
    }

    private void StandardEndTurnCallback(Turnable target, bool isStillActive, int turnSpeed, int timeHangingMilliSeconds) {
        Debug.Log("callback ahs been called");
        Timer timer = new Timer((obj) => {
            Debug.Log("    in the callback timer");
            nextTurn = true;
            if (isStillActive)
                AddToTurnables(turnSpeed, target);
        },null, timeHangingMilliSeconds, Timeout.Infinite);
        
    }


    // Use this for initialization

    public void Register(Turnable turnable, int turnsInactive) {
        Debug.Log("about to regisester to turn keeper");
        AddToTurnables(currentTurn + turnsInactive, turnable);
    }

    //public void TakeTurn() {
    //    if (!hasTurnable()) {
    //        turnQueued = true;
    //        return;
    //    }

    //    NextTurnable().TakeTurn();
    //}



    private bool HasTurnable() {
        return turnables.Count > 0;
    }


    private Turnable NextTurnable() {
        Debug.Log("about to get next turnable");
        while (currentTurnables == null || currentTurnables.Count == 0) {
            Debug.Log("Current turn: " + currentTurn);
            turnables.TryGetValue(++currentTurn, out currentTurnables);
        }
        Turnable output = currentTurnables[0];
        currentTurnables.Remove(output);
        return output;


        
    }


    public void Reset() {
        this.turnables = new Dictionary<int, List<Turnable>>();
        currentTurn = 0;
    }


    private void AddToTurnables(int turnNumber, Turnable toBeAdded) {
        if (turnNumber < currentTurn) return;

        List<Turnable> targetTurnList;
        if (!turnables.TryGetValue(turnNumber, out targetTurnList)) {
            targetTurnList = new List<Turnable>();
        }

        targetTurnList.Add(toBeAdded);

        turnables[turnNumber] = targetTurnList;
        
    }
}
