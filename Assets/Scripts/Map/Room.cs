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

        public Hallway north = null;
        public Hallway south = null;
        public Hallway east = null;
        public Hallway west = null;

        public int x, y;

        public bool isRevealed = false;

        public Room(int x, int y, float difficulty, List<Key> keyCode, ILayoutFactory layoutFactory) {
            this.x = x;
            this.y = y;
            this.keyCode = keyCode;

            layout = layoutFactory.createLayout();

			this.difficulty = difficulty;
			if (difficulty > MAX_DIFFICULTY) {
				MAX_DIFFICULTY = difficulty;
			}
        }

        public void Attach(Room otherRoom) {
            //if (!Hallway.IsValidHallway(this, otherRoom)) {
            //    Debug.Log("Hallway cannot be created between two rooms");
            //    return;
            //}



            Hallway hallway = new Hallway(this, otherRoom);
			bool createLock = hallway.HasKey ();

            if (IsNorth(otherRoom)) {
                this.north = hallway;
                otherRoom.south = hallway;
				if (createLock) {
					layout.AddDoorNorth (otherRoom, hallway.key);
					otherRoom.layout.AddDoorSouth (this, hallway.key);
				} else {
					layout.AddDoorNorth(otherRoom);
					otherRoom.layout.AddDoorSouth(this);
				}
                
            } else if (IsSouth(otherRoom)) {
                this.south = hallway;
                otherRoom.north = hallway;
				if (createLock) {
					layout.AddDoorSouth (otherRoom, hallway.key);
					otherRoom.layout.AddDoorNorth (this, hallway.key);
				} else {
					layout.AddDoorSouth(otherRoom);
					otherRoom.layout.AddDoorNorth(this);
				}
            } else if (IsEast(otherRoom)) {
                this.east = hallway;
				otherRoom.west = hallway;
				if (createLock) {
					layout.AddDoorEast (otherRoom, hallway.key);
					otherRoom.layout.AddDoorWest (this, hallway.key);
				} else {
					layout.AddDoorEast(otherRoom);
					otherRoom.layout.AddDoorWest(this);
				}
            } else if (IsWest(otherRoom)) {
                this.west = hallway;
                otherRoom.east = hallway;
				if (createLock) {
					layout.AddDoorWest (otherRoom, hallway.key);
					otherRoom.layout.AddDoorEast (this, hallway.key);
				} else {
					layout.AddDoorWest(otherRoom);
					otherRoom.layout.AddDoorEast(this);
				}            
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
            return this.y - otherRoom.y == -1 && this.x - otherRoom.x == 0;
        }

        private bool IsSouth(Room otherRoom) {
            return this.y - otherRoom.y == 1 && this.x - otherRoom.x == 0;
        }

        private bool IsEast(Room otherRoom) {
            return this.x - otherRoom.x == -1 && this.y - otherRoom.y == 0;
        }

        private bool IsWest(Room otherRoom) {
            return this.x - otherRoom.x == 1 && this.y - otherRoom.y == 0;
        }

        public bool HasOpenAdjacency() {
			return 	(north == null && y < Map.MapConfig.HEIGHT - 1) || 
				(south == null && y > 0 ) || 
				(east == null && x < Map.MapConfig.WIDTH - 1) || 
				(west == null && x > 0);
        }

		public List<Position> FindOpenAdjacencies() {
			List<Position> output = new List<Position>();

			if (north == null && y < Map.MapConfig.HEIGHT - 1) output.Add(new Position(x, y + 1));
			if (south == null && y > 0) output.Add(new Position(x, y - 1));
			if (east == null && x < Map.MapConfig.WIDTH - 1) output.Add(new Position(x + 1, y));
			if (west == null && x > 0) output.Add(new Position(x - 1, y));

            return output;
        }

        public List<Position> FindClosedAdjacencies() {
            List<Position> output = new List<Position>();

            if (north != null) output.Add(new Position(x, y + 1));
            if (south != null) output.Add(new Position(x, y - 1));
            if (east != null) output.Add(new Position(x + 1, y));
            if (west != null) output.Add(new Position(x - 1, y));

            return output;
        }

        public void SpawnEnemies() {
            layout.AddEnemies(GetDifficulty());
        }
			

		public void AddKey(Key key) {
			layout.AddKey (key);
		}

		public void AddPickup(string name) {
			layout.AddPickup (name);
		}

		public Position GetPlayerPosition(Direction fromDirection) {
			switch (fromDirection) {
			case Direction.NORTH:
				return layout.GetPlayerPositionSouth();
				break;
			case Direction.SOUTH:
				return layout.GetPlayerPositionNorth();
				break;
			case Direction.EAST:
				return layout.GetPlayerPositionWest();
				break;
			case Direction.WEST: 
				return layout.GetPlayerPositionEast();
				break;
			}

			return new Position (0, 0);
		}
    }
}
