﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Map {
    public class Map {
        public Room[,] rooms;
		public Room currentRoom;
		public Room room; 

		//Used for generating the map
		private List<Room> roomsWithOpenAdjacencies;
		private List<Key> currentKeyCode;
		private int roomCount;
		private int keyIndex;
		private float keyLevelDifficulty;
		private float maxDifficulty;

		private Room maxDifficultyCurrentLevel;
		private float difficultyThresh;
		private List<Room> keyRooms;
		private List<int> pickupRooms;

        public Map() {
            rooms = new Room[MapConfig.WIDTH, MapConfig.HEIGHT];
            GenerateMap();
        }

        private void GenerateMap() {
			InitMapGen ();
			CreateHomeRoom ();
			for (int i = 1; i < MapConfig.ROOM_COUNT; i++) {
				AdjustKeyLevel (i);
				CreateRoomInOpenAdjacency (i);
			}
			PlaceBoss ();
            PlaceEnemies();
			PlaceKeys ();

        }



		//steps for map generation
		private void InitMapGen() {
			roomsWithOpenAdjacencies = new List<Room> ();
			currentKeyCode = new List<Key> ();
			keyRooms = new List<Room> ();
			roomCount = 0;
			keyLevelDifficulty = 0;
			keyIndex = 0;
            difficultyThresh = 0f;
			pickupRooms = new List<int> ();
			for (int i = 0; i < MapConfig.PICKUP_SET.Length; i++) {
				pickupRooms.Add (Random.Range (1, MapConfig.ROOM_COUNT));
			}
		}

		private void CreateHomeRoom() {
			AddRoom (MapConfig.HOME_X, MapConfig.HOME_Y, 0f, CopyKeyCode(currentKeyCode));
            currentRoom = rooms[MapConfig.HOME_X, MapConfig.HOME_Y];

            maxDifficultyCurrentLevel = rooms[MapConfig.HOME_X, MapConfig.HOME_Y];
        }

		private void AdjustKeyLevel(int roomCount) {
			if (roomCount/MapConfig.KEY_LEVEL_COUNT != (roomCount - 1)/MapConfig.KEY_LEVEL_COUNT) {
				Debug.Log ("KEY LEVEL UP");
				currentKeyCode.Add (MapConfig.KEY_SET [keyIndex++]);
				keyLevelDifficulty = maxDifficulty + MapConfig.KEY_LEVEL_DIFFICULTY_INC;

                Debug.Log("Adding keyroom at: " + maxDifficultyCurrentLevel.x + ", " + maxDifficultyCurrentLevel.y);
				keyRooms.Add (maxDifficultyCurrentLevel);
				difficultyThresh = keyLevelDifficulty;
			}
		}

		private void CreateRoomInOpenAdjacency(int roomCount) {
            Room chosenRoom = roomsWithOpenAdjacencies [Random.Range (0, roomsWithOpenAdjacencies.Count - 1)];
            List<Position> availablePositions = FindAvailableAdjPositions(chosenRoom.x, chosenRoom.y);
			Position chosenPosition = availablePositions [Random.Range (0, availablePositions.Count - 1)];

			float difficulty;
			if (chosenRoom.keyCode.Count == currentKeyCode.Count) {
				Debug.Log ("keycodes match");
				difficulty = chosenRoom.GetRawDifficulty () + MapConfig.STD_DIFFICULTY_INC;

			} else {
				Debug.Log ("keycodes dont match");
				difficulty = keyLevelDifficulty;
			}

			AddRoom (chosenPosition.x, chosenPosition.y, difficulty, currentKeyCode, chosenRoom);

			//if this room should have a pickup, add it
			if (ShouldHavePickup(roomCount)) {
				Debug.Log ("*********************************************");
				Debug.Log ("Creating pickup at index: " + roomCount);
				rooms [chosenPosition.x, chosenPosition.y].AddPickup (NextPickup());
			}

			if (rooms [chosenPosition.x, chosenPosition.y].GetRawDifficulty () >= difficultyThresh) {
                Debug.Log("New max difficulty current level: " + rooms[chosenPosition.x, chosenPosition.y].GetRawDifficulty());
				maxDifficultyCurrentLevel = rooms [chosenPosition.x, chosenPosition.y];
			}
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
					//Debug.Log("Room (" + room.x + ", " + room.y + ") difficulty: " + room.GetDifficulty());
                    room.SpawnEnemies();
            }
        }
			


		private void PlaceKeys() {
			//First spawn all keyLevel keys
			Debug.Log("KeyRoom.length: " + keyRooms.Count);
			for (int i = 0; i < currentKeyCode.Count; i++) {
				Debug.Log ("ADDING KEY AT " + keyRooms [i].x + ", " + keyRooms [i].y);
				keyRooms [i].AddKey (currentKeyCode [i]);
			}
			//Then spawn boss key
			maxDifficultyCurrentLevel.AddKey(new Key("Boss"));

		}

		private void PlaceBoss() {
			bool validBoss = false;
			while(!validBoss) {
				Room chosenAttempt = roomsWithOpenAdjacencies [Random.Range (0, roomsWithOpenAdjacencies.Count - 1)];
				List<Position> availablePositionsEntrance = FindAvailableAdjPositions (chosenAttempt.x, chosenAttempt.y);
				foreach  (Position pos in availablePositionsEntrance) {
					List<Position> availablePositionsBoss = FindAvailableAdjPositions (pos.x, pos.y); 
					if (availablePositionsBoss.Count > 0) {
						Debug.Log ("Boss ENtrance at: " + pos.x + ", " + pos.y);

						Debug.Log ("entrance keys");
						foreach (Key key in chosenAttempt.keyCode) {
							Debug.Log ("\tKey: " + key.keyString);
						}

						//We found a valid boss entrance room
						//add entrance
						Debug.Log("Attaching entrance to " + chosenAttempt.x + ", " + chosenAttempt.y);
						AddRoom(pos.x, pos.y, chosenAttempt.GetRawDifficulty(), chosenAttempt.keyCode, chosenAttempt);

						Room entrance = rooms [pos.x, pos.y];
						Position bossPos = availablePositionsBoss [Random.Range (0, availablePositionsBoss.Count - 1)];

						List<Key> bossKeyCode = CopyKeyCode (currentKeyCode);
						bossKeyCode.Add (new Key ("Boss"));
						AddRoom (bossPos.x, bossPos.y, maxDifficulty, bossKeyCode, entrance, new BossLayoutFactory ("Boss", 12, 8));
						Debug.Log ("Boss Room at: " + bossPos.x + ", " + bossPos.y);
						validBoss = true;
						break;

					}
				}
		
			}
		}
        
        private void Graphify() {

        }

		private void AddRoom(int x, int y, float difficulty, List<Key> keyCode, Room parent, ILayoutFactory layout) {
			AddRoom (x, y, difficulty, keyCode, layout);
			parent.Attach (rooms [x, y]);
		}

        private void AddRoom(int x, int y, float difficulty, List<Key> keyCode, Room parent) {
			AddRoom (x, y, difficulty, keyCode);
			parent.Attach (rooms [x, y]);


        }

		private void AddRoom(int x, int y, float difficulty, List<Key> keyCode) {
			AddRoom(x, y, difficulty, keyCode, new SimpleLayoutFactory("Forrest", 12, 8));
		}

		private void AddRoom(int x, int y, float difficulty, List<Key> keyCode, ILayoutFactory layout) {
            Debug.Log("Adding room at: " + x + ", " + y);
			rooms [x, y] = new Room (x, y, difficulty, CopyKeyCode(keyCode), layout);
			Debug.Log ("Keys: ");
			foreach (Key key in keyCode) {
				Debug.Log ("\tKey: " + key.keyString);
			}

			UpdateOpenRooms (x, y);
			UpdateOpenRooms (x+1, y);
			UpdateOpenRooms (x-1, y);
			UpdateOpenRooms (x, y+1);
			UpdateOpenRooms (x, y-1);
            if (difficulty > maxDifficulty) {
				maxDifficulty = difficulty;
			}
		}

		private bool ShouldHavePickup(int index) {
			return pickupRooms.Contains (index);
		}

		int pickupIndex = 0;
		private string NextPickup() {
			return MapConfig.PICKUP_SET [pickupIndex++];
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

		private List<Key> CopyKeyCode(List<Key> keyCode) {
			List<Key> output = new List<Key> ();
			foreach (Key key in keyCode) {
				output.Add (key);
			}
			return output;
		}

		public static class MapConfig {
			public static int WIDTH = 10;
			public static int HEIGHT = 10;
			public static int ROOM_COUNT = 8;
			public static int KEY_LEVEL_COUNT = 3;
			public static int HOME_X = 0;
			public static int HOME_Y = 0;

			public static float STD_DIFFICULTY_INC = 3f;
			public static float KEY_LEVEL_DIFFICULTY_INC = 0f;

			public static Key[] KEY_SET = {new Key("Blue"), new Key("Yellow"), new Key("Magenta"), new Key("White") };
			public static string[] PICKUP_SET = { "Push", "Shoot" };
		}
    }
}
