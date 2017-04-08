using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class Player : MovingObject,  Observer, Controllable   {

    public float restartLevelDelay = 0.1f;
    public int wallDamage = 1;
	private Health health;
    private Animator animator;
	private bool increasingHealth = false; 

    private ActionCommandFactory actionCommandFactory;
    //private Dictionary<string, ActionCommand> actions;

    private ActionBar actionBar;

    private PlayerController controller;



	// Use this for initialization
	protected override void Start () {
        base.Start();
        animator = GetComponent<Animator>();
        //actions = new Dictionary<string, ActionCommand>();
        //actions.Add("1", actionCommandFactory.CreateMeleeAttack());
		//actions.Add ("2", actionCommandFactory.CreateKeyAction (Color.red));

        controller = GetComponent<PlayerController>();
	}

	public void Awake() {
		health = GetComponent<Health> ();
        actionBar = GameObject.Find("ActionBar").GetComponent<ActionBar>();

        health.Register (this);
	}

	// Update is called once per frame
	public void ControlUpdate () {

        if (!GameManager.instance.IsPlayersTurn()) return;

		if (Input.GetKeyDown (KeyCode.I)) {
			controller.OpenInventory ();
			return;
		}

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
            actionBar.Use(0);
			animator.SetTrigger("CharacterBasicAttack");
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
            actionBar.Use(1);
        } else if (Input.GetKeyDown (KeyCode.Alpha3)) {
            actionBar.Use(3);
        } else if (Input.GetKeyDown (KeyCode.Alpha4)) {
            actionBar.Use(4);
		}
    }

    public PlayerState SnapshotState() {
        PlayerState output = new PlayerState(actionBar.GetActions(), health.currentHealth);
        return output;
    }

    public void LoadState(PlayerState state) {
        Debug.Log("Loading player state");
        actionBar.SetActions(state.actions);
        this.health.currentHealth = state.currentHealth;
    }

    public PlayerState DefaultState() {
        this.actionCommandFactory = ActionCommandFactory.GetInstance();

        List<ActionCommand> actions = new List<ActionCommand>();
        actions.Add(actionCommandFactory.CreateMeleeAttack());
        actions.Add(actionCommandFactory.CreateKeyAction(Color.red));
		actions.Add (actionCommandFactory.CreateRangedAttack ());
		actions.Add (actionCommandFactory.CreatePushAttack ());

        return new PlayerState(actions, health.maxHealth);
    }

	public void EquipAction(ActionCommand action, string key) {
		Debug.Log ("Action set");
		//actions [key] = action;
	}

    private void OnTriggerEnter2D(Collider2D other) {
		increasingHealth = false; 
        if (other.tag == "Exit") {
            enabled = false;
        }

        else if (other.tag == "Food") {
			health.Heal(2);
			increasingHealth = true; 
            other.gameObject.SetActive(false);
        }

        else if (other.tag == "Soda") {
			health.Heal(5);
			increasingHealth = true; 
            other.gameObject.SetActive(false);
        }

		else if (other.tag == "Flower") {
			//Add pointsPerFlower to players food points total
			health.Heal(5);
			increasingHealth = true; 
			//Disable the flower object the player collided with.
			other.gameObject.SetActive(false);
		}
    }

    protected override void OnCantMove<T>(T component) {
        //Set hitWall to equal the component passed in as a parameter.
        Wall hitWall = component as Wall;

        //Call the DamageWall function of the Wall we are hitting.
        hitWall.DamageWall(wallDamage);

        //Set the attack trigger of the player's animation controller in order to play the player's attack animation.
        //animator.SetTrigger("CharacterBasicAttack");
    }

    public bool IsActive() {
        return true;
    }

    public void EndTurn() {
        GameManager.instance.playersTurn = false;
    }

	public void Notify() {
		if (increasingHealth = true) {
			animator.SetTrigger ("playerHit");
		}
		if (health.currentHealth <= 0) {
			GameManager.instance.GameOver ();
		}
	}

	public void Setup() {

	}

	public void TearDown() {

	}
		
}
