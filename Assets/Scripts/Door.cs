
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Map;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {
    public float restartLevelDelay = 1f;

    BoxCollider2D box;
    Room destination;
	Direction direction = Direction.NORTH;

    void Awake() {
        box = GetComponent<BoxCollider2D>();
    }

	public void SetDestination(Room destination, Direction direction) {
        this.destination = destination;
		this.direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            GameManager.instance.Save();
            Invoke("Restart", restartLevelDelay);
			GameManager.instance.ChangeRoom(destination, direction);
            other.GetComponent<PlayerController>().enabled = false  ;
        }
    }

    private void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}