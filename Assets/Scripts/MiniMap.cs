using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Map;

public class MiniMap : MonoBehaviour {

    public Color currentRoom = Color.green;
    public Color discoveredRoom = Color.gray;
    public Color undiscoveredRoom = Color.clear;

    //public Texture2D roomTexture;
    private GUIStyle currentRoomStyle = null;
    private GUIStyle discoveredRoomStyle = null;
    private GUIStyle undiscoveredRoomStyle = null;

    private int width = 200;
    private int height = 150;

    private int xOffset;
    private int yOffset;

    private float roomWidth;
    private float roomHeight;
    private float seperation;
    private float hallwayWidth;
    private float hallwayHeight;

    private Map map;

    void Start() {
        map = GameManager.instance.map;

        xOffset = Screen.width - width;
        yOffset = Screen.height - 50;

        seperation = 2;
        roomWidth = (width - (Map.MapConfig.WIDTH - 1) * seperation) / Map.MapConfig.WIDTH;
        roomHeight = (height - (Map.MapConfig.HEIGHT - 1) * seperation) / Map.MapConfig.HEIGHT;
        hallwayWidth = roomWidth / 4f;
        hallwayHeight = roomHeight / 4f;
        Debug.Log("ROOM WIDTH: " + roomWidth);
    }

    private Texture2D MakeTex(int width, int height, Color col) {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i) {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
    void InitGUI() {
        if (currentRoomStyle == null) {
            currentRoomStyle = new GUIStyle(GUI.skin.box);
            currentRoomStyle.normal.background = MakeTex(1, 1, currentRoom);
        }
        if (discoveredRoomStyle == null) {
            discoveredRoomStyle = new GUIStyle(GUI.skin.box);
            discoveredRoomStyle.normal.background = MakeTex(1, 1, discoveredRoom);
        }
        if (undiscoveredRoomStyle == null) {
            undiscoveredRoomStyle = new GUIStyle(GUI.skin.box);
            undiscoveredRoomStyle.normal.background = MakeTex(1, 1, undiscoveredRoom);
        }
    }


    void OnGUI() {
        InitGUI();
        DrawMiniMap();
    }

    private void DrawMiniMap() {
        for (int i = 0; i < Map.MapConfig.WIDTH; i++) {
            for (int j = 0; j < Map.MapConfig.HEIGHT; j++) {
                Room room = map.rooms[i, j];

                if (room != null) {
					if (room == GameManager.instance.CurrentRoom()) {
                        DrawRoom(i, j, currentRoomStyle);
                        DrawHallways(room, discoveredRoomStyle);
                    } else if (room.isRevealed == true ) {
                        DrawRoom(i, j, discoveredRoomStyle);
                        DrawHallways(room, discoveredRoomStyle);
                    } else {
                        DrawRoom(i, j, undiscoveredRoomStyle);
                    }
                }
            }
        }
    }

    private void DrawRoom(int x, int y, GUIStyle color) {
        GUI.Box(new Rect(xOffset + x*roomWidth + (x-1)*seperation, 
            yOffset - y*roomHeight - (y-1)*seperation, roomWidth, roomHeight), "", color);
    }
    private void DrawHallways(Room room, GUIStyle color) {
        Position roomPos = new Position(room.x, room.y);
       
        foreach (Position pos in room.FindClosedAdjacencies()) {
            //DrawHallway(roomPos, pos, color);
        }
        
    }

    private void DrawHallway(Position pos1, Position pos2, GUIStyle color) {
        if (pos1.x < pos2.x) {
            GUI.Box(new Rect(50,
                50, roomWidth, roomHeight), GUIContent.none, color);
            GUI.Box(new Rect(100,
                50, 3f, 1), GUIContent.none, color);
        } else if (pos1.x < pos2.x) {
            //GUI.Box(new Rect(xOffset + (pos1.x + 1) * roomWidth + (pos1.x - 1) * seperation,
            //    yOffset - pos1.y * roomHeight - (pos1.y - 1) * seperation - roomHeight / 2 - 1, 2, 2), "", color);
        } else if (pos1.y > pos2.y) {
           // GUI.Box(new Rect(xOffset + pos1.x * roomWidth + (pos1.x - 1) * seperation + roomWidth/2 - 1,
           //     yOffset - (pos1.y - 1) * roomHeight - (pos1.y - 1) * seperation, 2, 2), "", color);
        } else {
           // GUI.Box(new Rect(xOffset + pos1.x * roomWidth + (pos1.x - 1) * seperation + roomWidth / 2 - 1,
           //     yOffset - (pos1.y) * roomHeight - (pos1.y - 2) * seperation, 2, 2), "", color);
        }
    }

}
