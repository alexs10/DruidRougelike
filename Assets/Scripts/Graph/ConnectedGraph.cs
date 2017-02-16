using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class ConnectedGraph<T>: Graph<T> {

    public ConnectedGraph(WeightStrategy<T> weightStrategy, List<Node<T>> nodes) : base(weightStrategy, nodes) { }

    protected override List<Node<T>> InitNodes(List<Node<T>> nodes) {
        foreach (Node<T> node in nodes) {
            List<Node<T>> others = new List<Node<T>>(nodes);
            others.Remove(node);
            foreach (Node<T> other in others) {
                node.AddAdjacent(other);
            }
        }
        return nodes;
    }
}

