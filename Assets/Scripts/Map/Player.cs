using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MovingObject,  Observer, Controllable   {

    public float restartLevelDelay = 0.1f;
    public int wallDamage = 1;
	private Health health;
    private Animator animator;

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
		actions.Add ("2", actionCommandFactory.CreateKeyAction (Color.red));

        controller = GetComponent<PlayerController>();
	}

	public void Awake() {
		health = GetComponent<Health> ();
		health.Register (this);
	}

	// Update is called once per frame
	public void ControlUpdate () {

        if (!GameManager.instance.IsPlayersTurn()) return;

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
            EndTurn();
        }

        //ACTIONS
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			actions ["1"].Execute ();
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			actions ["2"].Execute ();
		}
    }

	public void EquipAction(ActionCommand action, string key) {
		actions [key] = action;
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Exit") {
            enabled = false;
        }

        else if (other.tag == "Food") {
			health.Heal(2);
            other.gameObject.SetActive(false);
        }

        else if (other.tag == "Soda") {
			health.Heal(5);
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

    public bool IsActive() {
        return true;
    }

    public void EndTurn() {
        GameManager.instance.playersTurn = false;
    }

	public void Notify() {
		animator.SetTrigger ("playerHit");
		if (health.currentHealth <= 0) {
			GameManager.instance.GameOver ();
		}
	}
		
}
