using UnityEngine;
using System.Collections.Generic;

public class Health : MonoBehaviour, Observable {

	public int maxHealth = 10;
	public int currentHealth;
	private List<Observer> observers = new List<Observer>();
	public int tempHealth; 

	// Use this for initialization
	void Awake () {
		currentHealth = maxHealth;
		tempHealth = currentHealth; 
	}
	public void Heal(int heal) {
		currentHealth += heal;
		if (currentHealth > maxHealth)
			currentHealth = maxHealth;
		tempHealth = currentHealth; 
		alert (); 
	}
	public void TakeDamage(int damage) {
		currentHealth -= damage;
		print(tempHealth + "temp"); 
		print (currentHealth + "current"); 
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
