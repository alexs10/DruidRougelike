using UnityEngine;
using System.Collections.Generic;
public class KeyActionCommand: TargetedActionCommand {
	private Color color;
	private int keyLayer;
	private TargetingStrategy meleeTargetingStrategy;
	public KeyActionCommand (Color color,
		TargetingController targetingController,
		PlayerController playerController,
		Player player)
		: base(targetingController, playerController, player) {

		this.color = color;
		keyLayer = LayerMask.GetMask ("BlockingLayer");
		meleeTargetingStrategy = new MeleeTargetStrategy ();
	}

	protected override void DoExecute(Transform target) {
		Vector2 rayPos = target.position;
		RaycastHit2D hit = Physics2D.Raycast (rayPos, Vector2.zero, 0f, keyLayer);
		if (hit && hit.collider.GetComponent<Lock> () != null) {
			hit.collider.GetComponent<Lock> ().Unlock (color);
			Debug.Log ("Just tried to unlock");
		}

        GameManager.instance.playersTurn = false;
	}

	protected override List<Vector2> GetValidTargets() {
		return meleeTargetingStrategy.GetTargets (player.transform.position);
	}
}


