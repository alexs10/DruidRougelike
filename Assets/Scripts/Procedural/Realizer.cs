using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Realizer : MonoBehaviour {
    private TileGenerationVisitor creationVisitor;
	public void Start() {
        creationVisitor = GetComponent<TileGenerationVisitor>();
	}


	public void Realize(Graph<TemplateRoom> rooms) {
        foreach (Edge<TemplateRoom> edge in rooms.edges) {
                TemplateHallway hallway = new TemplateHallway(edge.node1.GetSubject(), edge.node2.GetSubject());
                hallway.Accept(creationVisitor);
            }

        foreach (Node<TemplateRoom> node in rooms.nodes) {
            TemplateRoom room = node.GetSubject();
            room.Accept(creationVisitor);

            creationVisitor.FillWithWalls();
        }
	}

   
}
