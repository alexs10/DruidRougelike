
using UnityEngine;


class MeleeAttackActionCommand: ActionCommand {
    private int damage;

    private TargetingController targetController;
    private PlayerController playerController;

    public MeleeAttackActionCommand(int damage, TargetingController targetController, PlayerController playerController) {
        this.damage = damage;
        this.targetController = targetController;
        this.playerController = playerController;
    }

    public int Execute() {
        Debug.Log("MELEE ATTACK: " + damage);
        playerController.ChangeState(targetController);
        return 2;
    }
}

