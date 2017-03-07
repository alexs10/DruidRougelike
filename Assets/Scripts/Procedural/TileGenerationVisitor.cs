using UnityEngine;
using System.Collections.Generic;



public class TileGenerationVisitor: MonoBehaviour, ITemplateVisitor {

    public GameObject[] floorTiles;                                 //Array of floor prefabs.                                
    public GameObject[] outerWallTiles;                             //Array of outer tile prefabs.
    public GameObject player;

    private List<Position> usedPositions = new List<Position>();

    private int xLeft = 0, xRight = 0, yBottom = 0, yTop = 0;
    private Position currentPosition = new Position(0, 0);
     
    public void Visit(TemplateHallway hallway) {
        Transform hallWay = new GameObject("Hallway").transform;

        foreach (Vector2 location in hallway.GetPositions()) {
            GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
            GameObject instance = Instantiate(toInstantiate, location, Quaternion.identity) as GameObject;
            instance.transform.SetParent(hallWay);

            UsePosition(location);
        }
    }

    public void Visit(TemplateRoom room) {
        Transform mainRoom = new GameObject("MainRoom").transform;

        float left, right, bottom, top;
        room.GetBounds(out left, out right, out top, out bottom);

        currentPosition = new Position(left, bottom);

        for (int i = (int)left; i < (int)right; i++) {
            for (int j = (int)bottom; j < (int)top; j++) {

                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                Vector2 location = new Vector2(i, j);
                GameObject instance = Instantiate(toInstantiate, location, Quaternion.identity) as GameObject;
                instance.transform.SetParent(mainRoom);
                UsePosition(location);
            }
        }

        //this is where all the in room elements get instantiated
        foreach (ITemplateElement roomElement in room.roomElements) {
            roomElement.Accept(this);
        }
    }

    public void Visit(PlayerSpawn playerSpawn) {
        GameObject.Find("Player").transform.position = new Vector2(currentPosition.x, currentPosition.y);
    }


    public void FillWithWalls() {
        Debug.Log("Size: " + usedPositions.Count + "Sqrt(Size): " + Mathf.Sqrt(usedPositions.Count));
        Debug.Log("left: " + xLeft + " right: " + xRight + " bottom: " + yBottom + " top: " + yTop);
        Transform walls = new GameObject("Walls").transform;

        //First create a large list of all locations
        List<Position> wallPositions = new List<Position>();
        for ( int i = xLeft; i <= xRight; i++) {
            for (int j = yBottom; j <= yTop; j++)
            {
                wallPositions.Add(new Position(i, j));
            }
        }
        //remove all the positions we have floors on
        foreach (Position usedPosition in usedPositions) {
            wallPositions.Remove(usedPosition);
        }

        //gen walls
        foreach (Position wallPosition in wallPositions) {
            GameObject toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
            GameObject instance = Instantiate(toInstantiate, wallPosition.Vector2(), Quaternion.identity) as GameObject;
            instance.transform.SetParent(walls);
        }
    }

    void UsePosition(Vector2 vector2) {
        UsePosition(new Position(vector2));
    }

    void UsePosition(Position position) {     
        //expand horizontal bound 
        if (position.x < xLeft) {
            xLeft = position.x;
        }
        if (position.x > xRight) {
            xRight = position.x;
        }

        //expand vertically
        if (position.y < yBottom) {
            yBottom = position.y;
        }
        if (position.y > yTop) {
            yTop = position.y;
        }

        usedPositions.Add(position);
    }

    private class Count {
        public int min;
        public int max;

        public Count(int min, int max) {
            this.min = min;
            this.max = max;
        }
    }

    private class Position {
        public int x, y;
        public Position(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public Position(float x, float y) {
            this.x = (int)x;
            this.y = (int)y;
        }

        public Position(Vector2 target) {
            this.x = (int)target.x;
            this.y = (int)target.y;
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Position p = (Position)obj;
            return x == p.x && y == p.y;
        }

        public override int GetHashCode() {
            return x ^ y;
        }

        public Vector2 Vector2() {
            return new Vector2(x, y);
        }
    }
}

