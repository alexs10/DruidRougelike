
using UnityEngine;


class MeleeAttackActionCommand: ActionCommand, Observer {
    private int damage;

    private TargetingController targetController;
    private PlayerController playerController;
    private Player player;

    private int damagingLayer;

    public MeleeAttackActionCommand(int damage, TargetingController targetController, PlayerController playerController, Player player) {
        this.damage = damage;
        this.targetController = targetController;
        this.playerController = playerController;
        this.player = player;

        damagingLayer = LayerMask.GetMask("BlockingLayer");
    }

    public int Execute() {
        playerController.ChangeState(targetController);
        targetController.Register(this);
        return 2;
    }

    public void Notify() {
        targetController.Deregister(this);
        if (targetController.HasTarget()) {;
            Transform target = targetController.TakeTarget();
            Vector2 rayPos = target.position;
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f, damagingLayer);

            if (hit) {
                hit.collider.GetComponent<Enemy>().TakeDamage(2);
                Debug.Log("MELEE ATTACK: " + damage);
            }

            player.EndTurn(2);
        } else {
            //do nothing
        }
    }

}

