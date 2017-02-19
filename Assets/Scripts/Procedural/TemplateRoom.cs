using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateRoom : MonoBehaviour, Cartesian {

    BoxCollider2D box;
    // Use this for initialization
    void Start() {
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        transform.position = new Vector2(Mathf.Floor(transform.position.x), Mathf.Floor(transform.position.y));
    }

    public int GetArea() {
        return (int)box.size.x * (int)box.size.y;
    }

    public void GetBounds(out float left, out float right, out float top, out float bottom) {
        Debug.Log("Position: " + transform.position +  " Box: " + box.size);
        left    = Mathf.Floor(transform.position.x);
        right = Mathf.Floor(transform.position.x + box.size.x);
        top = Mathf.Floor(transform.position.y + box.size.y) ;
        bottom = Mathf.Floor(transform.position.y);
    }

    public Vector2 GetCoordinates() {
        return new Vector2(
            transform.position.x + box.size.x / 2,
            transform.position.y + box.size.y / 2);
    }

}
