using UnityEngine;
using System.Collections;
using System;

public class Player : MovingObject, Turnable   {

    public float restartLevelDelay = 1f;
    public int wallDamage = 1;
    private int health;
    private Animator animator;

    private bool playersTurn = false;
    private EndTurnCallback endTurnCallback = null;


	// Use this for initialization
	protected override void Start () {
        base.Start();
        animator = GetComponent<Animator>();
        Debug.Log("about to go balls deep");
        TurnKeeper.instance.Register(this, 1);
        Debug.Log("wen balls deep");
	}
	
	// Update is called once per frame
	void Update () {
        if (!playersTurn) return;

        int horizontal = 0;
        int vertical = 0;

        //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));

        //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        //Check if moving horizontally, if so set vertical to zero.
        if (horizontal != 0) {
            vertical = 0;
        }

        //Check if we have a non-zero value for horizontal or vertical
        if (horizontal != 0 || vertical != 0) {
            //Call AttemptMove passing in the generic parameter Wall, since that is what Player may interact with if they encounter one (by attacking it)
            //Pass in horizontal and vertical as parameters to specify the direction to move Player in.
            AttemptMove<Wall>(horizontal, vertical);
            EndTurn(5);
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
            health += 10;

            //Disable the food object the player collided with.
            other.gameObject.SetActive(false);
        }

        //Check if the tag of the trigger collided with is Soda.
        else if (other.tag == "Soda") {
            //Add pointsPerSoda to players food points total
            health += 20;


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

    public void TakeDamage( int loss) {
        animator.SetTrigger("playerHit");
        health -= loss;
        CheckIfGameOver();
    }

    public void TakeTurn(EndTurnCallback endTurnCallback) {
        Debug.Log("Taking my turn bby0");
        this.endTurnCallback = endTurnCallback;
        playersTurn = true;
    }

    private void EndTurn(int turnsInactive) {
        Debug.Log("Ending turn bby0");
        playersTurn = false;
        endTurnCallback(this, true, 5, MovingObject.SmoothMoveTime);
    }

    private void CheckIfGameOver() {
        if (health <= 0) {
            GameManager.instance.GameOver();
        }
    }
}
