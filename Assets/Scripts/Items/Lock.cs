using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour {
	public Color color = Color.magenta;
	private SpriteRenderer spriteRenderer;
	private BoxCollider2D boxCollider;
	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		boxCollider = GetComponent<BoxCollider2D> ();
		spriteRenderer.color = color;
	}

	public void Unlock(Color key) {
		Debug.Log ("KeyColor: " + key + " LockColor: " + color);
		if (key == this.color) {
			Debug.Log ("Wooo unlocked");
			boxCollider.enabled = false;
			spriteRenderer.color = Color.gray;
		}
	}
}
