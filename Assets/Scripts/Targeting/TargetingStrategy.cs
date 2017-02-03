using UnityEngine;
using System.Collections.Generic;

public interface TargetingStrategy {
    List<Vector2> GetTargets(Vector2 center);
}

