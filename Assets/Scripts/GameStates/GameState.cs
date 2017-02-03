using System;



interface GameState {
    void OnEnterState();
    void OnLeaveState();
    void Update();
}
