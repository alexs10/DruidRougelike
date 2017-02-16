using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Node<T> {
    private List<Node<T>> adjacent;
    private T subject;

    public Node(T subject) {
        adjacent = new List<Node<T>>();
        this.subject = subject;
    }

    public T GetSubject() {
        return subject;
    }

    public List<Node<T>> GetAdjacent() {
        return adjacent;
    }

    public void AddAdjacent(Node<T> adj) {
        adjacent.Add(adj);
    }

    public void RemoveAdjacent(Node<T> adj) {
        adjacent.Remove(adj);
    }
}

