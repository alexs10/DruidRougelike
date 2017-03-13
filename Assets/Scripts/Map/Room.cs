using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace Assets.Scripts.Map {
    class Room {
        public float difficulty;
        public List<Key> keyCode;

        public Hallway north;
        public Hallway south;
        public Hallway east;
        public Hallway west;

        public int x, y;

        public Room(int x, int y, float difficulty, List<Key> keyCode) {
            if (difficulty < 0f || difficulty > 1f)
                throw new ArgumentOutOfRangeException("difficulty");

            this.x = x;
            this.y = y;
            this.difficulty = difficulty;
            this.keyCode = keyCode;

        }

        public void Attach(Room otherRoom) {
            if (!Hallway.IsValidHallway(this, otherRoom)) {
                Debug.Log("Hallway cannot be created between two rooms");
                return;
            }



            Hallway hallway = new Hallway(this, otherRoom);
            if (IsNorth(otherRoom)) {
                this.north = hallway;
                otherRoom.south = hallway;
            } else if (IsSouth(otherRoom)) {
                this.south = hallway;
                otherRoom.north = hallway;
            } else if (IsEast(otherRoom)) {
                this.east = hallway;
                otherRoom.west = hallway;
            } else if (IsWest(otherRoom)) {
                this.west = hallway;
                otherRoom.east = hallway;
            } else {
                throw new ArgumentException();
            }
            
        }

        private bool IsAdjacent(Room otherRoom) {
            return IsNorth(otherRoom) || IsSouth(otherRoom) || IsEast(otherRoom) || IsWest(otherRoom);
        }

        private bool IsNorth(Room otherRoom) {
            return this.y - otherRoom.y == 1 && this.x - otherRoom.x == 0;
        }

        private bool IsSouth(Room otherRoom) {
            return this.y - otherRoom.y == -1 && this.x - otherRoom.x == 0;
        }

        private bool IsEast(Room otherRoom) {
            return this.x - otherRoom.x == 1 && this.y - otherRoom.y == 0;
        }

        private bool IsWest(Room otherRoom) {
            return this.x - otherRoom.x == -1 && this.y - otherRoom.y == 0;
        }

        public bool HasOpenAdjacency() {
            return north == null || south == null || east == null || west == null;
        }

        public List<Direction> FindOpenAdjacencies() {
            List<Direction> output = new List<Direction>();

            if (north != null) output.Add(Direction.North);
            if (south != null) output.Add(Direction.South);
            if (east != null) output.Add(Direction.East);
            if (west != null) output.Add(Direction.West);

            return output;
        }

        public enum Direction {
            North, South, East, West
        }
    }
}
