using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour, Observer {

    private Text healthText;
    private Health playerHealth;

	// Use this for initialization
	void Start () {
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
        playerHealth.Register(this);
        Notify();
	}
	
	public void Notify() {
        Debug.Log("boooyyyy");
		healthText.text = "Health: " + playerHealth.currentHealth + "/10";
    }
}
