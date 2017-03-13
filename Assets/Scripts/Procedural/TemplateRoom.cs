using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateRoom : MonoBehaviour, Cartesian {

    BoxCollider2D box;

    public List<ITemplateElement> roomElements;

    public Dictionary<TemplateRoom, Position> doorLocations;

    private List<Position> gridPositions = new List<Position>();

    void InitGridPositions(int columns, int rows) {
        gridPositions.Clear();

        for (int i = 1; i < columns - 1; i++) {
            for (int j = 1; j < rows - 1; j++) {
                gridPositions.Add(new Position(i, j));
            }
        }
    }

    public void AddDoor(Position doorLocation, TemplateRoom leadsTo) {
        doorLocations.Add(leadsTo, doorLocation);
    }


    // Use this for initialization
    void Start() {
        roomElements = new List<ITemplateElement>();
        doorLocations = new Dictionary<TemplateRoom, Position>();
        box = GetComponent<BoxCollider2D>();
        InitGridPositions((int)box.size.x, ((int)box.size.y));
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

    public Position RandomUnusedPosition() {
        int index = Random.Range(0, gridPositions.Count);
        Position returnIndex = gridPositions[index];
        gridPositions.RemoveAt(index);
        return returnIndex;
    }




}
