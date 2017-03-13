using UnityEngine;
using System.Collections.Generic;

public class PlainsRoomPlanner : IRoomPlanner {


    public void Plan(TemplateRoom room, int difficulty) {
        LayoutWalls(room, 5, 9);
    }

    void LayoutWalls(TemplateRoom room, int min, int max) {
        int objectCount = Random.Range(min, max);

        for (int i = 0; i < objectCount; i++) {
            Position wallPos = room.RandomUnusedPosition();
            room.AddRoomElement(new TemplateWall(wallPos.x, wallPos.y));
        }
    }
}

