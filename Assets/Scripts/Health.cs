using UnityEngine;
using System.Collections.Generic;

public class Health : MonoBehaviour, Observable {

	public int maxHealth = 10;
	public int currentHealth;
	private List<Observer> observers = new List<Observer>();

	// Use this for initialization
	void Awake () {
		currentHealth = maxHealth;
	}
	public void Heal(int heal) {
		currentHealth += heal;
		if (currentHealth > maxHealth)
			currentHealth = maxHealth;
		alert (); 
	}
	public void TakeDamage(int damage) {
		currentHealth -= damage;
		Debug.Log (currentHealth);
		alert ();
	}

	private void alert() {
		foreach (Observer observer in observers) {
			observer.Notify ();
		}
	}

	public void Register(Observer o) {
        Debug.Log("registered");
		observers.Add (o);
	}

	public void Deregister(Observer o) {
		observers.Add (o);
	}
}
