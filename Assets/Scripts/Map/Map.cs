using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Map {
    class Map {
        public Room[,] rooms;
        private List<Room> roomsWithOpenAdjacencies;
        private int roomCount;

        public Map(int width, int height) {
            rooms = new Room[width, height];
            GenerateMap();
        }

        private void GenerateMap() {

        }

        private void AddRoom(int x, int y, Room parent) {

        }

    }
}
