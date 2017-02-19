using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class ConnectedGraph<T>: Graph<T> {

    public ConnectedGraph(WeightStrategy<T> weightStrategy, List<Node<T>> nodes) : base(weightStrategy, nodes, new List<Edge<T>>()) { }

    protected override void InitNodesAndEdges(List<Node<T>> nodes, List<Edge<T>> edges, out List<Node<T>> outNodes, out List<Edge<T>> outEdges) {

        //deep copy nodes into other nodes
        List<Node<T>> otherNodes = new List<Node<T>>();
        foreach(Node<T> node in nodes) {
            otherNodes.Add(node);
        }
        outEdges = new List<Edge<T>>();
        outNodes = nodes;
        foreach (Node<T> node in nodes) {
            otherNodes.Remove(node);
            foreach (Node<T> other in otherNodes) {
                outEdges.Add(new Edge<T>(node, other));


            }
        }
        
    }
}

