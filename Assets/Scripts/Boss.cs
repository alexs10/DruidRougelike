using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : Enemy {
	public GameObject minion;
	public float spawnChance = 0.4f;


	public override void MoveEnemy() {

		if (Random.value < spawnChance) {
			Debug.Log ("Spawning");

			float xSpawn = transform.position.x + Random.Range (-2, 2);
			float ySpawn = transform.position.y + Random.Range (-2, 2);
			if (xSpawn < 0)
				xSpawn = 0;
			if (xSpawn > 11)
				xSpawn = 11;
			if (ySpawn < 0)
				ySpawn = 0;
			if (ySpawn > 7)
				ySpawn = 7;

			Vector2 minionSpawn = new Vector2 (xSpawn, ySpawn);
				


			int blockingLayer = LayerMask.GetMask ("BlockingLayer");

			Debug.Log (blockingLayer);

			RaycastHit2D blockHit = Physics2D.Raycast(minionSpawn, Vector2.zero, 0f, blockingLayer);


			if (blockHit.transform == null) {
				Debug.Log ("Made it");
				GameObject enemy = Instantiate (minion, minionSpawn, Quaternion.identity);
			}

		}

		base.MoveEnemy ();
	}

	public override void Die() {
		base.Die (); 
		//Debug.Log("Enemy is dead");
		//GameManager.instance.RemoveEnemy (this);
		//Destroy(gameObject);
		SceneManager.LoadScene("Win"); 

	}
}
