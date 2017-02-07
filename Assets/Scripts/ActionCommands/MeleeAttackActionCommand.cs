
using UnityEngine;
using System.Collections.Generic;

class MeleeAttackActionCommand: TargetedActionCommand {
    private int damage;
    private int damagingLayer;
	private TargetingStrategy meleeTargetingStrategy;

    public MeleeAttackActionCommand(int damage, TargetingController targetController, PlayerController playerController, Player player) 
			: base(targetController, playerController, player) {

		this.damage = damage;
		this.meleeTargetingStrategy = new MeleeTargetStrategy ();
        damagingLayer = LayerMask.GetMask("BlockingLayer");
    }

	protected override void DoExecute(Transform target) {
		Vector2 rayPos = target.position;
		RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f, damagingLayer);

		if (hit && hit.collider.GetComponent<Enemy>() != null) {
			hit.collider.GetComponent<Enemy>().TakeDamage(2);
			Debug.Log("MELEE ATTACK: " + damage);
		}

		player.EndTurn(2);
	}

	protected override List<Vector2> GetValidTargets() {
		return meleeTargetingStrategy.GetTargets (player.transform.position);
    }

}

