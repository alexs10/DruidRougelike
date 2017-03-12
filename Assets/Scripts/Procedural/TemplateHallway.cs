using System.Collections.Generic;
using UnityEngine;

public class TemplateHallway {
    private List<Vector2> hallwayPositions;

    public TemplateHallway(TemplateRoom roomA, TemplateRoom roomB) {
        hallwayPositions = new List<Vector2>();
        float aLeft, aRight, aTop, aBottom, bLeft, bRight, bTop, bBottom;

        roomA.GetBounds(out aLeft, out aRight, out aTop, out aBottom);
        roomB.GetBounds(out bLeft, out bRight, out bTop, out bBottom);

        Position doorA, doorB;

        //check if they overlap on x
        if (IsOverlaping(aLeft, aRight, bLeft, bRight)) {
            Debug.Log("Hallway intersection x");
            // find the x overlap
            float intersect = FindIntersection(aLeft, aRight, bLeft, bRight);
            //Debug.Log("Creating hallway at x=" + intersect + "from " + ((aTop < bTop ? bBottom : aBottom) - 1) + " to " + (aTop > bTop ? bTop : aTop));
            // create hallway -1 is so we dont overlap with the room
            float bottom = (aTop > bTop ? bTop : aTop);
            float top = (aTop < bTop ? bBottom : aBottom) - 1;

            BuildVerticleHallway(intersect, bottom, top);
            
            if (aTop > bTop) {
                doorA = new Position(intersect, top + 1);
                doorB = new Position(intersect, bottom - 1);
            } else {
                doorB = new Position(intersect, top + 1);
                doorA = new Position(intersect, bottom - 1);
            }

        }
        else if (IsOverlaping(aBottom, aTop, bBottom, bTop)) {
            float intersect = FindIntersection(aBottom, aTop, bBottom, bTop);
            float left = (aRight > bRight ? bRight : aRight);
            float right = (aRight < bRight ? bLeft : aLeft) - 1;
            BuildHorizontalHallway(intersect, left, right);

            if (aRight > bRight) {
                doorA = new Position(right + 1, intersect);
                doorB = new Position(left - 1, intersect);
            } else {
                doorB = new Position(right + 1, intersect);
                doorA = new Position(left - 1, intersect);
            }

        }
        else {
            Debug.Log("Creating compound Hallway");
            float intersectX = FindIntersection(aLeft, aRight, aLeft, aRight);
            float intersectY = FindIntersection(bBottom, bTop, bBottom, bTop);

            Debug.Log("Interesct x: " + intersectX + " Intersect Y: " + intersectY);
            float leftEnd = (bRight < intersectX ? bRight : intersectX + 1);
            float rightEnd = (bLeft > intersectX ? bLeft - 1 : intersectX - 1);
            float bottomEnd = (aTop < intersectY ? aTop : intersectY);
            float topEnd = (aBottom > intersectY ? aBottom + 1 : intersectY);

            Debug.Log("left: " + leftEnd + " right: " + rightEnd + " bottom: " + bottomEnd + " top: " + topEnd);
            BuildHorizontalHallway(intersectY, leftEnd, rightEnd);
            BuildVerticleHallway(intersectX, bottomEnd, topEnd);

            if (aTop > bTop) {
                doorA = new Position(intersectX, topEnd + 1);
            } else {
                doorA = new Position(intersectX, bottomEnd - 1);
            }

            if (bRight > aRight) {
                doorB = new Position(rightEnd + 1, intersectY);
            } else {
                doorB = new Position(leftEnd - 1, intersectY);
            }

        }

        roomA.AddDoor(doorA, roomB);
        roomB.AddDoor(doorB, roomA);
    }

    public List<Vector2> GetPositions() {
        return hallwayPositions;
    }

    private bool IsOverlaping(float aLow, float aHigh, float bLow, float bHigh) {
        return aLow < bHigh && bLow < aHigh;
    }

    private float FindIntersection(float aLow, float aHigh, float bLow, float bHigh) {
        float intersectLow = aLow < bLow ? bLow : aLow;
        float intersectHigh = aHigh > bHigh ? bHigh : aHigh;
        return Mathf.Floor(Random.Range(intersectLow, intersectHigh));
    }

    private void BuildVerticleHallway(float x, float yBottom, float yTop) {
        BuildHallway(x, yBottom, yTop, true);

    }

    private void BuildHorizontalHallway(float y, float xLeft, float xRight) {
        BuildHallway(y, xLeft, xRight, false);
    }

    private void BuildHallway(float constant, float min, float max, bool verticle) {
        for (float j = min; j <= max; j++) { 
            hallwayPositions.Add(verticle ? new Vector2(constant, j) : new Vector2(j, constant));
        }
    }

    public void Accept(ITemplateVisitor visitor) {
        visitor.Visit(this);
    }
}

