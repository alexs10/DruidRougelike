using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void PauseGame() {
		SceneManager.LoadScene ("Pause-menu", LoadSceneMode.Additive);
	}

	public void ResumeGame() {
		//SceneManager.SetActiveScene(SceneManager.GetSceneByName("MapGen"));
		SceneManager.UnloadSceneAsync ("Pause-menu");

	}
}
