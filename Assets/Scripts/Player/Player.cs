using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MovingObject, Turnable, Observer, Controllable   {

    public float restartLevelDelay = 1f;
    public int wallDamage = 1;
	private Health health;
    private Animator animator;

    private bool playersTurn = false;
    private EndTurnCallback endTurnCallback = null;
    private ActionCommandFactory actionCommandFactory; 
    private Dictionary<string, ActionCommand> actions;

    private PlayerController controller;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        animator = GetComponent<Animator>();

        this.actionCommandFactory = ActionCommandFactory.GetInstance();
        actions = new Dictionary<string, ActionCommand>();
        actions.Add("1", actionCommandFactory.CreateMeleeAttack());

        controller = GetComponent<PlayerController>();

        TurnKeeper.instance.Register(this, 1);
	}

	public void Awake() {
		health = GetComponent<Health> ();
		health.Register (this);
	}

	// Update is called once per frame
	public void ControlUpdate () {
        //if (!playersTurn) return;

        int horizontal = 0;
        int vertical = 0;

        //MOVING
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));
        if (horizontal != 0) {
            vertical = 0;
        }
        if (horizontal != 0 || vertical != 0) {
            AttemptMove<Wall>(horizontal, vertical);
            EndTurn(5);
        }

        //ACTIONS
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            int turnDelay = actions["1"].Execute();
            if (turnDelay > 0)
                EndTurn(turnDelay);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Check if the tag of the trigger collided with is Exit.
        if (other.tag == "Exit") {
            //Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
            Invoke("Restart", restartLevelDelay);

            //Disable the player object since level is over.
            enabled = false;
        }

        //Check if the tag of the trigger collided with is Food.
        else if (other.tag == "Food") {
            //Add pointsPerFood to the players current food total.
			health.Heal(2);
            //health += 10;

            //Disable the food object the player collided with.
            other.gameObject.SetActive(false);
        }

        //Check if the tag of the trigger collided with is Soda.
        else if (other.tag == "Soda") {
            //Add pointsPerSoda to players food points total
			health.Heal(5);


            //Disable the soda object the player collided with.
            other.gameObject.SetActive(false);
        }
    }

    protected override void OnCantMove<T>(T component) {
        //Set hitWall to equal the component passed in as a parameter.
        Wall hitWall = component as Wall;

        //Call the DamageWall function of the Wall we are hitting.
        hitWall.DamageWall(wallDamage);

        //Set the attack trigger of the player's animation controller in order to play the player's attack animation.
        animator.SetTrigger("playerChop");
    }

    public void TakeTurn(EndTurnCallback endTurnCallback) {
        this.endTurnCallback = endTurnCallback;
        controller.setInactive(false);
    }

    private void EndTurn(int turnsInactive) {
        controller.setInactive(true);
        endTurnCallback(this, true, 5, MovingObject.SmoothMoveTime);
    }

	public void Notify() {
		animator.SetTrigger ("playerHit");
		if (health.currentHealth <= 0) {
			GameManager.instance.GameOver ();
		}
	}
		
}
