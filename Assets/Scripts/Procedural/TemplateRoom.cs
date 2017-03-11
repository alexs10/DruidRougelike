using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateRoom : MonoBehaviour, Cartesian {

    BoxCollider2D box;

    public List<ITemplateElement> roomElements;

    public Dictionary<TemplateRoom, Position> doorLocations;

    // Use this for initialization
    void Start() {
        roomElements = new List<ITemplateElement>();
        doorLocations = new Dictionary<TemplateRoom, Position>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        transform.position = new Vector2(Mathf.Floor(transform.position.x), Mathf.Floor(transform.position.y));
    }

    public int GetArea() {
        return (int)box.size.x * (int)box.size.y;
    }

    public int GetWidth() {
        return (int)box.size.x;
    }

    public int GetHeight() {
        return (int)box.size.y;
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

    public void Accept(ITemplateVisitor visitor) {
        visitor.Visit(this);
    }

    public void AddRoomElement(ITemplateElement roomElement) {
        roomElements.Add(roomElement);
    }

    public class Position {
        public int x, y;
        public Position(int x, int y) {
            this.x = x;
            this.y = y;
        }

    }

}
