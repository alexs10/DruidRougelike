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
            if (aLeft <= bRight && bLeft <= aRight) {
                Debug.Log("Hallway intersection x");
                // find the x overlap
                float intersect = aLeft;
                while (intersect < bLeft && intersect > bRight) {
                    intersect++;
                }

                //fin
                Debug.Log("Creating hallway at x=" + intersect + "from " + (aTop > bTop ? aBottom : bBottom) + " to " + (aTop < bTop ? bTop : aTop));
                // create hallway
                bm.BuildVerticleHallway(intersect, (aTop < bTop ? bTop : aTop), (aTop > bTop ? aBottom : bBottom));

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
}
