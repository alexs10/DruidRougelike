using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace Assets.Scripts.Map {
    public class Room {
		//true difficulty = difficulty/maxDifficulty
		private static float MAX_DIFFICULTY = 1;
		private float difficulty;

        public RoomLayout layout;
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
            this.keyCode = keyCode;

			this.difficulty = difficulty;
			if (difficulty > MAX_DIFFICULTY) {
				MAX_DIFFICULTY = difficulty;
			}
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

		public float GetDifficulty() {
			return difficulty / MAX_DIFFICULTY;
		}

		public float GetRawDifficulty() {
			return difficulty;
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
			return 	(north == null && y < Map.MapConfig.HEIGHT - 1) || 
				(south == null && y > 0 ) || 
				(east == null && x < Map.MapConfig.WIDTH - 1) || 
				(west == null && x > 0);
        }

		public List<Position> FindOpenAdjacencies() {
			List<Position> output = new List<Position>();

			if (north != null && y < Map.MapConfig.HEIGHT - 1) output.Add(new Position(x, y + 1));
			if (south != null && y > 0) output.Add(new Position(x, y - 1));
			if (east != null && x < Map.MapConfig.WIDTH - 1) output.Add(new Position(x + 1, y));
			if (west != null && x > 0) output.Add(new Position(x - 1, y));

            return output;
        }

    }
}
