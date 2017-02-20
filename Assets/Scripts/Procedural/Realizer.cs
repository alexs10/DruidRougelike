using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Realizer : MonoBehaviour {
	private BoardManager bm;

	public void Start() {
		bm = GetComponent<BoardManager> ();
	}


	public void Realize(Graph<TemplateRoom> rooms) {
        foreach (Edge<TemplateRoom> edge in rooms.edges) {
            Debug.Log("Edge");
            TemplateRoom RoomA = edge.node1.GetSubject();
            TemplateRoom RoomB = edge.node2.GetSubject();
            float aLeft, aRight, aTop, aBottom, bLeft, bRight, bTop, bBottom;
            RoomA.GetBounds(out aLeft, out aRight, out aTop, out aBottom);
            RoomB.GetBounds(out bLeft, out bRight, out bTop, out bBottom);

            //check if they overlap on x
            if (IsOverlaping(aLeft, aRight, bLeft, bRight)) {
                Debug.Log("Hallway intersection x");
                // find the x overlap
                float intersect = FindIntersection(aLeft, aRight, bLeft, bRight);
                //Debug.Log("Creating hallway at x=" + intersect + "from " + ((aTop < bTop ? bBottom : aBottom) - 1) + " to " + (aTop > bTop ? bTop : aTop));
                // create hallway -1 is so we dont overlap with the room
                bm.BuildVerticleHallway(intersect, (aTop > bTop ? bTop : aTop), (aTop < bTop ? bBottom : aBottom) - 1);

            } else if (IsOverlaping(aBottom, aTop, bBottom, bTop)) {
                float intersect = FindIntersection(aBottom, aTop, bBottom, bTop);
                bm.BuildHorizontalHallway(intersect, (aRight > bRight ? bRight : aRight), (aRight < bRight ? bLeft : aLeft) - 1);

            } else {
                Debug.Log("Creating compound Hallway");
                float intersectX = FindIntersection(aLeft, aRight, aLeft, aRight);
                float intersectY = FindIntersection(bBottom, bTop, bBottom, bTop);

                Debug.Log("Interesct x: " + intersectX + " Intersect Y: " + intersectY);
                float leftEnd = (bRight < intersectX ? bRight : intersectX + 1);
                float rightEnd = (bLeft > intersectX ? bLeft - 1 : intersectX - 1);
                float bottomEnd = (aTop < intersectY ? aTop: intersectY);
                float topEnd = (aBottom > intersectY ? aBottom + 1: intersectY);

                Debug.Log("left: " + leftEnd + " right: " + rightEnd + " bottom: " + bottomEnd + " top: " + topEnd);
                bm.BuildHorizontalHallway(intersectY, leftEnd, rightEnd);
                bm.BuildVerticleHallway(intersectX, bottomEnd, topEnd);

            }
        } 
		foreach (Node<TemplateRoom> node in rooms.nodes) {
			TemplateRoom room = node.GetSubject ();
			bm.BuildRoom (room.transform.position.x, 
				room.transform.position.y, 
				room.GetComponent<BoxCollider2D> ().size.x, 
				room.GetComponent<BoxCollider2D> ().size.y);
		}
	}

    private bool IsOverlaping(float aLow, float aHigh, float bLow, float bHigh) {
        return aLow < bHigh && bLow < aHigh; 
    }

    private float FindIntersection(float aLow, float aHigh, float bLow, float bHigh) {
        float intersectLow = aLow < bLow ? bLow : aLow;
        float intersectHigh = aHigh > bHigh ? bHigh : aHigh;
        return Mathf.Floor(Random.Range(intersectLow, intersectHigh));
    }
}
