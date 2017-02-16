using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface GraphNode {
    Dictionary<GraphNode, int> GetAdjacentNodes();
    void AddAdjacent(GraphNode node, int weight);
    void RemoveAdjacent(GraphNode node);

}

