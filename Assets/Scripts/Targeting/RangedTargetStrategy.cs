using System.Collections.Generic;
using UnityEngine;
public class RangedTargetStrategy: TargetingStrategy {

	private int range;

	public RangedTargetStrategy(int range) {
		this.range = range;
	}

	public List<Vector2> GetTargets(Vector2 center) {
		List<Vector2> validTargets = new List<Vector2>();
		for (int i = (int)center.x - range; i <= (int)center.x + range; i++) {
			for (int j = (int)center.y - range; j <= (int)center.y + range; j++) {
				Debug.Log ("i: " + i + " j: " + j +  " Range: " + range);
				if (Mathf.Abs(i - center.x) + Mathf.Abs(j - center.y) <= range + 0.1) {
					validTargets.Add(new Vector2(i, j));
				}
			}
		}

		return validTargets;
	}
}


