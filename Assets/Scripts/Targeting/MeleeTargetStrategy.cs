using System.Collections.Generic;
using UnityEngine;
public class MeleeTargetStrategy: TargetingStrategy {
	public List<Vector2> GetTargets(Vector2 center) {
		List<Vector2> validTargets = new List<Vector2>();
		for (int i = (int)center.x - 1; i < (int)center.x + 2; i++) {
			for (int j = (int)center.y - 1; j < (int)center.y + 2; j++) {
				validTargets.Add(new Vector2(i, j));
			}
		}
		return validTargets;
	}
}


