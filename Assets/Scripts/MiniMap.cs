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

    private int width = 150;
    private int height = 100;

    private int xOffset;
    private int yOffset;

    private int roomWidth;
    private int roomHeight;
    private int seperation;

    private Map map;

    void Start() {
        map = GameManager.instance.map;

        xOffset = Screen.width - width;
        yOffset = Screen.height - 50;

        seperation = 2;
        roomWidth = (width - (Map.MapConfig.WIDTH - 1) * seperation) / Map.MapConfig.WIDTH;
        roomHeight = (height - (Map.MapConfig.HEIGHT - 1) * seperation) / Map.MapConfig.HEIGHT;
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
            currentRoomStyle.normal.background = MakeTex(30, 30, currentRoom);
        }
        if (discoveredRoomStyle == null) {
            discoveredRoomStyle = new GUIStyle(GUI.skin.box);
            discoveredRoomStyle.normal.background = MakeTex(30, 30, discoveredRoom);
        }
        if (undiscoveredRoomStyle == null) {
            undiscoveredRoomStyle = new GUIStyle(GUI.skin.box);
            undiscoveredRoomStyle.normal.background = MakeTex(30, 30, undiscoveredRoom);
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
                    if (room == GameManager.instance.currentRoom) {
                        DrawRoom(i, j, currentRoomStyle);
                    } else if (room.isRevealed == true ) {
                        DrawRoom(i, j, discoveredRoomStyle);
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
}
