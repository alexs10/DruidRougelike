using UnityEngine;
using System.Collections.Generic;



public class TileGenerationVisitor: MonoBehaviour, ITemplateVisitor {

    private class Count {
        public int min;
        public int max;

        public Count(int min, int max) {
            this.min = min;
            this.max = max;
        }
    }

    public GameObject[] floorTiles;                                 //Array of floor prefabs.                                
    public GameObject[] outerWallTiles;                             //Array of outer tile prefabs.

    private List<Vector2> unusedPositions = new List<Vector2>();
    private float xLeft = 0, xRight = 0, yBottom = 0, yTop = 0;

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

        for (int i = (int)left; i < (int)right; i++) {
            for (int j = (int)bottom; j < (int)top; j++) {

                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                Vector2 location = new Vector2(i, j);
                GameObject instance = Instantiate(toInstantiate, location, Quaternion.identity) as GameObject;
                instance.transform.SetParent(mainRoom);
                UsePosition(location);
            }
        }
    }


    public void FillWithWalls() {
        Debug.Log("Size: " + unusedPositions.Count + "Sqrt(Size): " + Mathf.Sqrt(unusedPositions.Count));
        Debug.Log("left: " + xLeft + " right: " + xRight + " bottom: " + yBottom + " top: " + yTop);
        Transform walls = new GameObject("Walls").transform;
        foreach (Vector2 unusedPosition in unusedPositions) {
            GameObject toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
            GameObject instance = Instantiate(toInstantiate, unusedPosition, Quaternion.identity) as GameObject;
            instance.transform.SetParent(walls);
        }
    }

    void UsePosition(Vector2 position) {
        //expand horizontal bound 
        if (position.x < xLeft) {
            Expand(position.x, xLeft - 1, yBottom, yTop);
            xLeft = position.x;
        }
        else if (position.x > xRight) {
            Expand(xRight + 1, position.x, yBottom, yTop);
            xRight = position.x;
        }

        //expand vertically
        if (position.y < yBottom) {
            Expand(xLeft, xRight, position.y, yBottom - 1);
            yBottom = position.y;
        }
        else if (position.x > yTop) {
            Expand(xLeft, xRight, yTop + 1, position.y);
            yTop = position.y;
        }

        unusedPositions.Remove(position);
    }

    void Expand(float xLeft, float xRight, float yBottom, float yTop) {
        for (float i = xLeft; i <= xRight; i++) {
            for (float j = yBottom; j <= yTop; j++) {
                unusedPositions.Add(new Vector2(i, j));
            }
        }
    }
}

