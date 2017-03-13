using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Map {
    class Hallway {
        public Room roomA;
        public Room roomB;
        public Key key;

        public Hallway(Room roomA, Room roomB) {
            if (!IsValidHallway(roomA, roomB))
                throw new ArgumentException();

            this.roomA = roomA;
            this.roomB = roomB;

            foreach (Key key in roomA.keyCode) {
                if (!roomB.keyCode.Contains(key)) {
                    this.key = key;
                }
            }

            foreach (Key key in roomB.keyCode) {
                if (!roomA.keyCode.Contains(key)) {
                    this.key = key;
                }
            }
            

        }

        public Room OtherEnd(Room self) {
            if (self == roomA) {
                return roomB;
            } else if (self == roomB) {
                return roomA;
            } else {
                throw new ArgumentException();
            }
        }

        public static bool IsValidHallway(Room roomA, Room roomB) {
            int differences = 0;
            foreach (Key key in roomA.keyCode) {
                if (!roomB.keyCode.Contains(key)) {
                    ++differences;
                }
            }
            foreach (Key key in roomB.keyCode) {
                if (!roomA.keyCode.Contains(key)) {
                    ++differences;
                }
            }

            return differences <= 1;
        }
    }
}
