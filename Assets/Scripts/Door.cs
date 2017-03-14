
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Map;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {
    public float restartLevelDelay = 1f;

    BoxCollider2D box;
    Room destination;

    void Awake() {
        box = GetComponent<BoxCollider2D>();
    }

    public void SetDestination(Room destination) {
        Debug.Log("setting dest to " + destination.x + "," + destination.y);
        this.destination = destination;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Debug.Log("Destination is " + destination.x + "," + destination.y);
            Invoke("Restart", restartLevelDelay);
            GameManager.instance.currentRoom = destination;
            other.GetComponent<PlayerController>().enabled = false  ;
        }
    }

    private void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}