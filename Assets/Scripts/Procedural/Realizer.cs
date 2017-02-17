using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Realizer : MonoBehaviour {
	private BoardManager bm;

	public void Start() {
		bm = GetComponent<BoardManager> ();
	}


	public void Realize(Graph<TemplateRoom> rooms) {
		foreach (Node<TemplateRoom> node in rooms.nodes) {
			TemplateRoom room = node.GetSubject ();
			bm.BuildRoom (room.transform.position.x, 
				room.transform.position.y, 
				room.GetComponent<BoxCollider2D> ().size.x, 
				room.GetComponent<BoxCollider2D> ().size.y);
		}
	}
}
