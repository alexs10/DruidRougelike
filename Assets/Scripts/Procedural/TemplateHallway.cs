using System.Collections.Generic;
using UnityEngine;

public class TemplateHallway {
    private List<Vector2> hallwayPositions;

    public TemplateHallway(TemplateRoom roomA, TemplateRoom roomB) {
        hallwayPositions = new List<Vector2>();
        float aLeft, aRight, aTop, aBottom, bLeft, bRight, bTop, bBottom;

        roomA.GetBounds(out aLeft, out aRight, out aTop, out aBottom);
        roomB.GetBounds(out bLeft, out bRight, out bTop, out bBottom);

        //check if they overlap on x
        if (IsOverlaping(aLeft, aRight, bLeft, bRight)) {
            Debug.Log("Hallway intersection x");
            // find the x overlap
            float intersect = FindIntersection(aLeft, aRight, bLeft, bRight);
            //Debug.Log("Creating hallway at x=" + intersect + "from " + ((aTop < bTop ? bBottom : aBottom) - 1) + " to " + (aTop > bTop ? bTop : aTop));
            // create hallway -1 is so we dont overlap with the room
            BuildVerticleHallway(intersect, (aTop > bTop ? bTop : aTop), (aTop < bTop ? bBottom : aBottom) - 1);

        }
        else if (IsOverlaping(aBottom, aTop, bBottom, bTop)) {
            float intersect = FindIntersection(aBottom, aTop, bBottom, bTop);
            BuildHorizontalHallway(intersect, (aRight > bRight ? bRight : aRight), (aRight < bRight ? bLeft : aLeft) - 1);

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

        }
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

