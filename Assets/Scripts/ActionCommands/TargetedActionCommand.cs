using UnityEngine;
using System.Collections.Generic;

public abstract class TargetedActionCommand: ActionCommand, Observer {

	private TargetingController targetingController;
	private PlayerController playerController;
    protected Player player;

	public TargetedActionCommand (	TargetingController targetingController,
		PlayerController playerController,
        Player player) {
		this.targetingController = targetingController;
		this.playerController = playerController;
        this.player = player;
	}

	public void Execute() {
        targetingController = GameObject.Find("TargetingUI").GetComponent<TargetingController>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        player = GameObject.Find("Player").GetComponent<Player>();

        targetingController.DefineValidTargets (GetValidTargets ());
		playerController.ChangeState (targetingController);
		targetingController.Register (this);

	}

	public void Notify() {
		if (targetingController.HasTarget ()) {
			DoExecute (targetingController.TakeTarget ());
		}
	}

    public abstract string GetName();

	protected abstract void DoExecute (Transform target);

	protected abstract List<Vector2> GetValidTargets ();
}

