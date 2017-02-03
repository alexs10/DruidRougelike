using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class MainState : GameState {
    private TurnKeeper turnkeeper;

    public MainState() {
        this.turnkeeper = TurnKeeper.GetInstance();
    }

    public void OnEnterState() {
        turnkeeper.StartTurning();
    }

    public void OnLeaveState() {
        turnkeeper.StopTurning();
    }

    public void Update() {

    }

}

