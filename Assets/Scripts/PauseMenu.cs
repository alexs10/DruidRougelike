using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	bool paused = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void PauseGame() {
		//SceneManager.LoadScene ("Pause-menu", LoadSceneMode.Additive);
		Time.timeScale = 0.0f; 
		paused = true; 
	}

	public void OnGUI()
	{
		if (paused == true) {

			GUIStyle pauseButtonStyle = new GUIStyle (GUI.skin.button); 
			Font myFont = (Font)Resources.Load ("Fonts/PressStart2P-Regular", typeof(Font)); 
			pauseButtonStyle.font = myFont; 
			pauseButtonStyle.fontSize = 14; 
			if (GUI.Button (new Rect((Screen.width/2)-150,200,300,50), "Click me to unpause", pauseButtonStyle)) 
				{
				Time.timeScale = 1.0f; 
				paused = false; 
			} 
		}

	} 
}
