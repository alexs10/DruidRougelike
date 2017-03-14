using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Position {
    public int x, y;

    public Position(float x, float y) {
        this.x = (int)x;
        this.y = (int)y;
    }
    public Position(int x, int y) {
        this.x = x;
        this.y = y;
    }

}