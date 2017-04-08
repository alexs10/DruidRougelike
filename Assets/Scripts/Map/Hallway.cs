using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Map {
    public class Hallway {
        public Room roomA;
        public Room roomB;
        public Key key;

        public Hallway(Room roomA, Room roomB) {
            //if (!IsValidHallway(roomA, roomB))
            //    throw new ArgumentException();



            this.roomA = roomA;
            this.roomB = roomB;


            if (roomA.keyCode.Count > roomB.keyCode.Count) {
				Debug.Log ("RoomA key" + roomA.keyCode.Last ().keyString);

                this.key = roomA.keyCode.Last();
            } else if (roomB.keyCode.Count > roomA.keyCode.Count()) {
				Debug.Log ("RoomB key" + roomB.keyCode.Last ().keyString);

                this.key = roomB.keyCode.Last();
            }

            foreach (Key key in roomA.keyCode) {
                if (!roomB.keyCode.Contains(key)) {
                    //this.key = key;
                }
            }

            foreach (Key key in roomB.keyCode) {
                if (!roomA.keyCode.Contains(key)) {
                    //this.key = key;
                }
            }
        }

		public bool HasKey() {
			Debug.Log ("Asize: " + roomA.keyCode.Count + " Bsize: " + roomB.keyCode.Count);
			return key != null;
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
            Debug.Log("IsValidHallway?");
            foreach (Key key in roomA.keyCode) {
                if (!roomB.keyCode.Contains(key)) {
                    Debug.Log("difference: " + key.keyString);
                    ++differences;
                }
            }
            foreach (Key key in roomB.keyCode) {
                if (!roomA.keyCode.Contains(key)) {
                    Debug.Log("difference: " + key.keyString);
                    ++differences;
                }
            }

            return differences <= 1;
        }
    }
}
