using UnityEngine;
using System.Collections.Generic;



class SingleTargetStrategy: TargetingStrategy {
    public List<Vector2> GetTargets(Vector2 center) {
        List<Vector2> output = new List<Vector2>();
        output.Add(center);
        return output;
    }
}

