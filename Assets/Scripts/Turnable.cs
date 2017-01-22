using System;
using System.Collections;


public delegate void EndTurnCallback(Turnable self, bool isStillActive, int turnSpeed, int hangingTimeMilliSeconds);

public interface Turnable {
    void TakeTurn(EndTurnCallback endTurnCallback);
}

