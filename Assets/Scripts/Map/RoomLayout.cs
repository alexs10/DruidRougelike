using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Map {
	public abstract class RoomLayout {

        private AreaFactory areaFactory;
        private Transform boardHolder;                                  

        protected List<Position> wallPositions = new List<Position>();
        protected List<Position> floorPositions = new List<Position>();
        protected List<Position> outerWallPositions = new List<Position>();

        protected List<Position> doorPositions = new List<Position>();
        protected List<Room> doorDesinations = new List<Room>();

        protected List<Position> lockPositions = new List<Position>();
        protected List<Key> lockKeys = new List<Key>();

        public RoomLayout(AreaFactory factory) {
            this.areaFactory = factory;
        }

        public abstract void AddDoorNorth(Room destination, Key key);
        public abstract void AddDoorSouth(Room destination, Key key);
        public abstract void AddDoorEast(Room destination, Key key);
        public abstract void AddDoorWest(Room destination, Key key);
        public abstract void AddDoorNorth(Room destination);
        public abstract void AddDoorSouth(Room destination);
        public abstract void AddDoorEast(Room destination);
        public abstract void AddDoorWest(Room destination);

        public void BuildRoom() {
            areaFactory.ClearBoard();

            //Random.InitState(GetHashCode());
            foreach(Position pos in wallPositions) {
                areaFactory.createWall(Random.Range(0, 1000), pos);
            }
            foreach (Position pos in floorPositions) {
                areaFactory.createFloor(Random.Range(0, 1000), pos);
            }
            foreach (Position pos in outerWallPositions) {
                areaFactory.createOuterWall(Random.Range(0, 1000), pos);
            }
            for (int i = 0; i < doorPositions.Count; i++) {
                areaFactory.createDoor(doorDesinations[i], doorPositions[i]);
            }
            for (int i = 0; i < lockPositions.Count; i++) {
                areaFactory.createLock(lockKeys[i].keyString, lockPositions[i]);
            }
        }
	}

}

