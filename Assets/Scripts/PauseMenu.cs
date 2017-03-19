using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	bool paused = false;

	public void PauseGame() {
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
			pauseButtonStyle.normal.textColor = Color.black; 
			pauseButtonStyle.hover.textColor = Color.white; 

			if (GUI.Button (new Rect((Screen.width/2)-150,200,300,50), "Resume Game", pauseButtonStyle)) 
				{
				Time.timeScale = 1.0f; 
				paused = false; 
			} 
		}

	} 
}
