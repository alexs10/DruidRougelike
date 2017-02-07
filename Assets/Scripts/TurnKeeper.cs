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
    }

    public void StartTurning() {
        nextTurn = true;
    }

    public void StopTurning() {
        nextTurn = false;
    }

    public void Update() {
        if (nextTurn && HasTurnable()) {
            Turnable nextTurnable;
            if ((nextTurnable = NextTurnable()).IsActive()) {
                nextTurnable.TakeTurn(new EndTurnCallback(StandardEndTurnCallback));
                nextTurn = false;
            }
                
        }
    }

    private void StandardEndTurnCallback(Turnable target, bool isStillActive, int turnSpeed, int timeHangingMilliSeconds) {
        new Timer((obj) => {
            nextTurn = true;
            if (isStillActive)
                AddToTurnables(turnSpeed, target);
        },null, timeHangingMilliSeconds, Timeout.Infinite);
        
    }


    // Use this for initialization

    public void Register(Turnable turnable, int turnsInactive) {
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
        while (currentTurnables == null || currentTurnables.Count == 0) {
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
