using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Map {
    public class Map {
        public Room[,] rooms;
		public Room currentRoom;

		//Used for generating the map
		private List<Room> roomsWithOpenAdjacencies;
		private List<Key> currentKeyCode;
		private int roomCount;
		private int keyIndex;
		private float keyLevelDifficulty;
		private float maxDifficulty;


        public Map() {
            rooms = new Room[MapConfig.WIDTH, MapConfig.HEIGHT];
            GenerateMap();
        }

        private void GenerateMap() {
			InitMapGen ();
			CreateHomeRoom ();
			for (int i = 1; i < MapConfig.ROOM_COUNT; i++) {
				AdjustKeyLevel (i);
				CreateRoomInOpenAdjacency ();
			}
            PlaceEnemies();
			PlaceKeys ();
        }



		//steps for map generation
		private void InitMapGen() {
			roomsWithOpenAdjacencies = new List<Room> ();
			currentKeyCode = new List<Key> ();
			roomCount = 0;
			keyLevelDifficulty = 0;
			keyIndex = 0;
		}

		private void CreateHomeRoom() {
			AddRoom (MapConfig.HOME_X, MapConfig.HOME_Y, 0f, currentKeyCode);
            currentRoom = rooms[MapConfig.HOME_X, MapConfig.HOME_Y];
		}

		private void AdjustKeyLevel(int roomCount) {
			if (roomCount/MapConfig.KEY_LEVEL_COUNT != (roomCount - 1)/MapConfig.KEY_LEVEL_COUNT) {
				Debug.Log ("KEY LEVEL UP");
				currentKeyCode.Add (MapConfig.KEY_SET [keyIndex++]);
				keyLevelDifficulty = maxDifficulty + MapConfig.KEY_LEVEL_DIFFICULTY_INC;
			}
		}

		private void CreateRoomInOpenAdjacency() {
            Room chosenRoom = roomsWithOpenAdjacencies [Random.Range (0, roomsWithOpenAdjacencies.Count - 1)];
            List<Position> availablePositions = FindAvailableAdjPositions(chosenRoom.x, chosenRoom.y);
			Position chosenPosition = availablePositions [Random.Range (0, availablePositions.Count - 1)];

			float difficulty;
			if (chosenRoom.keyCode == currentKeyCode) {
				difficulty = chosenRoom.GetRawDifficulty () + MapConfig.STD_DIFFICULTY_INC;
			} else {
				difficulty = keyLevelDifficulty;
			}

			AddRoom (chosenPosition.x, chosenPosition.y, difficulty, currentKeyCode, chosenRoom);
		}

        private List<Position> FindAvailableAdjPositions(int x, int y) {
            List<Position> output = new List<Position>();
            if (x > 0 && rooms[x - 1, y] == null)
                output.Add(new Position(x - 1, y));
            if (x < MapConfig.WIDTH - 1 && rooms[x + 1, y] == null)
                output.Add(new Position(x + 1, y));
            if (y > 0 && rooms[x, y - 1] == null)
                output.Add(new Position(x, y - 1));
            if (y < MapConfig.HEIGHT - 1 && rooms[x, y + 1] == null)
                output.Add(new Position(x, y + 1));
            return output; 
        }

        private void PlaceEnemies() {
            foreach (Room room in rooms) {
                if (room != null)
                    room.SpawnEnemies();
            }
        }

		private void PlaceKeys() {
			//TODO implement this method
		}

        private void AddRoom(int x, int y, float difficulty, List<Key> keyCode, Room parent) {
			AddRoom (x, y, difficulty, keyCode);
			parent.Attach (rooms [x, y]);


        }

		private void AddRoom(int x, int y, float difficulty, List<Key> keyCode) {
            Debug.Log("Adding room at: " + x + ", " + y);
			rooms [x, y] = new Room (x, y, difficulty, CopyKeyCode(), new SimpleLayoutFactory("Forrest", 12, 8));

			UpdateOpenRooms (x, y);
			UpdateOpenRooms (x+1, y);
			UpdateOpenRooms (x-1, y);
			UpdateOpenRooms (x, y+1);
			UpdateOpenRooms (x, y-1);
            if (difficulty > maxDifficulty) {
				maxDifficulty = difficulty;
			}
		}

		private void UpdateOpenRooms(int x, int y) {
			if (x < 0 || x >= MapConfig.WIDTH || y < 0 || y >= MapConfig.HEIGHT)
				return;
            Room room = rooms[x, y];
            if (room == null)
                return;

			if (FindAvailableAdjPositions(room.x, room.y).Count > 0) {
				if (!roomsWithOpenAdjacencies.Contains (room))
					roomsWithOpenAdjacencies.Add (room);
			} else {
				if (roomsWithOpenAdjacencies.Contains (room))
					roomsWithOpenAdjacencies.Remove (room);
			}
		}

		private List<Key> CopyKeyCode() {
			List<Key> output = new List<Key> ();
			foreach (Key key in currentKeyCode) {
				output.Add (key);
			}
			return output;
		}

		public static class MapConfig {
			public static int WIDTH = 10;
			public static int HEIGHT = 10;
			public static int ROOM_COUNT = 25;
			public static int KEY_LEVEL_COUNT = 7;
			public static int HOME_X = 0;
			public static int HOME_Y = 0;

			public static float STD_DIFFICULTY_INC = 1f;
			public static float KEY_LEVEL_DIFFICULTY_INC = -2f;

			public static Key[] KEY_SET = {new Key("red"), new Key("blue"), new Key("green"), new Key("magenta") };
		}
    }
}
