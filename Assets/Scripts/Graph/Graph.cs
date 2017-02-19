using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public abstract class Graph<T> {
    private WeightStrategy<T> weightStrategy;
    public List<Node<T>> nodes;
    public List<Edge<T>> edges;
    public Graph(WeightStrategy<T> weightStrategy, List<Node<T>> nodes, List<Edge<T>> edges) {
        InitNodesAndEdges(nodes, edges, out this.nodes, out this.edges);
        this.weightStrategy = weightStrategy;
    }

    protected abstract void InitNodesAndEdges(List<Node<T>> nodes, List<Edge<T>> edges, out List<Node<T>> outNodes, out List<Edge<T>> outEdges);

    public float GetWeight(Node<T> a, Node<T> b) {
        return weightStrategy.CalcuateWeight(a, b);
    }

    public Graph<T> MinimumSpanningTree() {

        //First set up the tably thing to hold the nodes and wieghts
        Dictionary<Node<T>, NodeWeightElement> dict = new Dictionary<Node<T>, NodeWeightElement>();
        foreach (Node<T> node in nodes) {
            dict.Add(node, new NodeWeightElement());
        }

        List<Node<T>> mstNodes = new List<Node<T>>();
        List<Edge<T>> mstEdges = new List<Edge<T>>();

        //Add our seed to deal with edge cases
        mstNodes.Add(nodes[0]);
        dict.Remove(nodes[0]);

        Node<T> relaxer = nodes[0];

        while (dict.Count > 0) {
            RelaxEdges(dict, relaxer);
            relaxer = ExtractMin(mstNodes, mstEdges, dict);
        }

        return new SimpleGraph<T>(weightStrategy, mstNodes, mstEdges);

    }

    private void RelaxEdges(Dictionary<Node<T>, NodeWeightElement> dict, Node<T> relaxer) {
        foreach (Node<T> adj in relaxer.GetAdjacent()) {
			if (dict.ContainsKey(adj) && dict[adj].weight > GetWeight(relaxer, adj)) {
                dict[adj] = new NodeWeightElement(GetWeight(relaxer, adj), relaxer);
            }
        }
    }

    private Node<T> ExtractMin(List<Node<T>> targetNodes, List<Edge<T>> targetEdges, Dictionary<Node<T>, NodeWeightElement> freeNodes) {
        float min = Int32.MaxValue;
        Node<T> fromNode = null;
        Node<T> minNode = null;
        foreach (KeyValuePair<Node<T>, NodeWeightElement> keyvalue in freeNodes) {
            if (keyvalue.Value.weight < min) {
                minNode = keyvalue.Key;
                fromNode = keyvalue.Value.previous;
                min = keyvalue.Value.weight;
            }
        }
        //Add the new node to the graph
        targetEdges.Add(new Edge<T>(fromNode, minNode, GetWeight(fromNode, minNode)));
        targetNodes.Add(minNode);

        //remove node from freeNodes
        freeNodes.Remove(minNode);

        return minNode;
    }

    private class NodeWeightElement {
        public float weight;
        public Node<T> previous;

        public NodeWeightElement() {
            this.weight = Int32.MaxValue;
            this.previous = null;
        }

        public NodeWeightElement(float weight, Node<T> previous) {
            this.weight = weight;
            this.previous = previous;
        }
    }
    

}

