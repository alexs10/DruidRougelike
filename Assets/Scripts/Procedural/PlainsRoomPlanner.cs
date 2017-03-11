using UnityEngine;
using System.Collections.Generic;

public class PlainsRoomPlanner : IRoomPlanner {
    private List<Position> gridPositions = new List<Position>();

    void InitGridPositions(int columns, int rows) {
        gridPositions.Clear();

        for (int i = 1; i < columns - 1; i++) {
            for (int j = 1; j < rows - 1; j++) {
                gridPositions.Add(new Position(i, j));
            }
        }
    }


    public void Plan(TemplateRoom room, int difficulty) {
        InitGridPositions(room.GetWidth(), room.GetHeight());
        LayoutWalls(room, 5, 9);
    }

    void LayoutWalls(TemplateRoom room, int min, int max) {
        int objectCount = Random.Range(min, max);

        for (int i = 0; i < objectCount; i++) {
            Position wallPos = RandomUnusedPosition();
            room.AddRoomElement(new TemplateWall(wallPos.x, wallPos.y));
        }
    }

    Position RandomUnusedPosition() {
        int index = Random.Range(0, gridPositions.Count);
        Position returnIndex = gridPositions[index];
        gridPositions.RemoveAt(index);
        return returnIndex;
    }

    private class Position {
        public int x;
        public int y;

        public Position(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }

    private class Count {
        public int min;
        public int max;

        public Count(int min, int max) {
            this.min = min;
            this.max = max;
        }
    }
}

