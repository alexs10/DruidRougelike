using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class SimpleGraph<T> : Graph<T> {

    public SimpleGraph(WeightStrategy<T> weightStrategy, List<Node<T>> nodes, List<Edge<T>> edges) : base(weightStrategy, nodes, edges) { }

    protected override void InitNodesAndEdges(List<Node<T>> nodes, List<Edge<T>> edges, out List<Node<T>> outNodes, out List<Edge<T>> outEdges) {
        outNodes = nodes;
        outEdges = edges;
    }
}