using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Edge<T> {
    public Node<T> node1;
    public Node<T> node2;
    public float weight;

    public Edge(Node<T> node1, Node<T> node2, float weight) {
        this.node1 = node1;
        this.node2 = node2;
        this.weight = weight;

        node1.AddAdjacent(node2);
        node2.AddAdjacent(node1);
    }

    public Edge(Node<T> node1, Node<T> node2) : this(node1, node2, 1f) { }

}

