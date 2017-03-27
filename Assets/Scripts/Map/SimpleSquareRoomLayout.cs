using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Map {
    class SimpleSquareRoomLayout : RoomLayout{

        private List<Position> gridPositions;

        private int wallMin = 5;
        private int wallMax = 9;
		private int itemMin = 2; 
		private int itemMax = 4; 
        private int columns;
        private int rows;

		private Position playerPositionSouth = new Position(0, 0);
		private Position playerPositionNorth = new Position(0, 0);
		private Position playerPositionEast = new Position(0, 0);
		private Position playerPositionWest = new Position(0, 0);

        public SimpleSquareRoomLayout(AreaFactory factory, int width, int height) : base(factory) {
            this.columns = width;
            this.rows = height;
            InitialiseGrid();
            BoardSetup();
            LayoutAtRandom(wallPositions, wallMin, wallMax);
			LayoutAtRandom (itemPositions, itemMin, itemMax); 
        }

        public override void AddEnemies(float difficulty) {
            //LayoutAtRandom(enemyPositions, (int)Mathf.Floor(difficulty * 3f), (int)Mathf.Floor(difficulty * 5f));
			LayoutAtRandom(enemyPositions, 2, 3);
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

		public override void AddKey(Key key) {
			keyPositions.Add (RandomUnusedPosition ());
			keys.Add (key);
		}

		#region PLAYER POSITION
		public override Position GetPlayerPositionNorth ()
		{
			return playerPositionNorth;
		}

		public override Position GetPlayerPositionSouth ()
		{
			return playerPositionSouth;
		}

		public override Position GetPlayerPositionEast ()
		{
			return playerPositionEast;
		}

		public override Position GetPlayerPositionWest ()
		{
			return playerPositionWest;
		}
		#endregion

        #region DOORS
        public override void AddDoorNorth(Room destination) {
            outerWallPositions.RemoveAll(w => w.x == columns / 2 && w.y == rows);
            doorPositions.Add(new Position(columns / 2, rows));
            doorDesinations.Add(destination);
			doorDirections.Add (Direction.NORTH);
			playerPositionNorth = new Position (columns / 2, rows - 1);
        }

        public override void AddDoorSouth(Room destination) {
            outerWallPositions.RemoveAll(w => w.x == columns / 2 && w.y == -1);
            doorPositions.Add(new Position(columns / 2, -1));
            doorDesinations.Add(destination);
			doorDirections.Add (Direction.SOUTH);
			playerPositionSouth = new Position (columns / 2, 0);
        }

        public override void AddDoorEast(Room destination) {
            outerWallPositions.RemoveAll(w => w.x == columns && w.y == rows / 2);
            doorPositions.Add(new Position(columns, rows / 2));
            doorDesinations.Add(destination);
			doorDirections.Add (Direction.EAST);
			playerPositionEast = new Position (columns - 1, rows / 2);
        }

        public override void AddDoorWest(Room destination) {
            outerWallPositions.RemoveAll(w => w.x == -1 && w.y == rows / 2);
            doorPositions.Add(new Position(-1, rows / 2));
            doorDesinations.Add(destination);
			doorDirections.Add (Direction.WEST);		
			playerPositionWest = new Position (0, rows / 2);
        }

        public override void AddDoorNorth(Room destination, Key key) {
            AddDoorNorth(destination);
			playerPositionNorth.x += 1;
            lockPositions.Add(new Position(columns / 2, rows - 1));
            lockKeys.Add(key);
        }

        public override void AddDoorSouth(Room destination, Key key) {
            AddDoorSouth(destination);
			playerPositionSouth.x -= 1;
            lockPositions.Add(new Position(columns / 2, 0));
            lockKeys.Add(key);
        }

        public override void AddDoorEast(Room destination, Key key) {
            AddDoorEast(destination);
			playerPositionEast.y += 1;
            lockPositions.Add(new Position(columns - 1, rows / 2));
            lockKeys.Add(key);
        }

        public override void AddDoorWest(Room destination, Key key) {
            AddDoorWest(destination);
			playerPositionWest.y += 1;
            lockPositions.Add(new Position(0, rows / 2));
            lockKeys.Add(key);
        }

        #endregion
    }
}
