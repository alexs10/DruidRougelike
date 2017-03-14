using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Map {
    class SimpleSquareRoomLayout : RoomLayout{

        private List<Position> gridPositions;

        private int wallMin = 5;
        private int wallMax = 9;
        private int columns;
        private int rows;

        public SimpleSquareRoomLayout(AreaFactory factory, int width, int height) : base(factory) {
            this.columns = width;
            this.rows = height;
            InitialiseGrid();
            BoardSetup();
            LayoutAtRandom(wallPositions, wallMin, wallMax);
        }

        public override void AddEnemies(float difficulty) {
            LayoutAtRandom(enemyPositions, (int)Mathf.Floor(difficulty * 3f), (int)Mathf.Floor(difficulty * 5f));
        }

        void InitialiseGrid() {
            gridPositions = new List<Position>();
            for (int x = 1; x < columns - 1; x++) {
                for (int y = 1; y < rows - 1; y++) {
                    gridPositions.Add(new Position(x, y));
                }
            }
        }

        void BoardSetup() {
            for (int x = -1; x < columns + 1; x++) {
                for (int y = -1; y < rows + 1; y++) {
                    if (x == -1 || x == columns || y == -1 || y == rows)
                        outerWallPositions.Add(new Position(x, y));
                    else
                        floorPositions.Add(new Position(x, y));
                }
            }
        }

        void LayoutAtRandom(List<Position> targetList, int min, int max) {
            int count = Random.Range(min, max);
            
            for (int i = 0; i < count; i++) {
                targetList.Add(RandomUnusedPosition());
            }
            
        }

        Position RandomUnusedPosition() {
            int index = Random.Range(0, gridPositions.Count);
            Position returnIndex = gridPositions[index];
            gridPositions.RemoveAt(index);
            return returnIndex;
        }

        #region DOORS
        public override void AddDoorNorth(Room destination) {
            outerWallPositions.RemoveAll(w => w.x == columns / 2 && w.y == rows);
            doorPositions.Add(new Position(columns / 2, rows));
            doorDesinations.Add(destination);
        }

        public override void AddDoorSouth(Room destination) {
            outerWallPositions.RemoveAll(w => w.x == columns / 2 && w.y == -1);
            doorPositions.Add(new Position(columns / 2, -1));
            doorDesinations.Add(destination);
        }

        public override void AddDoorEast(Room destination) {
            outerWallPositions.RemoveAll(w => w.x == columns && w.y == rows / 2);
            doorPositions.Add(new Position(columns, rows / 2));
            doorDesinations.Add(destination);
        }

        public override void AddDoorWest(Room destination) {
            outerWallPositions.RemoveAll(w => w.x == -1 && w.y == rows / 2);
            doorPositions.Add(new Position(-1, rows / 2));
            doorDesinations.Add(destination);
        }

        public override void AddDoorNorth(Room destination, Key key) {
            AddDoorNorth(destination);
            lockPositions.Add(new Position(columns / 2, rows - 1));
            lockKeys.Add(key);
        }

        public override void AddDoorSouth(Room destination, Key key) {
            AddDoorSouth(destination);
            lockPositions.Add(new Position(columns / 2, 0));
            lockKeys.Add(key);
        }

        public override void AddDoorEast(Room destination, Key key) {
            AddDoorEast(destination);
            lockPositions.Add(new Position(columns - 1, rows / 2));
            lockKeys.Add(key);
        }

        public override void AddDoorWest(Room destination, Key key) {
            AddDoorWest(destination);
            lockPositions.Add(new Position(0, rows / 2));
            lockKeys.Add(key);
        }

        #endregion
    }
}
