using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureDrawer : MonoBehaviour {
    public Texture2D texture;


    private BoxCollider2D box;
	// Use this for initialization
	void Start () {
        box = GetComponent<BoxCollider2D>();

        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.red);
        texture.Apply();
        

	}

    private void OnGUI() {
        GUI.DrawTexture(new Rect(transform.position*32, box.size*32), texture);
    }

}
