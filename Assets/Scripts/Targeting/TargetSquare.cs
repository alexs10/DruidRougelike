using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSquare : MonoBehaviour {
    BoxCollider2D colider;
    SpriteRenderer spriteRender;

    private Color targetColor = new Color(1, 0, 0, 0.5f);
    private Color defualtColor = new Color(1, 1, 1, 0f);
    private Color nontargetableColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

	// Use this for initialization
	void Start () {
        colider = GetComponent<BoxCollider2D>();
        spriteRender = GetComponent <SpriteRenderer>();
        SetDefault();
	}
	
    public bool IsNontargetable() {
        return spriteRender.color == nontargetableColor;
    }

	public void SetTargeted() {
        spriteRender.color = targetColor;
    }

    public void SetDefault() {
        spriteRender.color = defualtColor;
    }

    public void SetNontargetable() {
        spriteRender.color = nontargetableColor;
    }

    
}
