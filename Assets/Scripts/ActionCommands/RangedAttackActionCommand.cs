
using UnityEngine;
using System.Collections.Generic;

class RangedAttackActionCommand: TargetedActionCommand {
	private int damage;
	private int damagingLayer;
	private TargetingStrategy rangedTargetingStrategy;

	public RangedAttackActionCommand(int damage, int range, TargetingController targetController, PlayerController playerController, Player player) 
		: base(targetController, playerController, player) {

		this.damage = damage;
		this.rangedTargetingStrategy = new RangedTargetStrategy (range);
		damagingLayer = LayerMask.GetMask("BlockingLayer");
	}

	protected override void DoExecute(Transform target) {
		Vector2 rayPos = target.position;
		RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f, damagingLayer);

		if (hit && hit.collider.GetComponent<Enemy>() != null) {
			hit.collider.GetComponent<Enemy>().TakeDamage(damage);
			Debug.Log("Ranged ATTACK: " + damage);
		}

		GameManager.instance.playersTurn = false;
	}

	public override Sprite GetSprite(){
		return Resources.Load<Sprite> ("RangeInv");
	}

	public override string GetDescription ()
	{
		return "Shoot your foes with some magic";
	}

	protected override List<Vector2> GetValidTargets() {
		return rangedTargetingStrategy.GetTargets (player.transform.position);
	}

	public override string GetName() {
		return "Wrath";
	}

}

