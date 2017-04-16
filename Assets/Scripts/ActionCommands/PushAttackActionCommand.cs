
using UnityEngine;
using System.Collections.Generic;

class PushAttackActionCommand: TargetedActionCommand {
	private int damage;
	private int distance;
	private int damagingLayer;
	private TargetingStrategy meleeTargetingStrategy;

	public PushAttackActionCommand(int damage, int distance, TargetingController targetController, PlayerController playerController, Player player) 
		: base(targetController, playerController, player) {

		this.damage = damage;
		this.distance = distance;
		this.meleeTargetingStrategy = new MeleeTargetStrategy ();
		damagingLayer = LayerMask.GetMask("BlockingLayer");
	}

	protected override void DoExecute(Transform target) {
		Vector2 rayPos = target.position;
		RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f, damagingLayer);

		if (hit && hit.collider.GetComponent<Enemy>() != null) {
			hit.collider.GetComponent<Enemy> ().TakeDamage (damage);

			for (int i = distance; i > 0; i--) {
				Vector2 pushDest = (hit.collider.transform.position - player.transform.position) * i + hit.collider.transform.position;
				if (hit.collider.GetComponent<Enemy> ().Push (pushDest)) {
					Debug.Log ("Push ATTACK: " + damage);
					break;
				
				}
			}
			
		}

		GameManager.instance.playersTurn = false;
	}

	public override Sprite GetSprite(){
		return Resources.Load<Sprite> ("PushInv");
	}

	public override string GetDescription ()
	{
		return "Shove your foes away";
	}

	protected override List<Vector2> GetValidTargets() {
		return meleeTargetingStrategy.GetTargets (player.transform.position);
	}

	public override string GetName() {
		return "Push";
	}

}

