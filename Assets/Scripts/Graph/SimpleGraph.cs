using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class SimpleGraph<T> : Graph<T> {

    public SimpleGraph(WeightStrategy<T> weightStrategy, List<Node<T>> nodes) : base(weightStrategy, nodes) { }

    protected override List<Node<T>> InitNodes(List<Node<T>> nodes) {
        return nodes;
    }
}