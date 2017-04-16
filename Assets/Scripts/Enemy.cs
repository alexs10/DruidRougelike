using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

//Enemy inherits from MovingObject, our base class for objects that can move, Player also inherits from this.
public class Enemy : MovingObject {
    public int playerDamage = 2;                            //The amount of food points to subtract from the player when attacking.


    private Animator animator;                          //Variable of type Animator to store a reference to the enemy's Animator component.
    private Transform target;                           //Transform to attempt to move toward each turn.
    private bool skipMove;                              //Boolean to determine whether or not enemy should skip a turn or move this turn.

    public int maxHealth = 10;
    private int health;
	private Text damageText; 
	bool showText = false; 
	Rect textArea = new Rect(200,200,200, 200); 


    protected override void Start() {
        //TurnKeeper.instance.Register(this,5);
        GameManager.instance.AddEnemyToList(this);
        animator = GetComponent<Animator>();

		this.health = maxHealth;

        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
    }

	void onGUI()
	{
		GUI.Label (textArea, "test"); 
		if (showText) {
			GUI.Label (textArea, "test2"); 
		}
	}


    protected override void AttemptMove<T>(int xDir, int yDir) {
        if (skipMove) {
            skipMove = false;
            return;
        }

        base.AttemptMove<T>(xDir, yDir);

        skipMove = true;
    }


    public virtual void MoveEnemy() {
        int xDir = 0;
        int yDir = 0;

        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
            yDir = target.position.y > transform.position.y ? 1 : -1;
        else
            xDir = target.position.x > transform.position.x ? 1 : -1;

        AttemptMove<Health>(xDir, yDir);
    }


    protected override void OnCantMove<T>(T component) {
		Debug.Log ("on cant move enemy");
        Health health = component as Health;

		Debug.Log("pd: " + playerDamage);
        health.TakeDamage(playerDamage);

        animator.SetTrigger("enemyAttack");

    }

    bool isActive = true;
    public bool IsActive() {
        return isActive;
    }

    public void TakeDamage(int damage) {
        health -= damage;
		showText = true; 
		animator.SetTrigger ("enemyHit"); 
        if (health <= 0) {
            Die();
        }
		//showText = false; 
    }

    public void Die() {
        Debug.Log("Enemy is dead");
        isActive = false;
		GameManager.instance.RemoveEnemy (this);
        Destroy(gameObject);
    }
}