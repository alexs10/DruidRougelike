using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Realizer : MonoBehaviour {
    private TileGenerationVisitor creationVisitor;
	public void Start() {
        creationVisitor = GetComponent<TileGenerationVisitor>();
	}


	public void Realize(Graph<TemplateRoom> rooms, List<TemplateHallway> hallways) {
        foreach (TemplateHallway hallway in hallways) {
            hallway.Accept(creationVisitor);
        }

        foreach (Node<TemplateRoom> node in rooms.nodes) {
            TemplateRoom room = node.GetSubject();
            room.Accept(creationVisitor);

            
        }
        creationVisitor.FillWithWalls();
    }

   
}
